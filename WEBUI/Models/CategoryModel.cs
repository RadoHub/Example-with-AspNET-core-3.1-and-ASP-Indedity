using Prj.Ent.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prj.WebUI.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(15, ErrorMessage ="En az 3 en fazla 15 karakter olmalidir", MinimumLength =3)]

        /*  
         eger property int veya decimal gibi sayisal tiplerden ise valuetype nullable cekilmelidir 
         Requiered Annotationun calisabilmesi icin. public decimal? Price {get; set;}  seklinde olmalidir   
         Daha sonra ise VIEW kisminda <span> icinde asp-validation-error="Name" kisminda propert adi yazilmalidir model ile giden
         Istersen hatalari tek bir span altinda asp-validation-summary ="All" yaparak tek bir psan altinda gosterebiliriz
             */
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public PageViewModel PageInfo { get; set; }
    }
}
