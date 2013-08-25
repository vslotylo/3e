using System.Data;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Hosting;
using System.Web.UI;
using System.Xml.Linq;
using WebMarket.DAL.Common;
using WebMarket.DAL.Entities;

namespace WebMarket.Notification.Templates
{
    public static class EmailTemplatesProvider
    {
        private static readonly EmailTemplate CustomerOrderTemplate;
        private static readonly EmailTemplate SalesTemplate;

        static EmailTemplatesProvider()
        {
            var doc = XDocument.Load(HostingEnvironment.MapPath("~/Notification/Templates/CustomerOrder.msgtmpl")).Root;
            CustomerOrderTemplate = new EmailTemplate(doc.Element("bodytemplate").Value.Trim(), doc.Element("subject").Value.Trim());
            doc = XDocument.Load(HostingEnvironment.MapPath("~/Notification/Templates/SalesNotification.msgtmpl")).Root;
            SalesTemplate = new EmailTemplate(doc.Element("bodytemplate").Value.Trim(), doc.Element("subject").Value.Trim());
        }

        public static EmailTemplate GetCustomerOrderTemplate(string userName, double totalSum)
        {
            var body = CustomerOrderTemplate.Body.Replace("@userName", userName);
            body = body.Replace("@totalSum", string.Format("{0:0.##}", totalSum));
            return new EmailTemplate(body, CustomerOrderTemplate.Subject);
        }

        public static EmailTemplate GetSalesTemplate(Order order)
        {
            var body = SalesTemplate.Body.Replace("@userName", order.User.Trim());
            body = body.Replace("@userEmail", order.Email.Trim());
            body = body.Replace("@userCity", order.City.Trim());
            body = body.Replace("@userAddress", order.Address.Trim());
            var culture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("uk-UA");
            body = body.Replace("@orderCreationDate", order.CreationDate.ToString("G"));
            Thread.CurrentThread.CurrentCulture = culture;
            body = body.Replace("@userPhone", order.Phone.Trim());
            body = body.Replace("@userComment", order.Comment);

            var context = new WebMarketDbContext();
            var ids = order.Items.Select(obj => obj.ProductId);
            var products = context.Products.Where(obj => ids.Contains(obj.Id)).ToList();
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
            return new EmailTemplate(body, SalesTemplate.Subject);
        }
    }
}