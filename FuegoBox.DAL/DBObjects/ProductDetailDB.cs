using AutoMapper;
using FuegoBox.DAL.Exceptions;
using FuegoBox.Shared.DTO.Category;
using FuegoBox.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.DAL.DBObjects
{
   public class ProductDetailDB
    {
        FuegoEntities dbContext;
        public ProductDetailDB()
        {
            dbContext = new FuegoEntities();
        }

        //function to get the details of the product using name of the product
   public ProductDetailDTO GetDetail(ProductDetailDTO productDetailDTO)
        {
            Product product = dbContext.Product.Where(a => a.Name == productDetailDTO.Name).FirstOrDefault();
           if (product!= null)
            {
                ProductDetailDTO newBasicDTO = new ProductDetailDTO();            
                newBasicDTO.Name = product.Name;
                newBasicDTO.Variants = (from v in dbContext.Variant.Where(cdf => cdf.ProductID == product.ID)
                                        join vp in dbContext.VariantProperty on v.ID equals vp.VariantID
                                        join img in dbContext.VariantImage on v.ID equals img.VariantID
                                        join vpv in dbContext.VariantPropertyValue on vp.PropertyValueID equals vpv.ID
                                        join p in dbContext.Property on vpv.PropertyID equals p.ID
                                        join value in dbContext.Value on vpv.ValueID equals value.ID
                                        select new VariantDTO()
                                        {
                                            ListingPrice=v.ListingPrice,
                                            Discount=v.Discount,
                                            Variant_Property=p.Name,
                                            Variant_Value1=value.Name,
                                            image = img.ImageURL
                                        });
               Variant var=  dbContext.Variant.Where(cdf => cdf.ProductID == product.ID).FirstOrDefault();
               VariantImage ima = dbContext.VariantImage.Where(cd => cd.VariantID == var.ID).FirstOrDefault();
               newBasicDTO.ImageURL = ima.ImageURL;
                return newBasicDTO;
            }
            return null;
        }

        //to check whether the product exists or not.
        public bool ProductExists(string Name)
        {
            Product product = dbContext.Product.Where(asd => asd.Name == Name).FirstOrDefault();
            if (product == null)
            {
                throw new ProductNotFound();
            }
            return true;
        }

        //adding product to the cart using the loggedin user's userID
        public ProductDetailDTO AddProduct( ProductDetailDTO pdto,Guid user_id)
        {
            Product product = dbContext.Product.Where(a => a.Name == pdto.Name).FirstOrDefault();
            Variant variant = dbContext.Variant.Where(s => s.ProductID == product.ID).FirstOrDefault();
            ProductDetailDTO cartdto = new ProductDetailDTO();
            Cart cart = new Cart();           
            cart.ID = Guid.NewGuid();
            cart.VariantID = variant.ID;
            cart.SellingPrice = variant.Discount;
            cart.Qty = 2;
            cart.UserID = user_id;
            cartdto.Name = product.Name;        
            dbContext.Cart.Add(cart);
            dbContext.SaveChanges();
            return cartdto;           
        }


        //search the product using the name or description of the product
       public ProductSearchResultDTO GetProductSearch(string searchString)
        {
            ProductSearchResultDTO newProductsSearchResultDTO = new ProductSearchResultDTO();
            newProductsSearchResultDTO.Products= (from pi in dbContext.Product.Where(p => p.Name.Contains(searchString) || p.Description.Contains(searchString))
                                                 join v in dbContext.Variant on pi.ID equals v.ProductID
                                                 join img in dbContext.VariantImage on v.ID equals img.VariantID
                                                 select new ProductDetailDTO()
                                                 {
                                                     ImageURL = img.ImageURL,
                                                     Name = pi.Name,
                                                     Description=pi.Description
                                                 }).ToList();            
            return newProductsSearchResultDTO;
        }
    }
}
