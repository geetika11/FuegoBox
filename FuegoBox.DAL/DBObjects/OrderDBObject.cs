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


       

        public ViewOrderDTO ViewOrder(Guid userid)
        {

            ViewOrderDTO viewcdto = new ViewOrderDTO();
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            //viewcdto.OrderItems = (from or in dbContext.Order.Where(cdd => cdd.UserID == userid)    
            //                       join cart in dbContext.Cart on or.UserID equals cart.UserID
            //                       join vari in dbContext.Variant on cart.VariantID equals vari.ID
            //                       join img in dbContext.VariantImage on vari.ID equals img.VariantID
            //                       join p in dbContext.Product on vari.ProductID equals p.ID
            //                       select new OrderItemsDTO()
            //                       {    
            //                           OrderDate=or.OrderDate,  
            //                           Name=p.Name,
            //                           Price=vari.Discount,
            //                           Url=img.ImageURL

            //                       });

           

            viewcdto.OrderItems = (from or in dbContext.Order.Where(cdd=>cdd.UserID==userid) 
                                   join op in dbContext.OrderProduct on or.ID equals op.OrderID
                                   join vari in dbContext.Variant on op.VariantID equals vari.ID
                                   join p in dbContext.Product on vari.ProductID equals p.ID
                                   join img in dbContext.VariantImage on vari.ID equals img.VariantID

                                   select new OrderItemsDTO()
                                   {
                                       Price=vari.Discount,
                                       OrderDate=or.OrderDate,
                                       Name=p.Name,
                                       Url = img.ImageURL



                                   }).ToList();
            return viewcdto;
        }

    }
}