using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Shared.DTO.Order
{
    public class AddressDTO
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int Pin { get; set; }
        public System.Guid userId { get; set; }
    }
}
