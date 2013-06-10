using System.Web.Mvc;
using ControllerBase = WebMarket.Controllers.ControllerBase;

namespace WebMarket.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            this.ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return this.View();
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your quintessential app description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your quintessential contact page.";

            return this.View();
        }
    }
}
