using FuegoBox.DAL.DBObjects;
using FuegoBox.Shared.DTO.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuegoBox.Business.BusinessObjects
{
  public  class CartDetailContext
    {
        CartDatabaseObject cartdatabaseobject = new CartDatabaseObject();

        //function to view the cart according to the user...
        public ViewCartDTO viewCart(Guid userId)
        {
            ViewCartDTO viewcartdto = cartdatabaseobject.viewCart(userId);
            double result = new double();
            foreach (var i in viewcartdto.CartProduct)
            {
                result = result + i.SellingPrice;

            }
            viewcartdto.Total = result;
            return viewcartdto;
        }


        //function to remove the cart items.
        public bool EmptyCart(Guid UserID)
        {
            cartdatabaseobject.EmptyCart(UserID);
            return true;
        }

        //function to remove the item from the cart....
        public void RemoveItem(Guid UserID, Guid VariantID)
        {
            cartdatabaseobject.RemoveItem(UserID, VariantID);
        }
    }
}
