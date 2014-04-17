using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebMarket.Core;
using WebMarket.Filters;
using WebMarket.Repository.Entities;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Controllers
{
    public class SearchController : ListControllerBase
    {
        private readonly IProductRepository productRepository;
        private Expression<Func<Product, bool>> expression;

        public SearchController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }


        public ActionResult Index(PageFilter pageFilter, SortFilter sortFilter, PageSizeFilter pageSizeFilter,
                                  SearchFilter searchFilter)
        {
            var seperators = new[] {" ", "-"};
            string[] tokens = searchFilter.Keyword.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            ViewModel = new FilterViewModelBase(pageSizeFilter, sortFilter, pageFilter, searchFilter);
            expression = obj => tokens.All(t => obj.DisplayName.Contains(t));
            IEnumerable<Product> products = productRepository.GetProductWithProducersByExpression(expression);
            StartInitializeCommon(products.Count());
            EndInitializeCommon(products);
            return View(ViewModel);
        }
    }
}