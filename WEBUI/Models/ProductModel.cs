using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Models
{
    public class ProductModel: Product
    {

        public List<Category> SelectedCategories { get; set; }
    }
}
