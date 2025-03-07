using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MusiCodeWebApp.Models
{
    public class Brand : Entity
    {
        public Brand()
        {
            IsDeleted = false;
            IsActive = true;
        }

        [Display(Name = "İsim")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(maximumLength:100, ErrorMessage = "Marka ismi 100 karakterden uzun olamaz.")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength: 500, ErrorMessage = "Marka açıklaması 500 karakterden uzun olamaz.")]
        public string Description { get; set; }
        public string Logo { get; set; }
        public bool IsActive { get; set; }

        //public int Category_ID { get; set; }
        //[ForeignKey("Category_ID")]
        //public virtual Category Category { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}