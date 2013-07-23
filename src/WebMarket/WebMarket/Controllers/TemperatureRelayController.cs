using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using WebMarket.Filters;

namespace WebMarket.Controllers
{
    public class TemperatureRelayController : ListControllerBase<TemperatureRelay>
    {
        public ActionResult Index(PageSizeFilter pageSizeFilter, SortFilter sortFilter, ProducersFilter producerFilter, PageFilter pageFilter, TypeFilter typeFilter)
        {
            this.ViewModel = new FilterViewModelBase<TemperatureRelay>(pageSizeFilter, sortFilter, producerFilter, pageFilter, typeFilter);
            var entities = this.DbContext.TemperatureRelays.Include(obj => obj.Producer).AsQueryable();
            entities = this.StartInitialize(entities);
            this.EndInitialize(entities);
            return this.View(this.ViewModel);
        }

        public ActionResult Details(int id = 0)
        {
            var entity = this.DbContext.TemperatureRelays.Include(i => i.Producer).FirstOrDefault(i => i.Id == id);
            if (entity == null)
            {
                return this.HttpNotFound();
            }
            return this.View(entity);
        }

        //
        // GET: /TemperatureRelay/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TemperatureRelay/Create

        [HttpPost]
        public ActionResult Create(TemperatureRelay temperaturerelay)
        {
            if (ModelState.IsValid)
            {
                DbContext.TemperatureRelays.Add(temperaturerelay);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(temperaturerelay);
        }

        //
        // GET: /TemperatureRelay/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TemperatureRelay temperaturerelay = DbContext.TemperatureRelays.Find(id);
            if (temperaturerelay == null)
            {
                return HttpNotFound();
            }
            return View(temperaturerelay);
        }

        //
        // POST: /TemperatureRelay/Edit/5

        [HttpPost]
        public ActionResult Edit(TemperatureRelay temperaturerelay)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(temperaturerelay).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(temperaturerelay);
        }

        //
        // GET: /TemperatureRelay/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TemperatureRelay temperaturerelay = DbContext.TemperatureRelays.Find(id);
            if (temperaturerelay == null)
            {
                return HttpNotFound();
            }
            return View(temperaturerelay);
        }

        //
        // POST: /TemperatureRelay/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TemperatureRelay temperaturerelay = DbContext.TemperatureRelays.Find(id);
            DbContext.TemperatureRelays.Remove(temperaturerelay);
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