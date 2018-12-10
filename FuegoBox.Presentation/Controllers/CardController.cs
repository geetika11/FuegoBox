using AutoMapper;
using FuegoBox.Business.BusinessObjects;
using FuegoBox.DAL;
using FuegoBox.DAL.DBObjects;
using FuegoBox.Presentation.ActionFilters;
using FuegoBox.Presentation.Models;
using FuegoBox.Shared.DTO.Cart;
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

        IMapper productmapper,cartMapper;
       
        ProductDetailContext productDetailContext;
        ProductDetailDB pdb;
        CartDetailContext cdc;
     
        public CardController()
        {

            productDetailContext = new ProductDetailContext();
            cdc = new CartDetailContext();
            pdb = new ProductDetailDB();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductDetail, ProductDetailDTO>();
            });
            var conf = new MapperConfiguration(cfg => {
                cfg.CreateMap<ViewCartDTO, ViewCartModel>();
            });

            productmapper = new Mapper(config);

            cartMapper = new Mapper(conf);
        }

        [UserAuthenticationFilter]
        public ActionResult CardDetail([Bind(Include = "Name")]ProductDetail productDetail)
        {
            ProductDetailDTO productDetailDTO = productmapper.Map<ProductDetail, ProductDetailDTO>(productDetail);
            Guid user_id = new Guid(Session["UserID"].ToString());
            ProductDetailDTO prodDetailDTO = productDetailContext.productAddToCart(productDetailDTO,user_id);           
            ProductDetail p = productmapper.Map<ProductDetailDTO, ProductDetail>(prodDetailDTO);            
            return View(p);
        }

        public ActionResult ViewCart()
        {
            ViewCartModel vcm = new ViewCartModel();
            ViewCartDTO vcdto = new ViewCartDTO();
            Guid userId = new Guid(Session["UserID"].ToString());
            vcdto = cdc.viewCart(userId);

            vcm = cartMapper.Map<ViewCartDTO, ViewCartModel>(vcdto);
            return View(vcm);
        }

        public ActionResult RemoveItem(Guid VariantID)
        {
            cdc.RemoveItem(new Guid(Session["UserID"].ToString()), VariantID);
            return RedirectToAction("ViewCart");
        }

    }
}