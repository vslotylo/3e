using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using WebMarket.Filters;

namespace WebMarket.Controllers
{
    public class ConverterController : ListControllerBase<Converter>
    {
        public ActionResult Index(PageSizeFilter pageSizeFilter, SortFilter sortFilter, ProducersFilter producerFilter, PageFilter pageFilter, TypeFilter typeFilter)
        {
            this.ViewModel = new FilterViewModelBase<Converter>(pageSizeFilter, sortFilter, producerFilter, pageFilter, typeFilter);
            var entities = this.DbContext.Converters.Include(i => i.Producer).AsQueryable();
            entities = this.StartInitialize(entities);
            this.EndInitialize(entities);
            return this.View(this.ViewModel);
        }

        //
        // GET: /Charger/Details/5

        public ActionResult Details(int id = 0)
        {
            var converter = this.DbContext.Converters.Include(i => i.Producer).FirstOrDefault(i => i.Id == id);
            if (converter == null)
            {
                return this.HttpNotFound();
            }
            return this.View(converter);
        }

        //
        // GET: /Converter/Create

        public ActionResult Create()
        {
            return this.View();
        }

        //
        // POST: /Converter/Create

        [HttpPost]
        public ActionResult Create(Converter converter)
        {
            if (this.ModelState.IsValid)
            {
                this.DbContext.Converters.Add(converter);
                this.DbContext.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(converter);
        }

        //
        // GET: /Converter/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Converter converter = this.DbContext.Converters.Find(id);
            if (converter == null)
            {
                return this.HttpNotFound();
            }
            return this.View(converter);
        }

        //
        // POST: /Converter/Edit/5

        [HttpPost]
        public ActionResult Edit(Converter converter)
        {
            if (this.ModelState.IsValid)
            {
                this.DbContext.Entry(converter).State = EntityState.Modified;
                this.DbContext.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(converter);
        }

        //
        // GET: /Converter/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Converter converter = this.DbContext.Converters.Find(id);
            if (converter == null)
            {
                return this.HttpNotFound();
            }
            return this.View(converter);
        }

        //
        // POST: /Converter/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Converter converter = this.DbContext.Converters.Find(id);
            this.DbContext.Converters.Remove(converter);
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