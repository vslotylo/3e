using System.Linq;
using System.Web.Mvc;
using WebMarket.Models;
using WebMarket.Repository.Entities;
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
            return View(Cart);
        }

        [HttpPost]
        public JsonResult Add(int pid, int quantity)
        {
            Product product = repository.GetWithProducersGroups(pid);
            CartItem cartItem = Cart.Items.FirstOrDefault(obj => obj.Product.Id == pid);
            if (cartItem == null)
            {
                Cart.Items.Add(new CartItem {Product = product, Quantity = quantity});
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            //todo
            return Json(Cart);
        }

        [HttpPost]
        public bool Delete(int pid)
        {
            CartItem cartItem = Cart.Items.FirstOrDefault(obj => obj.Product.Id == pid);
            if (cartItem != null)
            {
                Cart.Items.Remove(cartItem);
            }

            return true;
        }
    }
}