using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.Repository.Entities;
using WebMarket.Filters;
using WebMarket.Repository.Interfaces;


namespace WebMarket.Controllers
{
    public class SearchController : ListControllerBase
    {
        private Expression<Func<Product, bool>> expression;
        private readonly IProductRepository productRepository;

        public SearchController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }


        public ActionResult Index(PageFilter pageFilter, SortFilter sortFilter, PageSizeFilter pageSizeFilter, SearchFilter searchFilter)
        {
            var seperators = new[] { " ", "-" };
            var tokens = searchFilter.Keyword.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            this.ViewModel = new FilterViewModelBase(pageSizeFilter, sortFilter, pageFilter, searchFilter);
            this.expression = obj=> tokens.All(t=> obj.DisplayName.Contains(t));
            var products = productRepository.GetProductWithProducersByExpression(expression);
            this.StartInitializeCommon(products.Count());
            this.EndInitializeCommon(products);
            return this.View(this.ViewModel);
        }
    }
}
