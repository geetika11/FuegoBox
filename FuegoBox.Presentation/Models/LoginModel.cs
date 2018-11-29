using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class LoginModel
    {
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required]
        public string Password { get; set; }


    }


    }