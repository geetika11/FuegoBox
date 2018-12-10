using AutoMapper;
using FuegoBox.Shared.DTO.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.DAL.DBObjects
{
    public class CartDatabaseObject
    {
        FuegoEntities dbContext;
        IMapper cart_DTOmapper;
        public CartDatabaseObject()
        {
            dbContext = new FuegoEntities();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cart, ViewCartDTO>();
            });
            cart_DTOmapper = new Mapper(config);
        }

        public ViewCartDTO viewCart(Guid userID)
        {
            IEnumerable<Cart> cart = dbContext.Cart.Where(cdd=>cdd.UserID==userID);
            ViewCartDTO viewcdto = new ViewCartDTO();
            viewcdto.CartProduct = (from p in dbContext.Cart.Where(cdd => cdd.UserID == userID)
                                    select new CartProductsDTO()
                                    {
                                        SellingPrice = p.SellingPrice,
                                        ID=p.ID,
                                        Variant_ID=p.VariantID                                    
                                    });
            return viewcdto;
        }

        public void RemoveItem(Guid UserID, Guid VariantID)
        {
            dbContext.Cart.RemoveRange(dbContext.Cart.Where(c => c.UserID == UserID && c.VariantID == VariantID));
            dbContext.SaveChanges();
            return;
        }
    }
}
