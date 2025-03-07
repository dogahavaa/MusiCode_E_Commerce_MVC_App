using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MusiCodeWebApp.Models
{
    public class Product : Entity
    {
        public Product()
        {
            IsDeleted = false;
            IsActive = true;
        }

        [Display(Name = "İsim")]
        [Required(ErrorMessage = "Bu alan zorunludur.")]
        [StringLength(maximumLength: 100, ErrorMessage = "Marka ismi 100 karakterden uzun olamaz.")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public double Price { get; set; }
        public short Stock { get; set; }
        public string Image { get; set; } //Birden fazla fotoğraf almayı hocaya sor
        public bool IsActive { get; set; }

        public int Category_ID { get; set; }
        [ForeignKey("Category_ID")]
        public virtual Category Category { get; set; }

        public int Brand_ID { get; set; }
        [ForeignKey("Brand_ID")]
        public virtual Brand Brand { get; set; }
    }
}