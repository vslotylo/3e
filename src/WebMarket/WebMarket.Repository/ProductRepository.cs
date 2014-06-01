using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WebMarket.Repository.Core;
using WebMarket.Repository.Entities;

namespace WebMarket.Repository.Current
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override Product Find(object id)
        {
            return DbContext.Products.Find(id);
        }

        public Product GetWithProducersGroups(object id)
        {
            return DbContext.Products
                            .Include(obj => obj.Producer)
                            .Include(obj => obj.Group)
                            .FirstOrDefault(obj => obj.Id == (int) id);
        }

        public Product GetWithCategory(object id)
        {
            return DbContext.Products.Include(i => i.Producer)
                            .Include(o => o.Group)
                            .Include(o => o.Category)
                            .FirstOrDefault(obj => obj.Id == (int) id);
        }

        public Product Details(string categoryName, string productName)
        {
            return
                DbContext.Products.Include(i => i.Producer)
                         .Include(o => o.Group)
                         .Include(o => o.Category)
                         .FirstOrDefault(obj => obj.CategoryName == categoryName && obj.Name == productName);
        }

        public IEnumerable<Product> GetByIds(IEnumerable<int> ids)
        {
            return DbContext.Products.Where(obj => ids.Contains(obj.Id)).ToList();
        }

        public IQueryable<Product> GetProductsWithProducerByProductName(string categoryName)
        {
            return
                DbContext.Products.Include(i => i.Producer).Where(obj => obj.CategoryName == categoryName).AsQueryable();
        }

        public IEnumerable<Product> GetProductWithProducersByExpression(Expression<Func<Product, bool>> expression)
        {
            return DbContext.Products.Include(obj => obj.Producer).Where(expression);
        }
    }
}