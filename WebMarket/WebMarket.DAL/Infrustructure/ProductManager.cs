using System;
using System.Collections.Generic;
using System.Linq;
using WebMarket.DAL.Common;
using WebMarket.DAL.Entities;
using WebMarket.DAL.Interfaces;
using System.Data.Entity;

namespace WebMarket.DAL.Infrustructure
{
    // todo
    public class ProductManager : IProductManager
    {
        private readonly WebMarketDbContext dbContext = new WebMarketDbContext();
        private static readonly List<Type> CategoriesDictionary = new List<Type>();
        static ProductManager()
        {
            CategoriesDictionary.Add(typeof(Avr));
            CategoriesDictionary.Add(typeof(Battery));
            CategoriesDictionary.Add(typeof(Charger));
            CategoriesDictionary.Add(typeof(Ups));
            CategoriesDictionary.Add(typeof(Converter));
            CategoriesDictionary.Add(typeof(VoltageRelay));
            CategoriesDictionary.Add(typeof(CurrentRelay));
            CategoriesDictionary.Add(typeof(TimeRelay));
            CategoriesDictionary.Add(typeof(TemperatureRelay));
            CategoriesDictionary.Add(typeof(Voltmeter));
        }

        public static string GetProductId(Product p)
        {
            var type = p.GetType();
            int id = CategoriesDictionary.IndexOf(type) + 1;
            return string.Format("{0}-{1}", id.ToString("D3"), p.Id.ToString("D4"));
        }

        private int GetType(string pid)
        {
            return int.Parse(pid.Substring(0, 3));            
        }

        private int GetId(string pid)
        {
            return int.Parse(pid.Substring(4, 4));
        }

        public Product GetProductByPid(string pid)
        {
            var typeId = GetType(pid);
            var id = GetId(pid);
            var type = CategoriesDictionary[typeId - 1];

            if (type == typeof(Avr)) return this.dbContext.Avrs.Include(p => p.Producer).FirstOrDefault(p => p.Id == id);
            if (type == typeof(Battery)) return this.dbContext.Batteries.Include(p => p.Producer).FirstOrDefault(p => p.Id == id);
            if (type == typeof(Charger)) return this.dbContext.Chargers.Include(p => p.Producer).FirstOrDefault(p => p.Id == id);
            if (type == typeof(Ups)) return this.dbContext.Ups.Include(p => p.Producer).FirstOrDefault(p => p.Id == id);
            if (type == typeof(Converter)) return this.dbContext.Converters.Include(p => p.Producer).FirstOrDefault(p => p.Id == id);
            if (type == typeof(VoltageRelay)) return this.dbContext.VoltageRelays.Include(p => p.Producer).FirstOrDefault(p => p.Id == id);
            if (type == typeof(CurrentRelay)) return this.dbContext.CurrentRelays.Include(p => p.Producer).FirstOrDefault(p => p.Id == id);
            if (type == typeof(TimeRelay)) return this.dbContext.TimeRelays.Include(p => p.Producer).FirstOrDefault(p => p.Id == id);
            if (type == typeof(TemperatureRelay)) return this.dbContext.TemperatureRelays.Include(p => p.Producer).FirstOrDefault(p => p.Id == id);
            if (type == typeof(Voltmeter)) return this.dbContext.Voltmeters.Include(p => p.Producer).FirstOrDefault(p => p.Id == id);
            return null;
        }
    }
}
