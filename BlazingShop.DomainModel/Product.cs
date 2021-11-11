using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlazingShop.DomainModel
{
   public class Product
    {
        [Key]
        public int PId { get; set; }

        [Required(ErrorMessage = "Please enter product name")]

        [Display(Name = "Product Name")]
        public string PName { get; set; }
        [Required(ErrorMessage = "Please enter price")]
        [Range(0, 500, ErrorMessage = "Price can be between 0 and 500")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please choose a image")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Please specify color")]
        public string ShadeColor { get; set; }
        [Required(ErrorMessage = "Please select category name")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
