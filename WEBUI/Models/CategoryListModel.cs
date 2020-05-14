using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Models
{
    public class CategoryListModel
    {
        public List<Category>  Categories { get; set; }
        public PageViewModel PageInfo { get; set; }
    }
}
