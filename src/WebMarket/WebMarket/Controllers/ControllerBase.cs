using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using WebMarket.Common;
using WebMarket.Models;
using WebMarket.Repository.Core;
using log4net;

namespace WebMarket.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly ILog Logger;

        public ControllerBase()
        {
            Logger = LogManager.GetLogger(GetType());
        }

        public Cart Cart { get; private set; }

        [Dependency]
        public IUnitOfWork UnitOfWork { get; set; }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            HttpRequestBase request = filterContext.RequestContext.HttpContext.Request;
            ILog logger = LogManager.GetLogger(request.Url.ToString());
            logger.Info(string.Format("{0} {1} {2} {3}", request.UserHostAddress, request.UserHostName,
                                      request.UserHostAddress, request.UserAgent));
            if (filterContext.Exception != null)
            {
                logger.Error(filterContext.Exception);
                Response.Write(filterContext.Exception.ToString());
            }

            base.OnActionExecuted(filterContext);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            var cart = (Cart) ControllerContext.HttpContext.Session[Constants.CartKey];
            if (cart == null)
            {
                cart = new Cart();
                ControllerContext.HttpContext.Session[Constants.CartKey] = cart;
            }

            Cart = cart;
            RefreshCart();
        }

        private void RefreshCart()
        {
            ViewData["CartTotalCount"] = Cart.TotalItems;
            ViewData["CartTotalSum"] = Cart.TotalPrice;
        }

        public void RemoveCart()
        {
            ControllerContext.HttpContext.Session[Constants.CartKey] = null;
        }
    }
}