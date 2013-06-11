using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.DAL.Entities;
using WebMarket.Filters;
using WebMarket.ViewModels;

namespace WebMarket.Controllers
{
    public class AvrController : ListControllerBase<Avr>
    {
        [HttpGet]
        public ActionResult Index(PageSizeFilter pageSizeFilter, SortFilter sortFilter, ProducersFilter producerFilter, PageFilter pageFilter, TypeFilter typeFilter, PowerCapacityFilter powerCapacityFilter)
        {
            this.ViewModel = new AvrViewModel(pageSizeFilter, sortFilter, producerFilter, pageFilter, typeFilter, powerCapacityFilter);
            var entities = this.DbContext.Avrs.Include(p=>p.Producer).AsQueryable();
            var powerFilter = new List<GenericFilterModel<Tuple<double, double>>>
            {
                new GenericFilterModel<Tuple<double,double>>{ Name = "0 - 0.5 кВт (котел або телевізор)", Value = new Tuple<double,double>(0, 0.5), ProductsCount = entities.Where(avr => avr.PowerCapacity <= 0.5).Count()},
                new GenericFilterModel<Tuple<double,double>>{ Name = "0,5 - 1 кВт (комп'ютер)", Value = new Tuple<double,double>(0.5, 1), ProductsCount = entities.Where(avr => avr.PowerCapacity > 0.5 && avr.PowerCapacity <= 1).Count()},
                new GenericFilterModel<Tuple<double,double>>{ Name = "1 - 2,2 кВт (холодильник)", Value = new Tuple<double,double>(1, 2.2), ProductsCount = entities.Where(avr => avr.PowerCapacity > 1 && avr.PowerCapacity <= 2.2).Count()},
                new GenericFilterModel<Tuple<double,double>>{ Name = "2,2 - 3,5 кВт (пральна машина)", Value = new Tuple<double,double>(2.2, 3.5), ProductsCount = entities.Where(avr => avr.PowerCapacity > 2.2 && avr.PowerCapacity <= 3.5).Count()},
                new GenericFilterModel<Tuple<double,double>>{ Name = "3,5 - 5 кВт (насос, мотор)", Value = new Tuple<double,double>(3.5, 5), ProductsCount = entities.Where(avr => avr.PowerCapacity > 3.5 && avr.PowerCapacity <= 5).Count()},
                new GenericFilterModel<Tuple<double,double>>{ Name = "5 - 8 кВт (квартира)", Value = new Tuple<double,double>(5, 8), ProductsCount = entities.Where(avr => avr.PowerCapacity > 5 && avr.PowerCapacity <= 8).Count()},
                new GenericFilterModel<Tuple<double,double>>{ Name = "8 - 18 кВт (особняк)", Value = new Tuple<double,double>(8, 18), ProductsCount = entities.Where(avr => avr.PowerCapacity > 8 && avr.PowerCapacity <= 18).Count()},
                new GenericFilterModel<Tuple<double,double>>{ Name = "18 - ... кВт (промислові)", Value = new Tuple<double,double>(18, 1000), ProductsCount = entities.Where(avr => avr.PowerCapacity > 18).Count()}
            };

            entities = this.StartInitialize(entities);

            if (powerCapacityFilter.PowerCapacityList.Any())
            {
                var filter = powerCapacityFilter.PowerCapacityList.First();
                entities = entities.Where(o => o.PowerCapacity > filter.Item1 && o.PowerCapacity <= filter.Item2);
            }
                       
            foreach (var item in powerFilter)
            {
                item.IsSelected = powerCapacityFilter.PowerCapacityList.Contains(item.Value);
            }

            ((AvrViewModel)this.ViewModel).PowerCapacity = powerFilter;
            
            this.EndInitialize(entities);

            return this.View(this.ViewModel);            
        }

        public ActionResult Details(int id = 0)
        {
            Avr avr = this.DbContext.Avrs.Include(p => p.Producer).FirstOrDefault(p => p.Id == id);
            if (avr == null)
            {
                return this.HttpNotFound();
            }
            return this.View(avr);
        }

        //
        // GET: /Avr/Create

        public ActionResult Create()
        {
            return this.View();
        }

        //
        // POST: /Avr/Create

        [HttpPost]
        public ActionResult Create(Avr avr)
        {
            if (this.ModelState.IsValid)
            {
                this.DbContext.Avrs.Add(avr);
                this.DbContext.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(avr);
        }

        //
        // GET: /Avr/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Avr avr = this.DbContext.Avrs.Find(id);
            if (avr == null)
            {
                return this.HttpNotFound();
            }
            return this.View(avr);
        }

        //
        // POST: /Avr/Edit/5

        [HttpPost]
        public ActionResult Edit(Avr avr)
        {
            if (this.ModelState.IsValid)
            {
                this.DbContext.Entry(avr).State = EntityState.Modified;
                this.DbContext.SaveChanges();
                return this.RedirectToAction("Index");
            }
            return this.View(avr);
        }

        //
        // GET: /Avr/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Avr avr = this.DbContext.Avrs.Find(id);
            if (avr == null)
            {
                return this.HttpNotFound();
            }
            return this.View(avr);
        }

        //
        // POST: /Avr/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Avr avr = this.DbContext.Avrs.Find(id);
            this.DbContext.Avrs.Remove(avr);
            this.DbContext.SaveChanges();
            return this.RedirectToAction("Index");
        }

        public ActionResult FilterByType()
        {
            

            return this.View();
        }

        //public ActionResult Search(string producer, string keyword)
        //{
        //    //var movies = from m in DbContext.Movies
        //    //             select m;

        //    //if (!String.IsNullOrEmpty(searchString))
        //    //{
        //    //    movies = movies.Where(s => s.Title.Contains(searchString));
        //    //}

            

            
        //}       

        protected override void Dispose(bool disposing)
        {
            this.DbContext.Dispose();
            base.Dispose(disposing);
        }
    }    
}