using Prj.Ent.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prj.Ent.Concrete
{
    public class Product: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public string  ImgUrl { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
