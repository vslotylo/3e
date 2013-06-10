using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.DAL.Common;
using WebMarket.Models;
using log4net;

namespace WebMarket.Controllers
{
    public class ControllerBase : Controller
    {
        private readonly WebMarketDbContext dbContext = new WebMarketDbContext();
        protected ILog logger;

        public ControllerBase()
        {
            this.logger = LogManager.GetLogger(this.GetType());
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

        public WebMarketDbContext DbContext
        {
            get
            {
                return this.dbContext;
            }
        }

        public Cart Cart
        {
            get;
            private set;
        }
    }
}
