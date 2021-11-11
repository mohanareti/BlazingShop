using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazingShop.DomainModel
{
   public class Category
    {
        [Key]
        public int CId { get; set; }

        [Required(ErrorMessage = "Please enter Category")]
        [StringLength(30, ErrorMessage = "Category cannot be greater than 30 chars")]
        [MinLength(2, ErrorMessage = "category should contain atleast 3 chars")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Please enter alphabets only")]
        public string CName { get; set; }
    }
}
