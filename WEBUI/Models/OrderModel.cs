using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Models
{
    //Order hem OrderModel hem de Payment classlarindan bilgiler alip genel sinif olarak kullanilacak. OrderModel bir kismi, payment diger kismi olacakl
    public class OrderModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        [Display(Name = "Şehir")]
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string Cvv { get; set; }

        //CART controller altinda CHECKOUT action`inda var oderModel= new OrderModel; den sonra orderModel.CartModel = new CartModel (){ CartId= cart.id ....}
        // yapmak icin kullanicaz. Boylece iki modeli de tasiyabilicez.  CartAndCartItemViewModel ile ayni cs icinden aldik.
        public CartViewModel CartViewModel { get; set; }
    }
}
