using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.DAL.Entities;
using WebMarket.DAL.Entities.Enums;
using System.Data.Entity;

namespace WebMarket.Controllers
{
    public class OrderController : ControllerBase
    {
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
            return this.View(order);
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
                if (order.Status == OrderStatus.Completed || order.Status == OrderStatus.Refunded)
                {
                    dbOrder.CloseDate = DateTime.Now;
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
                order.Items.Add(new OrderItem { Pid = item.Product.ProductId, Quantity = item.Quantity, TotalItemPrice = item.TotalItemPrice, UnitPrice = item.Product.PriceUah });
            }

            order.Status = OrderStatus.Pending;
            order.CreationDate = DateTime.UtcNow;
            this.DbContext.Orders.Add(order);
            try
            {
                this.DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                //todo
            }

            this.RemoveCart();
            return true;
        }
    }
}
