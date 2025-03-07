using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusiCodeWebApp.Models
{
    public class Category:Entity
    {
        public Category()
        {
            IsDeleted = false; 
            IsActive = true;
        }

        [Display(Name ="İsim")]
        [Required(ErrorMessage ="Bu alan zorunludur")]
        [StringLength(maximumLength:75, ErrorMessage ="bu alan en fazla 75 karakter olmalıdır")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength:500, ErrorMessage = "bu alan en fazla 500 karakter olmalıdır")]
        public string Description { get; set; }

        [Display(Name = "Durum")]
        public bool IsActive { get; set; }

        //public virtual ICollection<Brand> Brands { get; set; }

        public virtual ICollection<Product> Product { get; set; }


    }
}