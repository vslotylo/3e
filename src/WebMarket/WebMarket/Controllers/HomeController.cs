using System;
using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.DAL.Entities;

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
            return this.View();
        }

        [HttpPost]
        public void Callback(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return;
            }

            var creationTime = DateTime.UtcNow.ToUkrainianTimeZone();
            var telephone = phone.Trim();
            try
            {
                this.DbContext.Callbacks.Add(new Callback { CreateTime = creationTime, Phone = telephone });
                this.DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                // log error here
                throw e;
            }
        }
    }
}
