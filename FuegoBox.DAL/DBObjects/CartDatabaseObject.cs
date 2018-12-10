﻿using AutoMapper;
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
            viewcdto.CartProduct = (from p in dbContext.Cart.Where(cdd => cdd.UserID == userID)
                                    select new CartProductsDTO()
                                    {
                                        SellingPrice = p.SellingPrice,
                                        ID=p.ID,
                                        Variant_ID=p.VariantID                                    
                                    });
            return viewcdto;
        }


        //function to remove the item from the cart according to the logged in user and variant id...
        public void RemoveItem(Guid UserID, Guid VariantID)
        {
            dbContext.Cart.RemoveRange(dbContext.Cart.Where(c => c.UserID == UserID && c.VariantID == VariantID));
            dbContext.SaveChanges();
            return;
        }
    }
}