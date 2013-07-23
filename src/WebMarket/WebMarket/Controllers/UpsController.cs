using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using WebMarket.Filters;

namespace WebMarket.Controllers
{
    public class UpsController : ListControllerBase<Ups>
    {
        public ActionResult Index(PageSizeFilter pageSizeFilter, SortFilter sortFilter, ProducersFilter producerFilter, PageFilter pageFilter, TypeFilter typeFilter)
        {
            this.ViewModel = new FilterViewModelBase<Ups>(pageSizeFilter, sortFilter, producerFilter, pageFilter, typeFilter);
            var entities = this.DbContext.Ups.Include(i => i.Producer).AsQueryable();
            entities = this.StartInitialize(entities);
            this.EndInitialize(entities);
            return this.View(this.ViewModel);
        }

        //
        // GET: /Ups/Details/5

        public ActionResult Details(int id = 0)
        {
            var ups = this.DbContext.Ups.Include(i => i.Producer).FirstOrDefault(i => i.Id == id);
            if (ups == null)
            {
                return this.HttpNotFound();
            }
            return this.View(ups);
        }

        //
        // GET: /Ups/Create

        public ActionResult Create()
        {
            return this.View();
        }

        //
        // POST: /Ups/Create

        [HttpPost]
        public ActionResult Create(Ups ups)
        {
            if (this.ModelState.IsValid)
            {
                this.DbContext.Ups.Add(ups);
                this.DbContext.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(ups);
        }

        //
        // GET: /Ups/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Ups ups = this.DbContext.Ups.Find(id);
            if (ups == null)
            {
                return this.HttpNotFound();
            }
            return this.View(ups);
        }

        //
        // POST: /Ups/Edit/5

        [HttpPost]
        public ActionResult Edit(Ups ups)
        {
            if (this.ModelState.IsValid)
            {
                this.DbContext.Entry(ups).State = EntityState.Modified;
                this.DbContext.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(ups);
        }

        //
        // GET: /Ups/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Ups ups = this.DbContext.Ups.Find(id);
            if (ups == null)
            {
                return this.HttpNotFound();
            }
            return this.View(ups);
        }

        //
        // POST: /Ups/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ups ups = this.DbContext.Ups.Find(id);
            this.DbContext.Ups.Remove(ups);
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