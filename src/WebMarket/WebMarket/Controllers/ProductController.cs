using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using WebMarket.Binders;
using WebMarket.Common;
using WebMarket.Core;
using WebMarket.Extensions.Entities;
using WebMarket.Filters;
using WebMarket.Models;
using WebMarket.Repository.Entities;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Controllers
{
    public class ProductController : ListControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public ActionResult Index(string category, PageSizeFilter pageSizeFilter, SortFilter sortFilter,
                                  ProducersFilter producerFilter, PageFilter pageFilter,
                                  [ModelBinder(typeof (GroupFilterBinder))] GroupFilter groupFilter)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.Url.Query))
                {
                    if (Request.Url.Query.Contains("type"))
                    {
                        return RedirectToAction("index", "error", new {statusCode = 404});
                    }
                }

                ViewModel = new FilterViewModelBase(category, pageSizeFilter, sortFilter, producerFilter, pageFilter,
                                                    groupFilter);
                IQueryable<Product> entities = productRepository.GetProductsWithProducerByProductName(category);
                entities = StartInitialize(entities);
                EndInitialize(entities);
                ViewModel.CategoryObj = categoryRepository.GetByName(category);
                return View(ViewModel);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return RedirectToAction("index", "error", new {statusCode = 404});
            }
        }

        public ActionResult Details(string category, string name)
        {
            if (!string.IsNullOrEmpty(Request.Url.Query))
            {
                if (Request.Url.Query.Contains("type"))
                {
                    return RedirectToAction("index", "error", new {statusCode = 404});
                }
            }

            Product product = productRepository.Details(category, name);
            if (product == null)
            {
                return RedirectToAction("index", "error", new {statusCode = 404});
            }

            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.Add(product);
                using (UnitOfWork)
                {
                    UnitOfWork.Commit();
                }
                return RedirectToAction("index");
            }

            return View(product);
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(int id)
        {
            Product product = productRepository.GetWithCategory(id);

            if (product == null)
            {
                return RedirectToAction("index", "error", new {statusCode = 404});
            }

            return View(new ProductEditModel(product));
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(ProductEditModel product)
        {
            Product currentProduct = productRepository.Find(product.Id);
            if (currentProduct == null)
            {
                return RedirectToAction("index", "error", new {statusCode = 404});
            }

            var entity = product.ToEntity(currentProduct).MarkAsEdited();
            productRepository.Update(entity);
            using (UnitOfWork)
            {
                UnitOfWork.Commit();
            }

            return RedirectToAction("details", new {category = currentProduct.CategoryName, name = currentProduct.Name});
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdminRoleName)]
        public JsonResult Delete(int id)
        {
            Product product = productRepository.Find(id);
            var categoryName = product.CategoryName;
            productRepository.Remove(product);
            using (UnitOfWork)
            {
                UnitOfWork.Commit();
            }

            var url = Url.RouteUrl("categoryAction",
                                   new RouteValueDictionary(new {category = categoryName, action = "index"}),
                                   Request.Url.Scheme, Request.Url.Host);
            return new JsonResult { Data =  url, ContentEncoding = Encoding.UTF8};
        }
    }
}