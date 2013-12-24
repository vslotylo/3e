using System.Web.Mvc;

namespace WebMarket.Controllers
{
    public class SEOController : Controller
    {
        public ActionResult Removed()
        {
            Response.Headers.Add("X-Robots-Tag","noindex");
            return View();
        }
    }
}
