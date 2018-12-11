﻿using AutoMapper;
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
   public class CategoryProductDB
    {
        FuegoEntities dbContext;
      
        public CategoryProductDB()
        {
            dbContext = new FuegoEntities();
        }

        //Getting all the products of the particular category through category name
        public CategoryDTO Getproduct(string categoryName)
        {
            System.Guid idvalue;
            Category category = dbContext.Category.Where(c => c.Name == categoryName).FirstOrDefault();
            idvalue = category.ID;
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO.Name = categoryName;
            categoryDTO.Products = (from pi in dbContext.Product.Where(c=>c.CategoryID==idvalue)
                                         join v in dbContext.Variant on pi.ID equals v.ProductID
                                         join img in dbContext.VariantImage on v.ID equals img.VariantID
                                         select new ProductDetailDTO() {
                                             ImageURL = img.ImageURL,
                                             Name=pi.Name
                                         }).ToList();
        
         return categoryDTO;
        }

        //displaying top 3 selling products from 3 top selling categories
        public CategoryDTO GetCategoryonHomePage()
        {
            CategoryDTO cd = new CategoryDTO();
            var i = 1;
            List<List<ProductDetailDTO>> productlist1 = new List<List<ProductDetailDTO>>();
            var categories = dbContext.Category.Include(abc => abc.Product).OrderByDescending(cdd => cdd.ProductsSold).ToList();
            foreach (Category cato in categories)
            {
                if (i <= 3)
                {
                    cd.Products = (from pi in dbContext.Product
                                   where pi.CategoryID == cato.ID
                                   join v in dbContext.Variant on pi.ID equals v.ProductID
                                   join img in dbContext.VariantImage on v.ID equals img.VariantID
                                   orderby v.QuantitySold descending
                                   select new ProductDetailDTO()
                                   {
                                       Name = pi.Name,
                                       CatName = cato.Name,
                                       ListingPrice = v.ListingPrice,
                                       Discount = v.Discount,
                                       ImageURL = img.ImageURL
                                   }).ToList().Take(3);
                    productlist1.Add(cd.Products.ToList());
                }
                else
                {
                    cd.Products = (from pi in dbContext.Product
                                   where pi.CategoryID == cato.ID
                                   join v in dbContext.Variant on pi.ID equals v.ProductID
                                   join img in dbContext.VariantImage on v.ID equals img.VariantID
                                   select new ProductDetailDTO()
                                   {
                                       Name = pi.Name,
                                       CatName = cato.Name,
                                       ListingPrice = v.ListingPrice,
                                       Discount = v.Discount,
                                       ImageURL = img.ImageURL
                                   }).ToList().Take(5);
                    productlist1.Add(cd.Products.ToList());
                }
                i++;
            }
            List<ProductDetailDTO> productList2 = new List<ProductDetailDTO>();
            foreach(var iter1 in productlist1)
            {
                foreach(var iter2 in iter1)
                {
                    productList2.Add(iter2);
                }
            }
            cd.Products = productList2;
            return cd;
        }

        //to check whether the category exists or not..
        public bool CategoryExists(string CategoryName)
        {
            Category category = dbContext.Category.Where(c => c.Name == CategoryName).FirstOrDefault();
            if (category == null)
            {
                throw new NotFoundException();
            }
            return true;
        }

    }
}
