using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using WebMarket.Filters;

namespace WebMarket.Controllers
{
    public class VoltmeterController : ListControllerBase<Voltmeter>
    {
        [HttpGet]
        public ActionResult Index(PageSizeFilter pageSizeFilter, SortFilter sortFilter, ProducersFilter producerFilter, PageFilter pageFilter, TypeFilter typeFilter)
        {
            this.ViewModel = new FilterViewModelBase<Voltmeter>(pageSizeFilter, sortFilter, producerFilter, pageFilter, typeFilter);
            var entities = this.DbContext.Voltmeters.Include(i => i.Producer).AsQueryable();
            entities = this.StartInitialize(entities);
            this.EndInitialize(entities);
            return this.View(this.ViewModel);
        }

        //
        // GET: /Voltmeter/Details/5

        public ActionResult Details(int id = 0)
        {
            var entity = this.DbContext.Voltmeters.Include(i => i.Producer).FirstOrDefault(i => i.Id == id);
            if (entity == null)
            {
                return this.HttpNotFound();
            }
            return this.View(entity);
        }

        //
        // GET: /Voltmeter/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Voltmeter/Create

        [HttpPost]
        public ActionResult Create(Voltmeter voltmeter)
        {
            if (ModelState.IsValid)
            {
                DbContext.Voltmeters.Add(voltmeter);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(voltmeter);
        }

        //
        // GET: /Voltmeter/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Voltmeter voltmeter = DbContext.Voltmeters.Find(id);
            if (voltmeter == null)
            {
                return HttpNotFound();
            }
            return View(voltmeter);
        }

        //
        // POST: /Voltmeter/Edit/5

        [HttpPost]
        public ActionResult Edit(Voltmeter voltmeter)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(voltmeter).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voltmeter);
        }

        //
        // GET: /Voltmeter/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Voltmeter voltmeter = DbContext.Voltmeters.Find(id);
            if (voltmeter == null)
            {
                return HttpNotFound();
            }
            return View(voltmeter);
        }

        //
        // POST: /Voltmeter/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Voltmeter voltmeter = DbContext.Voltmeters.Find(id);
            DbContext.Voltmeters.Remove(voltmeter);
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