using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Bus.Abstract
{

    public interface ICartService
    {
        void InitializeCart(int id);

        Cart GetCartByUserId(int userId);

        void AddToCart(int userId, int productId, int quantity);
        void DeleteFromCart(int userId, int productId);
        void ClearUserCart(int cartId);
    }
}
