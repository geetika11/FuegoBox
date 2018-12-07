﻿
using AutoMapper;
using FuegoBox.Business.BusinessObjects;
using FuegoBox.Presentation.Models;
using FuegoBox.Shared.DTO.Category;
using FuegoBox.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FuegoBox.Presentation.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        IMapper productmapper;
        IMapper catMapper, ProductsSearchResultVMMapper;
        ProductDetailContext productDetailContext;
        CategoryDetailContext categoryDetailContext;

        public CategoryController()
        {

            productDetailContext = new ProductDetailContext();
            categoryDetailContext = new CategoryDetailContext();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductDetail, ProductDetailDTO>();
            });

            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryDTO, CategoryModel>();
            });

            var conf1 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductSearchResultDTO, ProductsSearchViewModel>();
            });

            productmapper = new Mapper(config);
            catMapper = new Mapper(conf);
            ProductsSearchResultVMMapper = new Mapper(conf1);

        }

        public ActionResult SearchResult(string searchString)
        {
            
            if (Session["UserID"] != null)
            {
                ViewBag.IsLoggedIn = "True";
            }

            if (String.IsNullOrEmpty(searchString))
            {
               
                return View("Search");//TODO
            }
            try
            {
                ProductSearchResultDTO newProductsSearchResultDTO = new ProductSearchResultDTO();
                ProductsSearchViewModel viewModel = new ProductsSearchViewModel();
                CategoryDTO cd = new CategoryDTO();

                newProductsSearchResultDTO = productDetailContext.GetProductwithString(searchString);
                viewModel = ProductsSearchResultVMMapper.Map<ProductSearchResultDTO, ProductsSearchViewModel>(newProductsSearchResultDTO);
                ViewBag.searchString = searchString;
                return View(viewModel);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex + ":Exception occured");
            }
            return View();
        }



        public ActionResult PDetail([Bind(Include = "Name")] ProductDetail productDetail)
        {
            try
            {
                ProductDetailDTO productDetailDTO = productmapper.Map<ProductDetail, ProductDetailDTO>(productDetail);
                ProductDetailDTO prodDetailDTO = productDetailContext.GetProductDetail(productDetailDTO);
                ProductDetail p = productmapper.Map<ProductDetailDTO, ProductDetail>(prodDetailDTO);
                return View(p);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex + ":Exception occured");
            }
            return View();
        }
      

       public ActionResult ViewProductCategory(string CategoryName)
        {
            
            CategoryModel categorymodel = new CategoryModel();
            CategoryDTO cdto = new CategoryDTO();
            cdto = categoryDetailContext.GetCategoryProduct(CategoryName);
            categorymodel = catMapper.Map<CategoryDTO, CategoryModel>(cdto);
            return View(categorymodel);
        }



    }
}