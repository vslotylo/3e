using System.Linq;
using System.Web.Mvc;

namespace WebMarket.Controllers
{
    public class ProducerController : ControllerBase
    {
        public ActionResult Index(string name)
        {
            var producers = DbContext.Producers.ToList();
            return View(producers);
        }

        public ActionResult Details(string name)
        {
            var producer = DbContext.Producers.FirstOrDefault(obj => obj.Name == name);
            return View(producer);
        }
    }
}
