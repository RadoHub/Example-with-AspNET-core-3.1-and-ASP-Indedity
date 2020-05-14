using Microsoft.EntityFrameworkCore;
using Prj.Dal.Abstract;
using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prj.Dal.Concrete.EF
{
    public class EFOrderDal : EFBaseRepository<Order, ShopContext>, IOrderDal
    {
        public void CreateOrderAndOrderItems(Order entity)
        {
            using (var con = new ShopContext())
            {
                con.Orders.Add(entity);
                con.SaveChanges();
            }
        }

        public List<Order> GetOrders(int? userId)
        {

            using (var con = new ShopContext())
            {
                var orders = con.Orders
                    .Include(o => o.OrderItems)  
                    .ThenInclude(o => o.Product) 
                    .AsQueryable();
                if (userId.HasValue)
                {
                    orders = orders.Where(o => o.UserId == userId); 
                }

                return orders.ToList();
            }
        }
    }
}
