using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Dal.Abstract
{
    public interface IOrderDal : IRepository<Order>
    {
        void CreateOrderAndOrderItems(Order entity);
        List<Order> GetOrders(int? userId);
    }
}
