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
            IMapper OrderMapper, omapper, orderMapper;
            public OrderController()
            {
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<AddressModel, AddressDTO>();
                });
            var conf = new MapperConfiguration(cfg => {
                cfg.CreateMap<ViewOrderDTO, ViewOrderModel>();
            });

            var config2 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrdersDTO, OrdersModel>();
            });
            OrderMapper = new Mapper(config);
            omapper = new Mapper(conf);

            orderMapper = new Mapper(config2);
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

        public ActionResult ViewOrder()
        {
            OrdersModel ordersModel = new OrdersModel();
            OrdersDTO ordersDTO = new OrdersDTO();
            Guid userId = new Guid(Session["UserID"].ToString());
            ordersDTO = oc.GetOrders(userId);
            ordersModel = orderMapper.Map<OrdersDTO, OrdersModel>(ordersDTO);
            return View(ordersModel);
        }

        public ActionResult ViewOrderItem([Bind(Include = ("ID"))] OrderModel orderModel)
        {
            ViewOrderModel vom = new ViewOrderModel();
            ViewOrderDTO vodto = new ViewOrderDTO();
            // Guid userId = new Guid(Session["UserID"].ToString());
            //vodto = oc.viewOrder(userId);
            vodto = oc.viewOrder(orderModel.ID);
            vom = omapper.Map<ViewOrderDTO, ViewOrderModel>(vodto);
            return View(vom);
        }
    }
}