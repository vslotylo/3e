using System.Linq;
using System.Web.Mvc;
using WebMarket.DAL.Interfaces;
using WebMarket.DAL.Infrustructure;
using WebMarket.Models;

namespace WebMarket.Controllers
{
    public class CartController : ControllerBase
    {
        private readonly IProductManager manager = new ProductManager();

        [HttpGet]
        public ActionResult Index()
        {
            return this.View(this.Cart);
        }

        [HttpPost]
        public JsonResult Add(string pid, int quantity)
        {
            var product = this.manager.GetProductByPid(pid);
            var cartItem = this.Cart.Items.FirstOrDefault(obj => obj.Product.ProductId == pid);
            if( cartItem == null)
            {
                this.Cart.Items.Add(new CartItem { Product = product, Quantity = quantity });
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            //todo
            return this.Json(this.Cart);
        }

        [HttpPost]
        public bool Delete(string pid)
        {
            var product = this.manager.GetProductByPid(pid);
            var cartItem = this.Cart.Items.FirstOrDefault(obj => obj.Product.ProductId == pid);
            if (cartItem != null)
            {
                this.Cart.Items.Remove(cartItem);                
            }

            return true;
        }
    }
}
