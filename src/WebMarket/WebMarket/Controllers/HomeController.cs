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
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public void Callback(string phone, string url)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return;
            }

            DateTime creationTime = DateTime.Now.ToUkrainianTimeZone();
            string telephone = phone.Trim();
            try
            {
                callbackRepository.Add(new Callback
                {
                    CreateTime = creationTime,
                    Phone = telephone,
                    Status = Status.Pending,
                    Url = url
                });

                using (UnitOfWork)
                {
                    UnitOfWork.Commit();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }
    }
}