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
        CartDatabaseObject cdo = new CartDatabaseObject();

        //function to view the cart according to the user...
        public ViewCartDTO viewCart(Guid userId)
        {
            ViewCartDTO vdto = cdo.viewCart(userId);
            double result = new double();
            foreach (var i in vdto.CartProduct)
            {
                result = result + i.SellingPrice;

            }
            vdto.Total = result;
            return vdto;
        }


        //function to remove the item from the cart....
        public void RemoveItem(Guid UserID, Guid VariantID)
        {
            cdo.RemoveItem(UserID, VariantID);
        }
    }
}
