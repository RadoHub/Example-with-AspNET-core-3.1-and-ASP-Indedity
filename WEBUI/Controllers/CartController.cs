using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IyzipayCore;
using IyzipayCore.Model;
using IyzipayCore.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Prj.Bus.Abstract;
using Prj.Ent.Concrete;
using Prj.WebUI.Identity;
using Prj.WebUI.Models;

namespace Prj.WebUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ICartService _cartService;
        private IOrderService _orderService;
        private UserManager<ApplicationUser> _userManager;
        public CartController(ICartService cartService, IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);
            var cart = _cartService.GetCartByUserId(user.Id); 
            return View(new CartViewModel() 
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(c => new CartItemViewModel() 
                {
                    CartItemId = c.Id,
                    ProductId = c.Product.Id,
                    Name = c.Product.Name,
                    Price = (int)c.Product.Price,
                    ImageUrl = c.Product.ImgUrl,
                    Quantity = c.Quantity,
                }).ToList()
            }); ;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user?.Id;
            _cartService.AddToCart(user.Id, productId, quantity);
            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFromCart(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = _cartService.GetCartByUserId(user.Id);
            _cartService.DeleteFromCart(user.Id, productId);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> CheckOut()
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = _cartService.GetCartByUserId(user.Id);
            var orderModel = new OrderModel();

            orderModel.CartViewModel = new CartViewModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemViewModel()
                { 
                    CartItemId = i.Id,
                    ProductId = i.Product.Id,
                    Name = i.Product.Name,
                    Price = i.Product.Price.Value,
                    ImageUrl = i.Product.ImgUrl,                    
                    Quantity = i.Quantity

                }).ToList()
            };



            return View(orderModel);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderModel ordermodel)
        {

            if (ModelState.IsValid)
            {
                var userId = await _userManager.GetUserAsync(User);

                var cart = _cartService.GetCartByUserId(userId.Id);


                ordermodel.CartViewModel = new CartViewModel()
                {
                    CartId = cart.Id,
                    CartItems = cart.CartItems.Select(i => new CartItemViewModel()
                    {
                        CartItemId = i.Id,
                        ProductId = i.Product.Id,
                        ImageUrl = i.Product.ImgUrl,
                        Name = i.Product.Name,
                        Price = i.Product.Price.Value,
                        Quantity = i.Quantity

                    }).ToList()
                };

                var payment = PaymentProcess(ordermodel);


                if (payment.Status == "success")
                {
                    SaveOrder(ordermodel, payment, userId.Id);

                    ClearCart( cart.Id);
                    return View("Success");
                }

            }



            return View(ordermodel);
        }

        private void ClearCart( int cartId)
        {
            _cartService.ClearUserCart( cartId);
        }

        private void SaveOrder(OrderModel model, Payment payment, int userId)
        {
            var order = new Order();
            order.OrderNumber = new Random().Next(111111, 999999).ToString();
            order.OrdurState = EnumOrderState.Completed;
            order.PaymentType = EnumPaymentType.CreditCart;
            order.PaymentId = payment.PaymentId;
            order.ConversationId = payment.ConversationId;
            order.OrderDate = new DateTime().Date;
            order.FirstName = model.FirstName;
            order.LastName = model.LastName;
            order.Email = model.Email;
            order.City = model.City;
            order.Phone = model.Phone;
            order.Address = model.Address;
            order.UserId = userId;

            foreach (var item in model.CartViewModel.CartItems)
            {
                var orderItem = new OrderItem()
                {
                    Price = (decimal)item.Price.Value,
                    Qantity = item.Quantity,
                    ProductId = item.ProductId,
                };



                order.OrderItems.Add(orderItem);
            }

            _orderService.Create(order);

        }

        private Payment PaymentProcess(OrderModel model)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-5YimLJUm3186W6YZVBMrp6ZDLwGtF7vX";
            options.SecretKey = "sandbox-FaqlMGqOhNW86qtkfYCwWgmGnDs6wCQJ";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            //payment olusturuluyor
            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = model.CartViewModel.TotalPrice().ToString().Split(",")[0]; //sepettteki ürünlerin toplam tutarı
            request.PaidPrice = model.CartViewModel.TotalPrice().ToString().Split(",")[0]; //vergili fiyati eger varsa yani karttan cekilecek olan tutar
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1; //taksit sayisi
            request.BasketId = model.CartViewModel.CartId.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.CardName;
            paymentCard.CardNumber = model.CardNumber;
            paymentCard.ExpireMonth = model.ExpirationMonth;
            paymentCard.ExpireYear = model.ExpirationYear;
            paymentCard.Cvc = model.Cvv;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            //paymentCard.CardHolderName = "John Doe";
            //paymentCard.CardNumber = "5528790000000008";
            //paymentCard.ExpireMonth = "12";
            //paymentCard.ExpireYear = "2030";
            //paymentCard.Cvc = "123";


            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();

            //teker teker almak icin tek bir elemani temsil etmeli
            BasketItem basketItem;
            foreach (var item in model.CartViewModel.CartItems)
            {
                basketItem = new BasketItem();
                basketItem.Id = item.CartItemId.ToString();
                basketItem.Name = item.Name;
                basketItem.Category1 = "Phone";
                basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                //split verilen tirnak icindeki degeri merkez noktasi alir ve sagini ve solunu ikiye boler. index ile sagdaki soldaki kalanlarin hepsini siralar
                basketItem.Price = (item.Quantity * item.Price).ToString().Split(",")[0];
                basketItems.Add(basketItem);
            }


            request.BasketItems = basketItems;
            Payment payment = Payment.Create(request, options);

            //Success isminde bir view ( cart klasoru altinda ) olusturup oraya yonlendiricez bu degerleri iletmek icin 
            return payment;

            //if (payment.Status == "success")
            //{
            //    return payment;
            //}
            //return payment;
        }

        
        public async Task<IActionResult> GetOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var orders = _orderService.GetOrders(userId);
            var orderListModel = new List<OrderListModel>();
            
            OrderListModel orderModel;
            foreach (var order in orders)
            {
                orderModel = new OrderListModel();
                orderModel.OrderId = order.Id;
                orderModel.OrderDate = order.OrderDate;
                orderModel.OrderNumber = order.OrderNumber;
                orderModel.FirstName = order.FirstName;
                orderModel.LastName = order.LastName;
                orderModel.Phone = order.Phone;
                orderModel.City = order.City;

                orderModel.OrderItems = order.OrderItems.Select(i => new OrderItemModel()
                {
                    OrderItemId = i.Id,
                    Name = i.Product.Name,
                    ImageUrl = i.Product.ImgUrl,
                    Price = i.Price,
                    Quantity = i.Qantity
                }).ToList();
                orderListModel.Add(orderModel);
            }

            return View(orderListModel);
        }
    }
}