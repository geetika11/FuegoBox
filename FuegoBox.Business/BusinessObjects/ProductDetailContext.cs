﻿using AutoMapper;
using FuegoBox.DAL.DBObjects;
using FuegoBox.Shared.DTO.Category;
using FuegoBox.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business.BusinessObjects
{
   public class ProductDetailContext
    {
        ProductDetailDB ProductDBObject;
       
        public ProductDetailContext()
        {
            ProductDBObject = new ProductDetailDB();
        }
        public ProductDetailDTO GetProductDetail(ProductDetailDTO productDetailDTO)
        {
            ProductDetailDTO produDetailDTO = ProductDBObject.GetDetail(productDetailDTO);
            return produDetailDTO;            
        }



        public ProductDetailDTO productAddToCart(ProductDetailDTO productDetailDTO)
        {
            ProductDetailDTO cDTO = ProductDBObject.AddProduct(productDetailDTO);
            return cDTO;
        }

        public ProductSearchResultDTO GetProductwithString(string searchString)
        {
            ProductSearchResultDTO produDetailDTO = ProductDBObject.GetProductSearch(searchString);
            return produDetailDTO;
        }


    }
}
