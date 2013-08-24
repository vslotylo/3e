using System.Web.Hosting;
using System.Xml.Linq;

namespace WebMarket.Notification.Templates
{
    public static class EmailTemplatesProvider
    {
        static EmailTemplatesProvider()
        {
            var doc = XDocument.Load(HostingEnvironment.MapPath("~/Notification/Templates/CustomerOrder.msgtmpl")).Root;
            CustomerOrderTemplate = new EmailTemplate { Body = doc.Element("bodytemplate").Value.Trim(), Subject = doc.Element("subject").Value.Trim() };
        }

        public static EmailTemplate CustomerOrderTemplate { get; private set; }
    }
}