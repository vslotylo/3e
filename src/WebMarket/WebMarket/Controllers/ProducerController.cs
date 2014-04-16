using System.Web.Mvc;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Controllers
{
    public class ProducerController : ControllerBase
    {
        private readonly IProducerRepository producerRepository;

        public ProducerController(IProducerRepository producerRepository)
        {
            this.producerRepository = producerRepository;
        }

        public ActionResult Index(string name)
        {
            var producers = producerRepository.All();
            return View(producers);
        }

        public ActionResult Details(string name)
        {
            var producer = producerRepository.GetByName(name);
            return View(producer);
        }
    }
}
