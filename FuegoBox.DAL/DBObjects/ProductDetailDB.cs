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
        IMapper ProductSearchMapper;
        IMapper P_DTOmapper, v_DTOmapper;
        public ProductDetailDB()
        {
            dbContext = new FuegoEntities();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDetailDTO>();
            });
            var productsSearchDTOConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDetailDTO>();
               
            });

            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Variant, VariantDTO>();
            });
           
            ProductSearchMapper = new Mapper(productsSearchDTOConfig);
            v_DTOmapper = new Mapper(conf);
            P_DTOmapper = new Mapper(config);
          
        }
   public ProductDetailDTO GetDetail(ProductDetailDTO productDetailDTO)
        {
            Product product = dbContext.Product.Where(a => a.Name == productDetailDTO.Name).FirstOrDefault();
           // Category cat = dbContext.Category.Where(f => f.ID == product.CategoryID).FirstOrDefault();            
             IEnumerable< Variant> variant = dbContext.Variant.Where(s => s.ProductID == product.ID);
              

            if (product!= null)
            {
               // VariantDTO vdto= v_DTOmapper.Map< Variant, VariantDTO>(variant);
                ProductDetailDTO newBasicDTO = P_DTOmapper.Map<Product, ProductDetailDTO>(product);

               // newBasicDTO.Variants = v_DTOmapper.Map<IEnumerable<Variant>, IEnumerable<VariantDTO>>(product.Variant);
                //  newBasicDTO.ListingPrice = vdto.ListingPrice;
                newBasicDTO.Name = product.Name;
                // newBasicDTO.CatName=cat.Name;
                //  newBasicDTO.Discount = vdto.Discount;
                // newBasicDTO.ImageURL = vi.ImageURL;
                newBasicDTO.Variants = (from v in dbContext.Variant.Where(cdf => cdf.ProductID == product.ID)
                                        join vp in dbContext.VariantProperty on v.ID equals vp.VariantID
                                        join vpv in dbContext.VariantPropertyValue on vp.PropertyValueID equals vpv.ID
                                        join p in dbContext.Property on vpv.PropertyID equals p.ID 
                                       
                                        select new VariantDTO()
                                        {
                                            ListingPrice=v.ListingPrice,
                                            Discount=v.Discount,
                                            Variant_Property=p.Name,
                                           

                                        });


                

                return newBasicDTO;
            }
            return null;
        }


        public ProductDetailDTO AddProduct( ProductDetailDTO pdto)
        {
            
            Product product = dbContext.Product.Where(a => a.Name == pdto.Name).FirstOrDefault();
            Variant variant = dbContext.Variant.Where(s => s.ProductID == product.ID).FirstOrDefault();
            ProductDetailDTO cartdto = new ProductDetailDTO();
            Cart cart = new Cart();
           
            cart.ID = Guid.NewGuid();
            cart.VariantID = variant.ID;
            cart.SellingPrice = variant.Discount;
            cart.Qty = 2;
            
            cartdto.Name = product.Name;        
            dbContext.Cart.Add(cart);
            dbContext.SaveChanges();
            return cartdto;           
        }

       public ProductSearchResultDTO GetProductSearch(string searchString)
        {
            IEnumerable<Product> searchResults = dbContext.Product.Where(p => p.Name.Contains(searchString));
            ProductSearchResultDTO newProductsSearchResultDTO = new ProductSearchResultDTO();
           
            newProductsSearchResultDTO.Products= (from pi in dbContext.Product.Where(p => p.Name.Contains(searchString))
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
