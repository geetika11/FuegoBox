﻿using FuegoBox.DAL.DBObjects;
using FuegoBox.Shared.DTO.Cart;
using FuegoBox.Shared.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business.BusinessObjects
{
    public class OrderContext
    {
        OrderDBObject orderDBObject;
        CartDatabaseObject cartdatabaseobject;

        public OrderContext()
        {
            orderDBObject = new OrderDBObject();
            cartdatabaseobject = new CartDatabaseObject();
        }

        public bool AddAddress(AddressDTO od, Guid userid)
        {
            Guid addressid = orderDBObject.AddAddress(od, userid);
            ViewCartDTO vdto = cartdatabaseobject.viewCart(userid);
            CartDetailContext cdc = new CartDetailContext();
            double result = new double();
            foreach (var i in vdto.CartProduct)
            {
                result = result + i.SellingPrice;

            }
            vdto.Total = result;
            orderDBObject.PlaceOrder(userid, vdto, addressid);
            cartdatabaseobject.EmptyCart(userid);
            return true;
        }

        public ViewOrderDTO viewOrder(Guid order_id)
        {
            ViewOrderDTO vdto = orderDBObject.ViewOrder(order_id);
            return vdto;
        }
         
        
        public OrdersDTO GetOrders(Guid userId)
        {
            OrdersDTO ordersDTO = orderDBObject.GetOrders(userId);
            return ordersDTO;
        }
    }
}