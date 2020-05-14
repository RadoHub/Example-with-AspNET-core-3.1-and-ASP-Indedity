using Prj.Bus.Abstract;
using Prj.Dal.Abstract;
using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prj.Bus.Concrete
{
    public class OrderManager : IOrderService
    {
        private IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        public void Create(Order entity)
        {
            _orderDal.CreateOrderAndOrderItems(entity);
        }

        public List<Order> GetOrders(int? userId)
        {
            return _orderDal.GetOrders((int)userId).ToList();
        }


    }
}
