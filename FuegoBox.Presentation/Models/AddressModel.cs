using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class AddressModel
    {
        [Required]
        public string Address1 { get; set; }

        [Required]
        public string Address2 { get; set; }

        [Required]
        public int Pin { get; set; }

        public System.Guid userId { get; set; }
    }
}