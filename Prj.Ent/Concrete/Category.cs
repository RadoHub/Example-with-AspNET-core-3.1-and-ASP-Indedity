using Prj.Ent.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Ent.Concrete
{
    public class Category: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
