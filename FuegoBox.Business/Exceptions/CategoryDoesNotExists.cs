using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business.Exceptions
{
    public class CategoryDoesNotExists : Exception
    {
        public CategoryDoesNotExists() { }
        public CategoryDoesNotExists(string message) : base(message) { }
        public CategoryDoesNotExists(string message, Exception inner) : base(message, inner) { }
    }
}
