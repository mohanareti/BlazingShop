using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlazingShop.DomainModel
{
   public class Appointment
    {
        [Key]
        public int AId { get; set; }

        public int ProductId { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [Required(ErrorMessage = "Please enter date and time")]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(30, ErrorMessage = "Name cannot be greater than 30 chars")]
        [MinLength(2, ErrorMessage = "name should contain atleast 3 chars")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Please enter alphabets only")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(?: (?:\+| 0{0, 2})91(\s*[\ -]\s*)?|[0]?)?[789]\d{9}| (\d[-] ?){ 10}\d$}", ErrorMessage = "" +
            "Enter correct phone number")]
        public string CustomerPhone { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [RegularExpression(@"[\w-]+@([\w -]+\.)+[\w-]+", ErrorMessage = "The email address is not entered in correct format")]
        [StringLength(50)]
        public string CustomerEmail { get; set; }
        public bool IsConfirmed { get; set; }

    }
}
