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
      
        public ViewCartDTO viewCart(Guid userId)
        {
            CartDatabaseObject cdo = new CartDatabaseObject();
            ViewCartDTO vdto = cdo.viewCart(userId);
            return vdto;
        }
    }
}
