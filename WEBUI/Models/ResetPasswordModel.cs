using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Models
{
    public class ResetPasswordModel
    {
        //token Forgotpassword(post) actionu ile mail uzerinden ResetPassword actionu`na gonderilecegi icin, 
        //ResetPassword (httpgeT) de bu degeri ResetPassword(post) methoduna tasimank icin Token propertisini tanimliyoruz.
        public string Token { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(10 , MinimumLength =6 , ErrorMessage ="Password Must at least be 6 and 10 character")]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "RePassword Must at least be 6 and 10 character")]
        [Compare("Password")]
        public string RePassword { get; set; }

    }
}
