using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Models
{
    public class PageViewModel
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public string CurrentObj { get; set; }

        public int GetTotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
}
