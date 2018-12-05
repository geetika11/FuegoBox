using AutoMapper;
using FuegoBox.DAL.Exceptions;
using FuegoBox.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.DAL.DBObjects
{
   public class ProductDetailDB
    {
        FuegoEntities dbContext;
 
        IMapper P_DTOmapper;
        public ProductDetailDB()
        {
            dbContext = new FuegoEntities();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDetailDTO>();
            });

            P_DTOmapper = new Mapper(config);

        }
   public ProductDetailDTO GetDetail(ProductDetailDTO productDetailDTO)
        {
            Product product = dbContext.Product.Where(a => a.Name == productDetailDTO.Name).FirstOrDefault();
            if (product!= null)
            {
                ProductDetailDTO newuserBasicDTO = P_DTOmapper.Map<Product, ProductDetailDTO>(product);
                return newuserBasicDTO;
            }
            return null;
        }

    }
}
