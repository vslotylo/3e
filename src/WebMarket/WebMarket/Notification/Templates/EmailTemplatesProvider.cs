using System.Web.Hosting;
using System.Xml.Linq;

namespace WebMarket.Notification.Templates
{
    public static class EmailTemplatesProvider
    {
        private static readonly EmailTemplate CustomerOrderTemplate;

        static EmailTemplatesProvider()
        {
            var doc = XDocument.Load(HostingEnvironment.MapPath("~/Notification/Templates/CustomerOrder.msgtmpl")).Root;
            CustomerOrderTemplate = new EmailTemplate(doc.Element("bodytemplate").Value.Trim(), doc.Element("subject").Value.Trim());
        }

        public static EmailTemplate GetCustomerOrderTemplate(string userName, double totalSum)
        {
            var body = CustomerOrderTemplate.Body.Replace("@userName", userName);
            body = body.Replace("@totalSum", string.Format("{0:0.##}", totalSum));
            return new EmailTemplate(CustomerOrderTemplate.Subject, body);
        }
    }
}