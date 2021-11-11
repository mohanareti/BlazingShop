using BlazingShop.DomainModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingShop.VIewModels
{
    public class ProductViewModel
    {
        [Key]
        public int PId { get; set; }
        [Required(ErrorMessage = "Please enter Product")]
        [StringLength(30, ErrorMessage = "Product cannot be greater than 30 chars")]
        [MinLength(2, ErrorMessage = "Product should contain atleast 3 chars")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Please enter alphabets only")]
        [Display(Name = "Product Name")]
        public string PName { get; set; }

        [Required(ErrorMessage = "Please enter price")]
        [Range(0, 500, ErrorMessage = "Price can be between 0 and 500")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Please choose a image")]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "Please specify color")]
        public string ShadeColor { get; set; }
        [Required(ErrorMessage = "Please select category name")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        //public IEnumerable<Category> Categories { get; set; }

    }
}
