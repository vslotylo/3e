using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using WebMarket.Filters;

namespace WebMarket.Controllers
{
    public class CurrentRelayController : ListControllerBase<CurrentRelay>
    {
        public ActionResult Index(PageSizeFilter pageSizeFilter, SortFilter sortFilter, ProducersFilter producerFilter, PageFilter pageFilter, TypeFilter typeFilter)
        {
            this.ViewModel = new FilterViewModelBase<CurrentRelay>(pageSizeFilter, sortFilter, producerFilter, pageFilter, typeFilter);
            var entities = this.DbContext.CurrentRelays.Include(obj => obj.Producer).AsQueryable();
            entities = this.StartInitialize(entities);
            this.EndInitialize(entities);
            return this.View(this.ViewModel);
        }

        public ActionResult Details(int id = 0)
        {
            var entity = this.DbContext.CurrentRelays.Include(i => i.Producer).FirstOrDefault(i => i.Id == id);
            if (entity == null)
            {
                return this.HttpNotFound();
            }
            return this.View(entity);
        }

        //
        // GET: /CurrentRelay/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CurrentRelay/Create

        [HttpPost]
        public ActionResult Create(CurrentRelay currentrelay)
        {
            if (ModelState.IsValid)
            {
                DbContext.CurrentRelays.Add(currentrelay);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(currentrelay);
        }

        //
        // GET: /CurrentRelay/Edit/5

        public ActionResult Edit(int id = 0)
        {
            CurrentRelay currentrelay = DbContext.CurrentRelays.Find(id);
            if (currentrelay == null)
            {
                return HttpNotFound();
            }
            return View(currentrelay);
        }

        //
        // POST: /CurrentRelay/Edit/5

        [HttpPost]
        public ActionResult Edit(CurrentRelay currentrelay)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(currentrelay).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(currentrelay);
        }

        //
        // GET: /CurrentRelay/Delete/5

        public ActionResult Delete(int id = 0)
        {
            CurrentRelay currentrelay = DbContext.CurrentRelays.Find(id);
            if (currentrelay == null)
            {
                return HttpNotFound();
            }
            return View(currentrelay);
        }

        //
        // POST: /CurrentRelay/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CurrentRelay currentrelay = DbContext.CurrentRelays.Find(id);
            DbContext.CurrentRelays.Remove(currentrelay);
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