using AutoMapper;
using FuegoBox.Business.BusinessObjects;
using FuegoBox.Presentation.Models;
using FuegoBox.Shared.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FuegoBox.Presentation.Controllers
{
    public class OrderController : Controller
    {
        
            OrderContext oc = new OrderContext();
            IMapper OrderMapper, omapper;
            public OrderController()
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<AddressModel, AddressDTO>();
                });
            var conf = new MapperConfiguration(cfg => {
                cfg.CreateMap<ViewOrderDTO, ViewOrderModel>();
            });

            OrderMapper = new Mapper(config);
            omapper = new Mapper(conf);

            }
            public ActionResult CheckOut()
            {

                Guid userid = new Guid(Session["UserID"].ToString());
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]

            public ActionResult PlaceOrder([Bind(Include = "Address1, Address2, Pin")] AddressModel am)
            {

                Guid userid = new Guid(Session["UserID"].ToString());

                AddressDTO odto = new AddressDTO();
                odto = OrderMapper.Map<AddressModel, AddressDTO>(am);
                bool abc = oc.AddAddress(odto, userid);
                return View("PlaceOrder");
            }

        public ActionResult ViewOrderItem()
        {
            ViewOrderModel vom = new ViewOrderModel();
            ViewOrderDTO vodto = new ViewOrderDTO();
            Guid userId = new Guid(Session["UserID"].ToString());
            vodto = oc.viewOrder(userId);
            vom = omapper.Map<ViewOrderDTO, ViewOrderModel>(vodto);
            return View(vom);
        }
    }
}