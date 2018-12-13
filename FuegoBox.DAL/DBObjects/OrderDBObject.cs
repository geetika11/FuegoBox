using AutoMapper;
using FuegoBox.Shared.DTO.Cart;
using FuegoBox.Shared.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.DAL.DBObjects
{
    public class OrderDBObject
    {
        FuegoEntities dbContext;

        CategoryProductDB categorydb = new CategoryProductDB();
        IMapper orderMapper, orderdtoMapper;
        public OrderDBObject()
        {
           
            dbContext = new FuegoEntities();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Address, AddressDTO>();
            });
            var conf = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddressDTO, Address>();
            });

            orderMapper = new Mapper(config);
            orderdtoMapper = new Mapper(conf);

        }

        public Guid AddAddress(AddressDTO addDTO, Guid userid)
        {
            Address address = new Address();
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            address.AddressLine1 = addDTO.Address1;
            address.AddressLine2 = addDTO.Address2;
            address.Pin = addDTO.Pin;
            address.ID = Guid.NewGuid();
            address.UserID = userid;
            dbContext.Address.Add(address);
            dbContext.SaveChanges();
            return address.ID;


        }

        public void PlaceOrder(Guid userid, ViewCartDTO vcdto, Guid addressid)
        {

            Order order = new Order();
          
                
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            order.ID = Guid.NewGuid();

            categorydb.update(vcdto);
           
           // var ab = cat.ProductsSold;
            order.DeliveryAddressID = addressid;
            order.TotalAmount = vcdto.Total;
            order.UserID = userid;
            order.OrderDate = DateTime.Now;
            order.DeliveryDate = DateTime.Now.AddDays(2);
            order.isCancelled = "N";
           
            dbContext.Order.Add(order);
           
            dbContext.SaveChanges();
            categorydb.addOrderProduct(order, vcdto);


        }




        public ViewOrderDTO ViewOrder(Guid orderID)
        {
            ViewOrderDTO viewcdto = new ViewOrderDTO();
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            viewcdto.OrderItems = (from or in dbContext.Order.Where(cdd => cdd.ID == orderID)
                                   join op in dbContext.OrderProduct on or.ID equals op.OrderID
                                   join vari in dbContext.Variant on op.VariantID equals vari.ID
                                   join p in dbContext.Product on vari.ProductID equals p.ID
                                   join img in dbContext.VariantImage on vari.ID equals img.VariantID
                                   orderby or.OrderDate descending

                                   select new OrderItemsDTO()
                                   {
                                       Price = vari.Discount,
                                       Url = img.ImageURL,
                                       OrderDate = or.OrderDate,
                                       Name = p.Name
                                   }).ToList();

            return viewcdto;
        }

        public OrdersDTO GetOrders(Guid user_Id)
        {
            OrdersDTO ordersDTO = new OrdersDTO();
            ordersDTO.orders = (from oc in dbContext.Order
                                where oc.UserID == user_Id
                                select new OrderDTO()
                                {
                                    TotalAmount = oc.TotalAmount,
                                    OrderDate = oc.OrderDate,
                                    DeliveryDate = oc.DeliveryDate,
                                    ID = oc.ID

                                }).ToList();
            foreach (var i in ordersDTO.orders)
            {
                if (i.DeliveryDate <= i.OrderDate)
                {
                    i.status = "Delivered";
                }
                else
                {
                    i.status = "Not Delivered";
                }
            }

            return ordersDTO;

        }

    }
}