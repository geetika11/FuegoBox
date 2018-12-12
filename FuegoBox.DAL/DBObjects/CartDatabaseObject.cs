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
        public CartDatabaseObject()
        {
            dbContext = new FuegoEntities();            
        }
        
        //function to display all the cart items according to the user...currently logged in ID of the User is userID
        public ViewCartDTO viewCart(Guid userID)
        {         
            ViewCartDTO viewcdto = new ViewCartDTO();
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            viewcdto.CartProduct = (from p in dbContext.Cart.Where(cdd => cdd.UserID == userID)
                                    join vari in dbContext.Variant on p.VariantID equals vari.ID
                                    join im in dbContext.VariantImage on vari.ID equals im.VariantID
                                    join prod in dbContext.Product on vari.ProductID equals prod.ID
                                    select new CartProductsDTO()
                                    {
                                        SellingPrice = p.SellingPrice,
                                        ID = p.ID,
                                        Name = prod.Name,
                                        Url = im.ImageURL,
                                        Variant_ID = p.VariantID
                                    });
            return viewcdto;
        }


        public void EmptyCart(Guid UserID)
        {
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            dbContext.Cart.RemoveRange(dbContext.Cart.Where(c => c.UserID == UserID));
            dbContext.SaveChanges();
            return;
        }


        //function to remove the item from the cart according to the logged in user and variant id...
        public void RemoveItem(Guid UserID, Guid VariantID)
        {
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            dbContext.Cart.RemoveRange(dbContext.Cart.Where(c => c.UserID == UserID && c.VariantID == VariantID));
            dbContext.SaveChanges();
            return;
        }
    }
}
