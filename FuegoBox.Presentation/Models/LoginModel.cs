﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class LoginModel
    {
        [Required]
        public string EmailId { get; set; }

        [Required]
        public string Password { get; set; }


    }
}