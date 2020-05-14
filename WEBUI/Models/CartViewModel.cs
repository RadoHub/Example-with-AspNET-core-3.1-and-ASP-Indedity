using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Models
{
    public class CartViewModel
    {
        public int CartId { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }

        public int TotalPrice()
        {
            //CartItems uzerinden gelecek degerleri toplu halde toplamaliyiz. 
            return CartItems.Sum(c => c.Quantity * c.Price.Value);
        }
        public int TotalPriceWithParam(int price, int quantity)
        {
            return price * quantity;
        }

    }
}
