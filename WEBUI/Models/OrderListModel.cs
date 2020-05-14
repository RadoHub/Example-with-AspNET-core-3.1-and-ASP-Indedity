using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Models
{
    public class OrderListModel
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState OrderState { get; set; }
        public EnumPaymentType PaymentTypes { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrderNote { get; set; }

        //her bir order degeri icin OrderIem olacak
        public List<OrderItemModel> OrderItems { get; set; }

        public decimal TotalPrice()
        {
            return OrderItems.Sum(o => o.Price * o.Quantity);
        }
    }
}
