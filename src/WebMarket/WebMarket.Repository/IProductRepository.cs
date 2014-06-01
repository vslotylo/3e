using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebMarket.Repository.Core;
using WebMarket.Repository.Entities;

namespace WebMarket.Repository.Current
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> GetProductsWithProducerByProductName(string categoryName);
        IEnumerable<Product> GetProductWithProducersByExpression(Expression<Func<Product, bool>> expression);
        Product GetWithProducersGroups(object id);
        Product GetWithCategory(object id);
        Product Details(string categoryName, string productName);
        IEnumerable<Product> GetByIds(IEnumerable<int> ids);
    }
}
