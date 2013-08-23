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
        public ActionResult Index(string category, PageSizeFilter pageSizeFilter, SortFilter sortFilter, ProducersFilter producerFilter, PageFilter pageFilter, TypeFilter typeFilter)
        {
            // todo validate args here
            var metadata = MetadataProvider.Current.GetMetadataInfo(category);
            this.ViewModel = new FilterViewModelBase<Product>(pageSizeFilter, sortFilter, producerFilter, pageFilter, typeFilter) { Metadata = metadata };
            
            var entities = this.DbContext.Products.Include(i => i.Producer).Where(obj => obj.CategoryName == category).AsQueryable();
            entities = this.StartInitialize(entities);
            this.EndInitialize(entities);
            return this.View(this.ViewModel);
        }

        public ActionResult Details(string category, string name)
        {
            var metadata = MetadataProvider.Current.GetMetadataInfo(category);
            var entity = this.DbContext.Products.Include(i => i.Producer).FirstOrDefault(obj => obj.CategoryName == category && obj.Name == name);
            if (entity == null)
            {
                return this.RedirectToAction("Index", "Error", new { statusCode = 404});
            }

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
                return this.RedirectToAction("Index");
            }

            return this.View(battery);
        }

        public ActionResult Edit(int id = 0)
        {
            var battery = this.DbContext.Products.Find(id);
            if (battery == null)
            {
                return this.RedirectToAction("Index", "Error");
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
                return this.RedirectToAction("Index");
            }
            return this.View(battery);
        }

        public ActionResult Delete(int id = 0)
        {
            Product battery = this.DbContext.Products.Find(id);
            if (battery == null)
            {
                return this.RedirectToAction("Index", "Error");
            }
            return this.View(battery);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var battery = this.DbContext.Products.Find(id);
            this.DbContext.Products.Remove(battery);
            this.DbContext.SaveChanges();
            return this.RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            this.DbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}
