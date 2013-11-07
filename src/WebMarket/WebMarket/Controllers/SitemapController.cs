using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace WebMarket.Controllers
{
    public class SitemapController : ControllerBase
    {
        public ActionResult Index()
        {
            var xdoc = new XDocument();

            var rootns = XNamespace.Get("http://www.sitemaps.org/schemas/sitemap/0.9");
            var imagens = XNamespace.Get("http://www.google.com/schemas/sitemap-image/1.1");

            var root = new XElement(rootns + "urlset");
            root.Add(new XAttribute(XNamespace.Xmlns + "image", imagens));

            var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    OmitXmlDeclaration = false,
                    ConformanceLevel = ConformanceLevel.Document,
                    Indent = true
                };

            var products = DbContext.Products.ToList();

            var host = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
            var links = products.Select(obj => new
                {
                    Url = string.Format("{0}/{1}/details/{2}", host, obj.CategoryName, obj.Name),
                    Photo = string.IsNullOrEmpty(obj.GetPreview()) ? null : string.Format("{0}/Content/tiles/{1}/{2}", host, obj.CategoryName, obj.GetPreview())
                });

            foreach (var link in links)
            {
                var url = new XElement(rootns + "url");
                url.Add(new XElement(rootns + "loc", link.Url));
                if (!string.IsNullOrEmpty(link.Photo))
                {
                    var photo = new XElement(imagens + "image");
                    photo.Add(new XElement(imagens + "loc", link.Photo));
                    url.Add(photo);
                }
                
                root.Add(url);
            }

            xdoc.Add(root);
            var sb = new StringBuilder();
            using (var xw = XmlWriter.Create(sb, settings))
            {
                xdoc.Save(xw);
            }

            return Content(sb.ToString(), "text/xml");
        }
    }
}
