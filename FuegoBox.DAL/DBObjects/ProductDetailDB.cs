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
            Category category = dbContext.Category.Where(ab => ab.ID == product.CategoryID).FirstOrDefault();
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            if (product!= null)
            {
                ProductDetailDTO newBasicDTO = new ProductDetailDTO();            
                newBasicDTO.Name = product.Name;
                newBasicDTO.Description = product.Description;
                newBasicDTO.CatName = category.Name;
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
               VariantImage ima = dbContext.VariantImage.Where(cd => cd.VariantID == var.ID).First();
               newBasicDTO.ImageURL = ima.ImageURL;
               newBasicDTO.ListingPrice = var.ListingPrice;
               newBasicDTO.Discount = var.Discount;
               return newBasicDTO;
            }
            return null;
        }

        //to check whether the product exists or not.
        public bool ProductExists(string Name)
        {
            Product product = dbContext.Product.Where(asd => asd.Name == Name).FirstOrDefault();
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            if (product == null)
            {
                throw new ProductNotFound();
            }
            return true;
        }

        //adding product to the cart using the loggedin user's userID
        public bool AddProduct( ProductDetailDTO pdto,Guid user_id)
        {
            Product product = dbContext.Product.Where(a => a.Name == pdto.Name).FirstOrDefault();
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            Variant variant = dbContext.Variant.Where(s => s.ProductID == product.ID).FirstOrDefault();
            ProductDetailDTO cartdto = new ProductDetailDTO();
            IEnumerable<Cart> ca=dbContext.Cart.Where(pd=>pd.UserID==user_id);
            foreach(var cd in ca)
            {
                if (cd.VariantID == variant.ID)
                {
                    return false;
                }
            }
           
                Cart cart = new Cart();
                cart.ID = Guid.NewGuid();
                cart.VariantID = variant.ID;
                cart.SellingPrice = variant.Discount;
                cart.Qty = 2;
                cart.UserID = user_id;
                cartdto.Name = product.Name;
                cartdto.ImageURL = pdto.ImageURL;
                dbContext.Cart.Add(cart);
                dbContext.SaveChanges();
            //return cartdto;
            return true;
           
            
                  
        }


        //search the product using the name or description of the product
       public ProductSearchResultDTO GetProductSearch(string searchString)
        {
            ProductSearchResultDTO newProductsSearchResultDTO = new ProductSearchResultDTO();
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
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
