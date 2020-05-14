using Microsoft.EntityFrameworkCore;
using Prj.Dal.Abstract;
using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prj.Dal.Concrete.EF
{
    public class EFCartDal : EFBaseRepository<Cart, ShopContext>, ICartDal
    {
        public override void UpdateObj(Cart entity)
        {
            using (var con = new ShopContext())
            {
                
                con.Carts.Update(entity);
                con.SaveChanges();

            }

        }
        public Cart GetByUserId(int userId)
        {
            using (var con = new ShopContext())
            {
                return con.Carts
                    .Include(i => i.CartItems)  
                    .ThenInclude(i => i.Product)  
                    .FirstOrDefault(i => i.UserId == userId);  
            }
        }

        public void DeleteFromCart(int cartId, int productId)
        {
            using (var con = new ShopContext())
            {
                var cmd = @"delete from CartItems where CartId=@p0 And ProductId=@p1";
                con.Database.ExecuteSqlCommand(cmd, cartId, productId);
            }
        }

        public void ClearUserCart( int cartId)
        {
            using (var con = new ShopContext())
            {
                var command = @"delete from CartItems where CartId=@p0";
                con.Database.ExecuteSqlCommand(command, cartId);
            }
        }
    }
}
