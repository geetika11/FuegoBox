﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Shared.DTO.Product
{
    public   class ProductSearchResultDTO
    {
        public IEnumerable<ProductDetailDTO> Products { get; set; }
    }
}
