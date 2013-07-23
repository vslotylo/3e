using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using WebMarket.Filters;

namespace WebMarket.Controllers
{
    public class TimeRelayController : ListControllerBase<TimeRelay>
    {
        public ActionResult Index(PageSizeFilter pageSizeFilter, SortFilter sortFilter, ProducersFilter producerFilter, PageFilter pageFilter, TypeFilter typeFilter)
        {
            this.ViewModel = new FilterViewModelBase<TimeRelay>(pageSizeFilter, sortFilter, producerFilter, pageFilter, typeFilter);
            var entities = this.DbContext.TimeRelays.Include(obj => obj.Producer).AsQueryable();
            entities = this.StartInitialize(entities);
            this.EndInitialize(entities);
            return this.View(this.ViewModel);
        }

        public ActionResult Details(int id = 0)
        {
            var entity = this.DbContext.TimeRelays.Include(i => i.Producer).FirstOrDefault(i => i.Id == id);
            if (entity == null)
            {
                return this.HttpNotFound();
            }
            return this.View(entity);
        }

        //
        // GET: /TimeRelay/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TimeRelay/Create

        [HttpPost]
        public ActionResult Create(TimeRelay timerelay)
        {
            if (ModelState.IsValid)
            {
                DbContext.TimeRelays.Add(timerelay);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timerelay);
        }

        //
        // GET: /TimeRelay/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TimeRelay timerelay = DbContext.TimeRelays.Find(id);
            if (timerelay == null)
            {
                return HttpNotFound();
            }
            return View(timerelay);
        }

        //
        // POST: /TimeRelay/Edit/5

        [HttpPost]
        public ActionResult Edit(TimeRelay timerelay)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(timerelay).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timerelay);
        }

        //
        // GET: /TimeRelay/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TimeRelay timerelay = DbContext.TimeRelays.Find(id);
            if (timerelay == null)
            {
                return HttpNotFound();
            }
            return View(timerelay);
        }

        //
        // POST: /TimeRelay/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeRelay timerelay = DbContext.TimeRelays.Find(id);
            DbContext.TimeRelays.Remove(timerelay);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            DbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}