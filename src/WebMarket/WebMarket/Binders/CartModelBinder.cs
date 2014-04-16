using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.Models;

namespace WebMarket.Binders
{
    public class CartModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var cart = (Cart)controllerContext.HttpContext.Session[Constants.CartKey];
            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[Constants.CartKey] = cart;
            }
            
            return cart;
        }

    }
}
