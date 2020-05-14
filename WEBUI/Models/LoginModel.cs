using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Models
{
    public class LoginModel
    {
        //username e gore login yapilacak kisim
        //[Required]
        //public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool seciliMi { get; set; }

        public string ReturnUrl { get; set; }
    }
}
