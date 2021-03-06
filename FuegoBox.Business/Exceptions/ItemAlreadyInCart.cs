﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business.Exceptions
{
    public class ItemAlreadyInCart: Exception
    {
        public ItemAlreadyInCart() { }
        public ItemAlreadyInCart(string message) : base(message) { }
        public ItemAlreadyInCart(string message, Exception inner) : base(message, inner) { }
    }
}
