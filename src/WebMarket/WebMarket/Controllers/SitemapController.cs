using System;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Controllers
{
    public class SitemapController : ControllerBase
    {
        private readonly XNamespace rootns = XNamespace.Get("http://www.sitemaps.org/schemas/sitemap/0.9");
        private readonly XNamespace imagens = XNamespace.Get("http://www.google.com/schemas/sitemap-image/1.1");
        private readonly IProductRepository productRepository;

        public SitemapController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ActionResult Index()
        {
            var xdoc = new XDocument();
            var root = new XElement(rootns + "urlset");
            root.Add(new XAttribute(XNamespace.Xmlns + "image", imagens));

            var products = productRepository.GetAll();

            var host = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
            // TODO links should end with /
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

            xdoc.Declaration = new XDeclaration("1.0", "utf-8", null);
            xdoc.Add(root);
           
            return Content(string.Format("{0}{1}{2}", xdoc.Declaration, Environment.NewLine, xdoc), "text/xml");
        }
    }
}
