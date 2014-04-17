using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Hosting;
using System.Web.UI;
using System.Xml.Linq;
using WebMarket.Repository.Entities;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Notification.Templates
{
    public class EmailTemplatesProvider
    {
        private readonly EmailTemplate customerOrderTemplate;
        private readonly IProductRepository productRepository;
        private readonly EmailTemplate salesTemplate;

        public EmailTemplatesProvider(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            XElement doc =
                XDocument.Load(HostingEnvironment.MapPath("~/Notification/Templates/CustomerOrder.msgtmpl")).Root;
            customerOrderTemplate = new EmailTemplate(doc.Element("bodytemplate").Value.Trim(),
                                                      doc.Element("subject").Value.Trim());
            doc = XDocument.Load(HostingEnvironment.MapPath("~/Notification/Templates/SalesNotification.msgtmpl")).Root;
            salesTemplate = new EmailTemplate(doc.Element("bodytemplate").Value.Trim(),
                                              doc.Element("subject").Value.Trim());
        }

        public EmailTemplate GetCustomerOrderTemplate(string userName, double totalSum)
        {
            string body = customerOrderTemplate.Body.Replace("@userName", userName);
            body = body.Replace("@totalSum", string.Format("{0:0.##}", totalSum));
            return new EmailTemplate(body, customerOrderTemplate.Subject);
        }

        public EmailTemplate GetSalesTemplate(Order order)
        {
            string body = salesTemplate.Body.Replace("@userName",
                                                     string.IsNullOrEmpty(order.User) ? string.Empty : order.User.Trim());
            body = body.Replace("@userEmail", string.IsNullOrEmpty(order.Email) ? string.Empty : order.Email.Trim());
            body = body.Replace("@userCity", string.IsNullOrEmpty(order.City) ? string.Empty : order.City.Trim());
            body = body.Replace("@userAddress",
                                string.IsNullOrEmpty(order.Address) ? string.Empty : order.Address.Trim());
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("uk-UA");
            body = body.Replace("@orderCreationDate", order.CreationDate.ToString("G"));
            Thread.CurrentThread.CurrentCulture = culture;
            body = body.Replace("@userPhone", string.IsNullOrEmpty(order.Phone) ? string.Empty : order.Phone.Trim());
            body = body.Replace("@userComment",
                                string.IsNullOrEmpty(order.Comment) ? string.Empty : order.Comment.Trim());

            IEnumerable<int> ids = order.Items.Select(obj => obj.ProductId);
            IEnumerable<Product> products = productRepository.GetByIds(ids);
            using (var stringWriter = new StringWriter())
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                foreach (OrderItem item in order.Items)
                {
                    Product product = products.Single(obj => obj.Id == item.ProductId);
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(product.DisplayName);
                    writer.RenderEndTag();
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(item.Quantity);
                    writer.RenderEndTag();
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write("{0} грн.", item.UnitPrice);
                    writer.RenderEndTag();
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write("{0} грн.", item.TotalItemPrice);
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }

                body = body.Replace("@orderItemsContent", stringWriter.ToString());
            }


            body = body.Replace("@totalSum", string.Format("{0:0.##}", order.Total));
            return new EmailTemplate(body, salesTemplate.Subject);
        }
    }
}