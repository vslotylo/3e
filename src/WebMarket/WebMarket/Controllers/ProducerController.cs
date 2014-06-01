using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.Models;
using WebMarket.Repository.Current;
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
            var producer = producerRepository.Find(name);
            return View(producer);
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Create()
        {
            return View(new ProducerEditModel());
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Create(ProducerEditModel producer)
        {
            if (ModelState.IsValid)
            {
                producerRepository.Add(producer.ToEntity(null));
                using (UnitOfWork)
                {
                    UnitOfWork.Commit();
                }
                return RedirectToAction("index");
            }

            return View(producer);
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(int id)
        {
            var entity = producerRepository.Find(id);
            return View(new ProducerEditModel(entity));
        }

        [HttpPost]
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(ProducerEditModel producer)
        {
            if (ModelState.IsValid)
            {
                producerRepository.Update(producer.ToEntity(null));
                using (UnitOfWork)
                {
                    UnitOfWork.Commit();
                }
                return RedirectToAction("index");
            }

            return View(producer);
        }
    }
}
