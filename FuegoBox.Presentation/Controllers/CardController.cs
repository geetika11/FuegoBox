using AutoMapper;
using FuegoBox.Business.BusinessObjects;
using FuegoBox.DAL;
using FuegoBox.DAL.DBObjects;
using FuegoBox.Presentation.Models;
using FuegoBox.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FuegoBox.Presentation.Controllers
{
    public class CardController : Controller
    {
        // GET: Card

        IMapper productmapper, cardMapper;
       
        ProductDetailContext productDetailContext;
        ProductDetailDB pdb;
     
        public CardController()
        {

            productDetailContext = new ProductDetailContext();

            pdb = new ProductDetailDB();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductDetail, ProductDetailDTO>();
            });

            var confi = new MapperConfiguration(cfg => {
                cfg.CreateMap<CardDTO, CardModel>();
            });

            productmapper = new Mapper(config);
            cardMapper = new Mapper(confi);
            

        }
        public ActionResult CardDetail([Bind(Include = "Name")]ProductDetail productDetail)

        {
            ProductDetailDTO productDetailDTO = productmapper.Map<ProductDetail, ProductDetailDTO>(productDetail);
            CardDTO prodDTO = productDetailContext.productAddToCart(productDetailDTO);
            CardModel p = cardMapper.Map<CardDTO, CardModel>(prodDTO);
            return View(p);

          
        }

        //public ActionResult CardDetail()
        //{
        //    List<Cart> abc= pdb.GetCardItems();
        //    return View();

           
        //}
        
    }
}