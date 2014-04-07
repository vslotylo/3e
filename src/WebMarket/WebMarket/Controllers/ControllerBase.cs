using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.Models;
using log4net;

namespace WebMarket.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly ILog Logger;

        public ControllerBase()
        {
            this.Logger = LogManager.GetLogger(this.GetType());
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;
            var logger = LogManager.GetLogger(request.Url.ToString());
            logger.Info(string.Format("{0} {1} {2} {3}", request.UserHostAddress, request.UserHostName, request.UserHostAddress, request.UserAgent));
            base.OnActionExecuted(filterContext);
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            var cart = (Cart)this.ControllerContext.HttpContext.Session[Constants.CartKey];
            if (cart == null)
            {
                cart = new Cart();
                this.ControllerContext.HttpContext.Session[Constants.CartKey] = cart;
            }

            this.Cart = cart;
            this.RefreshCart();
        }

        private void RefreshCart()
        {
            this.ViewData["CartTotalCount"] = this.Cart.TotalItems;
            this.ViewData["CartTotalSum"] = this.Cart.TotalPrice;
        }

        public void RemoveCart()
        {
            this.ControllerContext.HttpContext.Session[Constants.CartKey] = null;
        }

        public Cart Cart
        {
            get;
            private set;
        }
    }
}
