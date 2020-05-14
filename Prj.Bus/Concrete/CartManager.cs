using Prj.Bus.Abstract;
using Prj.Dal.Abstract;
using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Bus.Concrete
{
    public class CartManager : ICartService
    {
        private ICartDal _cartDal;
        public CartManager(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public void AddToCart(int userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);
            if (cart!=null)
            {
                var index = cart.CartItems.FindIndex(c => c.ProductId ==  productId);

                if (index<0)
                {
                    cart.CartItems.Add(new CartItem 
                    { 
                        ProductId=productId,
                        Quantity = quantity,
                        CartId=cart.Id
                    });
                }
                else 
                {
                    cart.CartItems[index].Quantity += quantity;
                }
                _cartDal.UpdateObj(cart);
            }
        }

        public void DeleteFromCart(int userId, int productId)
        {
            var cart = _cartDal.GetByUserId(userId);
            if (cart!=null)
            {
                _cartDal.DeleteFromCart(cart.Id, productId);
            }
        }

        public void ClearUserCart( int cartId)
        {
            _cartDal.ClearUserCart( cartId);
        }

        public Cart GetCartByUserId(int userId)
        {
            return _cartDal.GetByUserId(userId); 
        }

        public void InitializeCart(int id)
        {            
            _cartDal.CreateObj(new Cart() { UserId = id });
        }
    }
}
