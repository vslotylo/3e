using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.Repository.Entities;
using WebMarket.Repository.Entities.Enums;
using WebMarket.Notification;
using WebMarket.Notification.Templates;
using WebMarket.Repository.Interfaces;
using WebMarket.ViewModels;

namespace WebMarket.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;

        public OrderController(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }

        public ActionResult Index()
        {
            return this.View(this.Cart);
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult List()
        {
            return this.View(orderRepository.GetAll());
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Details(int id)
        {
            var order = orderRepository.Get(id);
            var products = order.Items.Select(obj => productRepository.GetWithProducersGroups(obj.ProductId));
            return this.View(new OrderDetailViewModel(order, products));
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(int id)
        {
            var order = orderRepository.Get(id);
            return this.View(order);
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(Order order)
        {
            if (this.ModelState.IsValid)
            {
                orderRepository.Update(order);
                orderRepository.Commit();
                return this.RedirectToAction("list");
            }

            return this.View(order);
        }

        [HttpPost]
        public bool Add(Order order)
        {
            foreach (var item in this.Cart.Items)
            {
                order.Items.Add(new OrderItem { ProductId = item.Product.Id, Quantity = item.Quantity, UnitPrice = item.Product.CalculatedPrice.PriceFinalUah });
            }

            order.Status = Status.Pending;
            order.CreationDate = DateTime.Now.ToUkrainianTimeZone();
            this.orderRepository.Insert(order);
            try
            {
                var templatesProvider = new EmailTemplatesProvider(productRepository);
                Task.Factory.StartNew(() =>
                {
                    var message = new NotificationMessage { EmailTemplate = templatesProvider.GetCustomerOrderTemplate(order.User, order.Total), To = new[] { order.Email } };
                    NotificationManager.Current.Notify(message);
                    message = new NotificationMessage { EmailTemplate = templatesProvider.GetSalesTemplate(order), To = new[] { Constants.SalesEmail } };
                    NotificationManager.Current.Notify(message);
                });
                
                this.orderRepository.Commit();
            }
            catch (Exception e)
            {
                this.Logger.Error(e);
            }

            this.RemoveCart();
            return true;
        }
    }
}
