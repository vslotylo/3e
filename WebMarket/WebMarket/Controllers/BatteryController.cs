using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using WebMarket.Filters;

namespace WebMarket.Controllers
{
    public class BatteryController : ListControllerBase<Battery>
    {
        [HttpGet]
        public ActionResult Index(PageSizeFilter pageSizeFilter, SortFilter sortFilter, ProducersFilter producerFilter, PageFilter pageFilter, TypeFilter typeFilter)
        {
            this.ViewModel = new FilterViewModelBase<Battery>(pageSizeFilter, sortFilter, producerFilter, pageFilter, typeFilter);
            var entities = this.DbContext.Batteries.Include(i=>i.Producer).AsQueryable();
            entities = this.StartInitialize(entities);
            this.EndInitialize(entities);
            return this.View(this.ViewModel);
        }

        //
        // GET: /Battery/Details/5

        public ActionResult Details(int id = 0)
        {
            var entity = this.DbContext.Batteries.Include(i => i.Producer).FirstOrDefault(i => i.Id == id);
            if (entity == null)
            {
                return this.HttpNotFound();
            }
            return this.View(entity);
        }

        //
        // GET: /Battery/Create

        public ActionResult Create()
        {
            return this.View();
        }

        //
        // POST: /Battery/Create

        [HttpPost]
        public ActionResult Create(Battery battery)
        {
            if (this.ModelState.IsValid)
            {
                this.DbContext.Batteries.Add(battery);
                this.DbContext.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(battery);
        }

        //
        // GET: /Battery/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Battery battery = this.DbContext.Batteries.Find(id);
            if (battery == null)
            {
                return this.HttpNotFound();
            }
            return this.View(battery);
        }

        //
        // POST: /Battery/Edit/5

        [HttpPost]
        public ActionResult Edit(Battery battery)
        {
            if (this.ModelState.IsValid)
            {
                this.DbContext.Entry(battery).State = EntityState.Modified;
                this.DbContext.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(battery);
        }

        //
        // GET: /Battery/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Battery battery = this.DbContext.Batteries.Find(id);
            if (battery == null)
            {
                return this.HttpNotFound();
            }
            return this.View(battery);
        }

        //
        // POST: /Battery/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Battery battery = this.DbContext.Batteries.Find(id);
            this.DbContext.Batteries.Remove(battery);
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