using Prj.Ent.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Ent.Concrete
{
    public class Order: IEntity        
    {
        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }
        

        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public EnumOrderState OrdurState { get; set; }
        public EnumPaymentType PaymentType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrderNote { get; set; }

        public string PaymentId { get; set; }

        public string PaymentToken { get; set; }

        public string ConversationId { get; set; }

        
        public List<OrderItem> OrderItems { get; set; }
    }

    public enum EnumPaymentType
    {
        CreditCart=0,
        PAYGIRO = 1, 
        PayPal =2 ,
        SofortPay=3
    }

    public enum EnumOrderState
    {
        Waiting=0,
        Unpaid=1,
        Completed=2
    }
}
