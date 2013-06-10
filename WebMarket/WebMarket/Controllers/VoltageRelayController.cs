using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using WebMarket.Filters;

namespace WebMarket.Controllers
{
    public class VoltageRelayController : ListControllerBase<VoltageRelay>
    {
        public ActionResult Index(PageSizeFilter pageSizeFilter, SortFilter sortFilter, ProducersFilter producerFilter, PageFilter pageFilter, TypeFilter typeFilter)
        {
            this.ViewModel = new FilterViewModelBase<VoltageRelay>(pageSizeFilter, sortFilter, producerFilter, pageFilter, typeFilter);
            var entities = this.DbContext.VoltageRelays.Include(obj => obj.Producer).AsQueryable();
            entities = this.StartInitialize(entities);
            this.EndInitialize(entities);
            return this.View(this.ViewModel);
        }

        public ActionResult Details(int id = 0)
        {
            var entity = this.DbContext.VoltageRelays.Include(i => i.Producer).FirstOrDefault(i => i.Id == id);
            if (entity == null)
            {
                return this.HttpNotFound();
            }
            return this.View(entity);
        }

        //
        // GET: /VoltageRelay/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /VoltageRelay/Create

        [HttpPost]
        public ActionResult Create(VoltageRelay voltagerelay)
        {
            if (ModelState.IsValid)
            {
                DbContext.VoltageRelays.Add(voltagerelay);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(voltagerelay);
        }

        //
        // GET: /VoltageRelay/Edit/5

        public ActionResult Edit(int id = 0)
        {
            VoltageRelay voltagerelay = DbContext.VoltageRelays.Find(id);
            if (voltagerelay == null)
            {
                return HttpNotFound();
            }
            return View(voltagerelay);
        }

        //
        // POST: /VoltageRelay/Edit/5

        [HttpPost]
        public ActionResult Edit(VoltageRelay voltagerelay)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(voltagerelay).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voltagerelay);
        }

        //
        // GET: /VoltageRelay/Delete/5

        public ActionResult Delete(int id = 0)
        {
            VoltageRelay voltagerelay = DbContext.VoltageRelays.Find(id);
            if (voltagerelay == null)
            {
                return HttpNotFound();
            }
            return View(voltagerelay);
        }

        //
        // POST: /VoltageRelay/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            VoltageRelay voltagerelay = DbContext.VoltageRelays.Find(id);
            DbContext.VoltageRelays.Remove(voltagerelay);
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