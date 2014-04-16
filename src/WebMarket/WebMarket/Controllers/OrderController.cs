using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.Models;
using WebMarket.Notification;
using WebMarket.Notification.Templates;
using WebMarket.Repository.Entities;
using WebMarket.Repository.Entities.Enums;
using WebMarket.Repository.Interfaces;
using WebMarket.ViewModels;

namespace WebMarket.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;

        public OrderController(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }

        public ActionResult Index()
        {
            return View(Cart);
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult List()
        {
            return View(orderRepository.All());
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Details(int id)
        {
            Order order = orderRepository.Find(id);
            IEnumerable<Product> products =
                order.Items.Select(obj => productRepository.GetWithProducersGroups(obj.ProductId));
            return View(new OrderDetailViewModel(order, products));
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(int id)
        {
            Order order = orderRepository.Find(id);
            return View(order);
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                orderRepository.Update(order);
                using (UnitOfWork)
                {
                    UnitOfWork.Commit();
                }
                return RedirectToAction("list");
            }

            return View(order);
        }

        [HttpPost]
        public bool Add(Order order)
        {
            foreach (CartItem item in Cart.Items)
            {
                order.Items.Add(new OrderItem
                    {
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.Product.CalculatedPrice.PriceFinalUah
                    });
            }

            order.Status = Status.Pending;
            order.CreationDate = DateTime.Now.ToUkrainianTimeZone();
            orderRepository.Add(order);
            try
            {
                var templatesProvider = new EmailTemplatesProvider(productRepository);
                Task.Factory.StartNew(() =>
                    {
                        var message = new NotificationMessage
                            {
                                EmailTemplate = templatesProvider.GetCustomerOrderTemplate(order.User, order.Total),
                                To = new[] {order.Email}
                            };
                        NotificationManager.Current.Notify(message);
                        message = new NotificationMessage
                            {
                                EmailTemplate = templatesProvider.GetSalesTemplate(order),
                                To = new[] {Constants.SalesEmail}
                            };
                        NotificationManager.Current.Notify(message);
                    });

                using (UnitOfWork)
                {
                    UnitOfWork.Commit();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            RemoveCart();
            return true;
        }
    }
}