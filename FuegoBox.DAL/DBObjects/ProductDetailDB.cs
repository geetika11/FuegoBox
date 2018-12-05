using AutoMapper;
using FuegoBox.DAL.Exceptions;
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

        IMapper P_DTOmapper, v_DTOmapper;
        public ProductDetailDB()
        {
            dbContext = new FuegoEntities();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDetailDTO>();
            });

          

            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Variant, VariantDTO>();
            });

            v_DTOmapper = new Mapper(conf);
            P_DTOmapper = new Mapper(config);
        }
   public ProductDetailDTO GetDetail(ProductDetailDTO productDetailDTO)
        {
            Product product = dbContext.Product.Where(a => a.Name == productDetailDTO.Name).FirstOrDefault();

            Category cat = dbContext.Category.Where(f => f.ID == product.CategoryID).FirstOrDefault();
            
             Variant variant = dbContext.Variant.Where(s => s.ProductID == product.ID).FirstOrDefault();
            VariantImage vi = dbContext.VariantImage.Where(s => s.VariantID == variant.ID).FirstOrDefault();
            if (product!= null)
            {
                VariantDTO vdto= v_DTOmapper.Map< Variant, VariantDTO>(variant);
                ProductDetailDTO newBasicDTO = P_DTOmapper.Map<Product, ProductDetailDTO>(product);
                newBasicDTO.ListingPrice = vdto.ListingPrice;
                newBasicDTO.CatName=cat.Name;
                newBasicDTO.Discount = vdto.Discount;
                newBasicDTO.img = vi.ImageURL;
                return newBasicDTO;
            }
            return null;
        }

    }
}
