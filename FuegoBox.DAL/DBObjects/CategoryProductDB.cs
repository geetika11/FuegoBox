using AutoMapper;
using FuegoBox.Shared.DTO.Category;
using FuegoBox.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.DAL.DBObjects
{
   public class CategoryProductDB
    {

        FuegoEntities dbContext;

        IMapper P_DTOmapper,c_Mapper;
        public CategoryProductDB()
        {
            dbContext = new FuegoEntities();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDetailDTO>();
            });

            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDTO>();
            });



            P_DTOmapper = new Mapper(config);
            c_Mapper = new Mapper(conf);

        }
        public CategoryDTO Getproduct(string catName)
        {
            System.Guid idvalue;
            Category cat = dbContext.Category.Where(c => c.Name == catName).FirstOrDefault();
            idvalue = cat.ID;

            ProductDetailDTO p = new ProductDetailDTO();
            IEnumerable<Product> product = dbContext.Product.Where(a => a.CategoryID == idvalue);
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO.Products = P_DTOmapper.Map<IEnumerable<Product>, IEnumerable<ProductDetailDTO>>(product);
            categoryDTO.Name = "Books";
            return categoryDTO;

           
        }

    }
}
