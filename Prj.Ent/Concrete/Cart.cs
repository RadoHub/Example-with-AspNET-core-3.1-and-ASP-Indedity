using Prj.Ent.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Ent.Concrete
{
    public class Cart: IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public List<CartItem> CartItems { get; set; } 
    }
}
