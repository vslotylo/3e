using System;
using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.Repository.Entities;
using WebMarket.Repository.Entities.Enums;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly ICallbackRepository callbackRepository;

        public HomeController(ICallbackRepository callbackRepository)
        {
            this.callbackRepository = callbackRepository;
        }

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
            return this.View();
        }

        [HttpPost]
        public void Callback(string phone, string url)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return;
            }

            var creationTime = DateTime.Now.ToUkrainianTimeZone();
            var telephone = phone.Trim();
            try
            {
                callbackRepository.Insert(new Callback { CreateTime = creationTime, Phone = telephone, Status = Status.Pending, Url = url });
                callbackRepository.Commit();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }
    }
}
