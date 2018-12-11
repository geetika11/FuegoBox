using AutoMapper;
using FuegoBox.Business.BusinessObjects;
using FuegoBox.Business.Exceptions;
using FuegoBox.Presentation.ActionFilters;
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
               
                return View();//TODO
            }
            try
            {
                ProductSearchResultDTO newProductsSearchResultDTO = new ProductSearchResultDTO();
                ProductsSearchViewModel viewModel = new ProductsSearchViewModel();
                newProductsSearchResultDTO = productDetailContext.GetProductwithString(searchString);
                if (newProductsSearchResultDTO.Products.Count()==0)
                {
                    return View("NoSearch");
                }

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


        [UserAuthenticationFilter]
        public ActionResult PDetail([Bind(Include = "Name")]ProductDetail productDetail)
        {
            ProductDetailDTO prodDetailDTO = new ProductDetailDTO();
            try
            {
                ProductDetailDTO productDetailDTO = productmapper.Map<ProductDetail, ProductDetailDTO>(productDetail);
                prodDetailDTO = productDetailContext.GetProductDetail(productDetailDTO);
               
            }
            catch (CategoryDoesNotExists ex)
            {
                return View("Error" + ex);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex + ":Exception occured");
            }
            ProductDetail p = productmapper.Map<ProductDetailDTO, ProductDetail>(prodDetailDTO);
            return View(p);
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