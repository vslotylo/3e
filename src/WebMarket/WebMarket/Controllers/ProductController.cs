using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.DAL.Entities;
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
                this.ViewModel = new FilterViewModelBase<Product>(category, pageSizeFilter, sortFilter, producerFilter, pageFilter, groupFilter);
                var entities = this.DbContext.Products.Include(i => i.Producer).Where(obj => obj.CategoryName == category).AsQueryable();
                entities = this.StartInitialize(entities);
                this.EndInitialize(entities);
                this.ViewModel.Metadata = MetadataProvider.Current.GetMetadataInfo(category);
                return this.View(this.ViewModel);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return this.RedirectToAction("index", "error", new { statusCode = 500 });
            }            
        }

        public ActionResult Details(string category, string name)
        {
            
            var entity = this.DbContext.Products.Include(i => i.Producer).Include(o=>o.Group).FirstOrDefault(obj => obj.CategoryName == category && obj.Name == name);
            if (entity == null)
            {
                return this.RedirectToAction("index", "error", new { statusCode = 404});
            }

            var metadata = MetadataProvider.Current.GetMetadataInfo(category);
            return this.View(new DetailsViewModel(entity, metadata));
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

        public ActionResult Edit(int id = 0)
        {
            var battery = this.DbContext.Products.Find(id);
            if (battery == null)
            {
                return this.RedirectToAction("index", "error");
            }
            return this.View(battery);
        }

        [HttpPost]
        public ActionResult Edit(Product battery)
        {
            if (this.ModelState.IsValid)
            {
                this.DbContext.Entry(battery).State = EntityState.Modified;
                this.DbContext.SaveChanges();
                return this.RedirectToAction("index");
            }
            return this.View(battery);
        }

        public ActionResult Delete(int id = 0)
        {
            Product battery = this.DbContext.Products.Find(id);
            if (battery == null)
            {
                return this.RedirectToAction("index", "error");
            }
            return this.View(battery);
        }

        [HttpPost, ActionName("Delete")]
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
