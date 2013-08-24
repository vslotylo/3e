using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.DAL.Entities;
using WebMarket.DAL.Entities.Enums;
using System.Data.Entity;
using WebMarket.DAL.Infrustructure;
using WebMarket.DAL.Interfaces;
using WebMarket.Notification;
using WebMarket.ViewModels;

namespace WebMarket.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IProductManager manager = new ProductManager();

        public ActionResult Index()
        {
            return this.View(this.Cart);
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult List()
        {
            return this.View(DbContext.Orders.ToList());
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Details(int id)
        {
            var order = DbContext.Orders.Include(obj => obj.Items).FirstOrDefault(obj => obj.Id == id);
            var products = order.Items.Select(obj => manager.GetProductByPid(obj.ProductId));
            return this.View(new OrderDetailViewModel(order, products));
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(int id)
        {
            var order = DbContext.Orders.Include(obj => obj.Items).FirstOrDefault(obj => obj.Id == id);
            return this.View(order);
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(Order order)
        {
            if (this.ModelState.IsValid)
            {
                var dbOrder = this.DbContext.Orders.Find(order.Id);
                this.DbContext.Entry(dbOrder).State = EntityState.Modified;
                dbOrder.Status = order.Status;
                if (order.Status == Status.Completed || order.Status == Status.Refunded)
                {
                    dbOrder.CloseDate = DateTime.UtcNow.ToUkrainianTimeZone();
                }

                this.DbContext.SaveChanges();
                return this.RedirectToAction("list");
            }

            return this.View(order);
        }

        [HttpPost]
        public bool Add(Order order)
        {
            foreach (var item in this.Cart.Items)
            {
                order.Items.Add(new OrderItem { ProductId = item.Product.Id, Quantity = item.Quantity, UnitPrice = item.Product.CalculatedPrice.PriceFinalUah });
            }

            order.Status = Status.Pending;
            order.CreationDate = DateTime.Now.ToUkrainianTimeZone();;
            this.DbContext.Orders.Add(order);
            try
            {
                Task.Factory.StartNew(() =>
                {
                    //Razor.
                    var message = new NotificationMessage { Subject = "Нове замовлення!", To = new[] { order.Email }, Body = "Дякуємо за замовлення!" };
                    NotificationManager.Current.Notify(message);
                });
                
                this.DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                this.Logger.Error(e);
            }

            this.RemoveCart();
            return true;
        }
    }
}
