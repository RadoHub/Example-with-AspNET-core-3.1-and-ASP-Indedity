﻿using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Models
{
    public class CategoriesMenuModels
    {
        public List<Category>  Categories { get; set; }
        public int  CurrentCategoryId { get; set; }
    }
}
