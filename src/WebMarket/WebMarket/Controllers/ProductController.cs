using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using WebMarket.DAL.Infrustructure;
using WebMarket.Filters;

namespace WebMarket.Controllers
{
    public class ProductController : ListControllerBase
    {
        [HttpGet]
        public ActionResult Index(string category, PageSizeFilter pageSizeFilter, SortFilter sortFilter, ProducersFilter producerFilter, PageFilter pageFilter, [ModelBinder(typeof(GroupFilterBinder))] GroupFilter groupFilter)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Request.Url.Query))
                {
                    if (this.Request.Url.Query.Contains("type"))
                    {
                        return this.RedirectToAction("index", "error", new { statusCode = 404 });
                    }
                }

                this.ViewModel = new FilterViewModelBase<Product>(category, pageSizeFilter, sortFilter, producerFilter, pageFilter, groupFilter);
                var entities = this.DbContext.Products.Include(i => i.Producer).Where(obj => obj.CategoryName == category).AsQueryable();
                entities = this.StartInitialize(entities);
                this.EndInitialize(entities);
                this.ViewModel.CategoryObj = DbContext.Categories.FirstOrDefault(obj => obj.Name == category);
                return this.View(this.ViewModel);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return this.RedirectToAction("index", "error", new { statusCode = 404 });
            }
        }

        public ActionResult Details(string category, string name)
        {
            if (!string.IsNullOrEmpty(this.Request.Url.Query))
            {
                if (this.Request.Url.Query.Contains("type"))
                {
                    return this.RedirectToAction("index", "error", new { statusCode = 404 });
                }
            }

            var product = this.DbContext.Products.Include(i => i.Producer).Include(o => o.Group).Include(o => o.Category).FirstOrDefault(obj => obj.CategoryName == category && obj.Name == name);
            if (product == null)
            {
                return this.RedirectToAction("index", "error", new { statusCode = 404 });
            }

            return this.View(product);
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Create(Product battery)
        {
            if (this.ModelState.IsValid)
            {
                this.DbContext.Products.Add(battery);
                this.DbContext.SaveChanges();
                return this.RedirectToAction("index");
            }

            return this.View(battery);
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(int id)
        {
            var product = this.DbContext.Products
                .Include(i => i.Producer)
                .Include(o => o.Group)
                .Include(o => o.Category)
                .FirstOrDefault(obj => obj.Id == id);

            
            if (product == null)
            {
                return this.RedirectToAction("index", "error", new { statusCode = 404 });
            }

            return this.View(product);
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(Product product)
        {
            var currentProduct = DbContext.Products
                .Include(obj => obj.Category).FirstOrDefault(obj => obj.Id == product.Id);
            if (currentProduct == null)
            {
                return this.RedirectToAction("index", "error", new { statusCode = 404 });
            }

            var name = product.Name.Trim();
            var discount = product.Discount;
            var price = product.Price;
            var description = string.IsNullOrEmpty(product.Description) ? string.Empty : product.Description.Trim();
            var dynamicProperties = product.DynamicProperties.Where(obj => !string.IsNullOrEmpty(obj.Key) && !string.IsNullOrEmpty(obj.Value.ToString()));

            product = currentProduct;
            product.Name = name;
            product.Discount = discount;
            product.Price = price;
            product.Description = description;
            product.DynamicProperties = dynamicProperties;

            //todo
            this.DbContext.Entry(product).State = EntityState.Modified;
            this.DbContext.SaveChanges();
            return this.RedirectToAction("details" , new { category = product.CategoryName, name = product.Name});
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Delete(int id = 0)
        {
            Product product = this.DbContext.Products.Find(id);
            if (product == null)
            {
                return this.RedirectToAction("index", "error");
            }
            return this.View(product);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult DeleteConfirmed(int id)
        {
            var battery = this.DbContext.Products.Find(id);
            this.DbContext.Products.Remove(battery);
            this.DbContext.SaveChanges();
            return this.RedirectToAction("index");
        }

        protected override void Dispose(bool disposing)
        {
            this.DbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}
