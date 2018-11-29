using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class RegisterModel
    {
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
       
        public string EmailId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }


        [Required]
        [RegularExpression("^[a-zA-Z ]+", ErrorMessage = "Name may only contain alphabetic characters and space")]
       
        public string Name { get; set; }


        [Required]
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter a valid Mobile Number")]
       
        public string PhoneNo { get; set; }


    }
}