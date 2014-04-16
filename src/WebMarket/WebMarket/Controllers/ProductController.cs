﻿using System;
using System.Linq;
using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.Core;
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

            productRepository.Update(product.ToEntity(currentProduct));
            using (UnitOfWork)
            {
                UnitOfWork.Commit();
            }
            return RedirectToAction("details", new {category = currentProduct.CategoryName, name = product.Name});
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Delete(int id = 0)
        {
            Product product = productRepository.Find(id);
            if (product == null)
            {
                return RedirectToAction("index", "error");
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productRepository.Find(id);
            productRepository.Remove(product);
            using (UnitOfWork)
            {
                UnitOfWork.Commit();
            }
            return RedirectToAction("index");
        }
    }
}