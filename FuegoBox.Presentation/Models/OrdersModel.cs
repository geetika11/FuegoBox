﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FuegoBox.Presentation.Models
{
    public class OrdersModel
    {
        public IEnumerable<OrderModel> orders { get; set; }
    }
}