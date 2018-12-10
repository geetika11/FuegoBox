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
        public ViewCartDTO viewCart(Guid userId)
        {
           
            ViewCartDTO vdto = cdo.viewCart(userId);
            return vdto;
        }
        public void RemoveItem(Guid UserID, Guid VariantID)
        {
            cdo.RemoveItem(UserID, VariantID);
        }
    }
}
