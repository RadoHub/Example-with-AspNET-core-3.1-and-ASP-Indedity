using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Models
{
    public class ProductsModel
    {
        public List<Product>  Products { get; set; }
        public PageViewModel  PageModel { get; set; }
    }
}
