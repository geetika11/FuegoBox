using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Shared.DTO.Product
{
    public class ProductDetailDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderLimit { get; set; }
        public IEnumerable<ImageDTO>ImageURL { get; set; }
       //public string ImageURL { get; set; }    
    }
}
