using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.DAL.Exceptions
{
    public class ProductNotFound : Exception
    {
        public ProductNotFound() { }
        public ProductNotFound(string message) : base(message) { }
        public ProductNotFound(string message, Exception inner) : base(message, inner) { }
    }
}
