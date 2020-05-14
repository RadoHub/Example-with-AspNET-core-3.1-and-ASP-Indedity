using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Bus.Abstract
{
    public interface IOrderService
    {
        void Create(Order entity);
        List<Order> GetOrders(int? userId);
    }
}
