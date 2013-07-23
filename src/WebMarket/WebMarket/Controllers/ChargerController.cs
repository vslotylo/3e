using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using WebMarket.Filters;

namespace WebMarket.Controllers
{
    public class ChargerController : ListControllerBase<Charger>
    {
        public ActionResult Index(PageSizeFilter pageSizeFilter, SortFilter sortFilter, ProducersFilter producerFilter, PageFilter pageFilter, TypeFilter typeFilter)
        {
            this.ViewModel = new FilterViewModelBase<Charger>(pageSizeFilter, sortFilter, producerFilter, pageFilter, typeFilter);
            var entities = this.DbContext.Chargers.Include(i => i.Producer).AsQueryable();
            entities = this.StartInitialize(entities);
            this.EndInitialize(entities);
            return this.View(this.ViewModel);
        }

        //
        // GET: /Charger/Details/5

        public ActionResult Details(int id = 0)
        {
            var charger = this.DbContext.Chargers.Include(i => i.Producer).FirstOrDefault(i => i.Id == id);
            if (charger == null)
            {
                return this.HttpNotFound();
            }
            return this.View(charger);
        }

        //
        // GET: /Charger/Create

        public ActionResult Create()
        {
            return this.View();
        }

        //
        // POST: /Charger/Create

        [HttpPost]
        public ActionResult Create(Charger charger)
        {
            if (this.ModelState.IsValid)
            {
                this.DbContext.Chargers.Add(charger);
                this.DbContext.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(charger);
        }

        //
        // GET: /Charger/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Charger charger = this.DbContext.Chargers.Find(id);
            if (charger == null)
            {
                return this.HttpNotFound();
            }
            return this.View(charger);
        }

        //
        // POST: /Charger/Edit/5

        [HttpPost]
        public ActionResult Edit(Charger charger)
        {
            if (this.ModelState.IsValid)
            {
                this.DbContext.Entry(charger).State = EntityState.Modified;
                this.DbContext.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(charger);
        }

        //
        // GET: /Charger/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Charger charger = this.DbContext.Chargers.Find(id);
            if (charger == null)
            {
                return this.HttpNotFound();
            }
            return this.View(charger);
        }

        //
        // POST: /Charger/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Charger charger = this.DbContext.Chargers.Find(id);
            this.DbContext.Chargers.Remove(charger);
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