using FuegoBox.DAL.DBObjects;
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
        CartDatabaseObject cdo;

        public OrderContext()
        {
            orderDBObject = new OrderDBObject();
            cdo = new CartDatabaseObject();
        }

        public bool AddAddress(AddressDTO od, Guid userid)
        {
            Guid addressid = orderDBObject.AddAddress(od, userid);
            ViewCartDTO vdto = cdo.viewCart(userid);
            double result = new double();
            foreach (var i in vdto.CartProduct)
            {
                result = result + i.SellingPrice;

            }
            vdto.Total = result;
            orderDBObject.PlaceOrder(userid, vdto, addressid);
            return true;
        }

        public ViewOrderDTO viewOrder(Guid userid)
        {
            ViewOrderDTO vdto = orderDBObject.ViewOrder(userid);
            return vdto;
        }
    }
}