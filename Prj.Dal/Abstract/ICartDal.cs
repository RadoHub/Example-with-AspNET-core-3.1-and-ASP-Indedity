using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Dal.Abstract
{
    public interface ICartDal : IRepository<Cart>
    {
        Cart GetByUserId(int userId);
        void DeleteFromCart(int cartId, int productId);
        void ClearUserCart(int cartId);
    }
}
