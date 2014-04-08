using System;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.Core;
using WebMarket.Models;
using WebMarket.Repository.Entities;
using WebMarket.Filters;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Controllers
{
    public class ProductController : ListControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public ActionResult Index(string category, PageSizeFilter pageSizeFilter, SortFilter sortFilter, ProducersFilter producerFilter, PageFilter pageFilter, [ModelBinder(typeof(GroupFilterBinder))] GroupFilter groupFilter)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Request.Url.Query))
                {
                    if (this.Request.Url.Query.Contains("type"))
                    {
                        return this.RedirectToAction("index", "error", new { statusCode = 404 });
                    }
                }

                this.ViewModel = new FilterViewModelBase(category, pageSizeFilter, sortFilter, producerFilter, pageFilter, groupFilter);
                var entities = this.productRepository.GetProductsWithProducerByProductName(category);
                entities = this.StartInitialize(entities);
                this.EndInitialize(entities);
                this.ViewModel.CategoryObj = categoryRepository.GetByName(category);
                return this.View(this.ViewModel);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return this.RedirectToAction("index", "error", new { statusCode = 404 });
            }
        }

        public ActionResult Details(string category, string name)
        {
            if (!string.IsNullOrEmpty(this.Request.Url.Query))
            {
                if (this.Request.Url.Query.Contains("type"))
                {
                    return this.RedirectToAction("index", "error", new { statusCode = 404 });
                }
            }

            var product = productRepository.Details(category, name);
            if (product == null)
            {
                return this.RedirectToAction("index", "error", new { statusCode = 404 });
            }

            return this.View(product);
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Create(Product product)
        {
            if (this.ModelState.IsValid)
            {
                this.productRepository.Insert(product);
                this.productRepository.Commit();
                return this.RedirectToAction("index");
            }

            return this.View(product);
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(int id)
        {
            var product = productRepository.GetWithCategory(id);
            
            if (product == null)
            {
                return this.RedirectToAction("index", "error", new { statusCode = 404 });
            }

            return this.View(new ProductEditModel(product));
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(ProductEditModel product)
        {
            var currentProduct = productRepository.Get(product.Id);
            if (currentProduct == null)
            {
                return this.RedirectToAction("index", "error", new { statusCode = 404 });
            }

            productRepository.Update(product.ToEntity(currentProduct));
            productRepository.Commit();
            return this.RedirectToAction("details", new { category = currentProduct.CategoryName, name = product.Name });
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Delete(int id = 0)
        {
            var product = this.productRepository.Get(id);
            if (product == null)
            {
                return this.RedirectToAction("index", "error");
            }
            return this.View(product);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = this.productRepository.Get(id);
            this.productRepository.Delete(product);
            this.productRepository.Commit();
            return this.RedirectToAction("index");
        }
    }
}
