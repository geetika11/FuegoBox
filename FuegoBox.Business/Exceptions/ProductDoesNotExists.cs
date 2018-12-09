using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business.Exceptions
{
    public class ProductDoesNotExists : Exception
    {
        public ProductDoesNotExists() { }
        public ProductDoesNotExists(string message) : base(message) { }
        public ProductDoesNotExists(string message, Exception inner) : base(message, inner) { }
    }
}
