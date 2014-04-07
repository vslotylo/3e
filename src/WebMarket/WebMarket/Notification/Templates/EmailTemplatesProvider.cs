using System.IO;
using System.Linq;
using System.Globalization;
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
        private readonly EmailTemplate salesTemplate;
        private readonly IProductRepository productRepository;

        public EmailTemplatesProvider(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
            var doc = XDocument.Load(HostingEnvironment.MapPath("~/Notification/Templates/CustomerOrder.msgtmpl")).Root;
            this.customerOrderTemplate = new EmailTemplate(doc.Element("bodytemplate").Value.Trim(), doc.Element("subject").Value.Trim());
            doc = XDocument.Load(HostingEnvironment.MapPath("~/Notification/Templates/SalesNotification.msgtmpl")).Root;
            this.salesTemplate = new EmailTemplate(doc.Element("bodytemplate").Value.Trim(), doc.Element("subject").Value.Trim());
        }
        
        public EmailTemplate GetCustomerOrderTemplate(string userName, double totalSum)
        {
            var body = this.customerOrderTemplate.Body.Replace("@userName", userName);
            body = body.Replace("@totalSum", string.Format("{0:0.##}", totalSum));
            return new EmailTemplate(body, this.customerOrderTemplate.Subject);
        }

        public EmailTemplate GetSalesTemplate(Order order)
        {
            var body = this.salesTemplate.Body.Replace("@userName", string.IsNullOrEmpty(order.User) ? string.Empty : order.User.Trim());
            body = body.Replace("@userEmail", string.IsNullOrEmpty(order.Email) ? string.Empty : order.Email.Trim());
            body = body.Replace("@userCity", string.IsNullOrEmpty(order.City) ? string.Empty : order.City.Trim());
            body = body.Replace("@userAddress", string.IsNullOrEmpty(order.Address) ? string.Empty : order.Address.Trim());
            var culture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("uk-UA");
            body = body.Replace("@orderCreationDate", order.CreationDate.ToString("G"));
            Thread.CurrentThread.CurrentCulture = culture;
            body = body.Replace("@userPhone", string.IsNullOrEmpty(order.Phone) ? string.Empty : order.Phone.Trim());
            body = body.Replace("@userComment", string.IsNullOrEmpty(order.Comment) ? string.Empty : order.Comment.Trim());

            var ids = order.Items.Select(obj => obj.ProductId);
            var products = productRepository.GetByIds(ids);
            using(var stringWriter = new StringWriter())
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                foreach (var item in order.Items)
                {
                    var product = products.Single(obj => obj.Id == item.ProductId);
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
            return new EmailTemplate(body, this.salesTemplate.Subject);
        }
    }
}