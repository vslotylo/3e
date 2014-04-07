using System.Linq;
using System.Web.Mvc;
using WebMarket.Models;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Controllers
{
    public class CartController : ControllerBase
    {
        private readonly IProductRepository repository;

        public CartController(IProductRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View(this.Cart);
        }

        [HttpPost]
        public JsonResult Add(int pid, int quantity)
        {
            var product = this.repository.GetWithProducersGroups(pid);
            var cartItem = this.Cart.Items.FirstOrDefault(obj => obj.Product.Id == pid);
            if (cartItem == null)
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
        public bool Delete(int pid)
        {
            var cartItem = this.Cart.Items.FirstOrDefault(obj => obj.Product.Id == pid);
            if (cartItem != null)
            {
                this.Cart.Items.Remove(cartItem);
            }

            return true;
        }
    }
}
