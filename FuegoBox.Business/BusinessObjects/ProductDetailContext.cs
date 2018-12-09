using AutoMapper;
using FuegoBox.Business.Exceptions;
using FuegoBox.DAL.DBObjects;
using FuegoBox.DAL.Exceptions;
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
            try
            {
                bool exists = ProductDBObject.ProductExists(productDetailDTO.Name);
            }
            catch (NotFoundException ex)
            {
                throw new ProductDoesNotExists();
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown Error");
            }


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
            try
            {
                ProductSearchResultDTO produDetailDTO = ProductDBObject.GetProductSearch(searchString);
                return produDetailDTO;
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown Error");
            }
        }


    }
}
