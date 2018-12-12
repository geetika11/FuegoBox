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
    public class CategoryDetailContext
    {
        CategoryProductDB categoryDBObject;

        public CategoryDetailContext()
        {
            categoryDBObject = new CategoryProductDB();
        }
        
        //display all the product from the particular category...
        public CategoryDTO GetCategoryProduct(string catName)
        {
            try
            {
                bool exists = categoryDBObject.CategoryExists(catName);
            }
            catch(NotFoundException ex)
            {
                throw new CategoryDoesNotExists();
            }
            CategoryDTO catproductDTO = categoryDBObject.Getproduct(catName);
            return catproductDTO;
        }

        //display product to the home page....
        public CategoryDTO GetCategoryOnHomePage()
        {
            CategoryDTO cdto = categoryDBObject.GetCategoryonHomePage();
            return cdto;
        }
    }
}
