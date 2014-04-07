using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.Repository.Data.Import;
using WebMarket.Repository.Interfaces;

namespace WebMarket.Controllers
{
    public class AdminController : ControllerBase
    {
        private readonly IDataImporter dataImporter;
        private readonly ICallbackRepository callbackRepository;
        private readonly string[] supportedLogLevels = new[] { "error", "info" };

        public AdminController(IDataImporter dataImporter, ICallbackRepository callbackRepository)
        {
            this.dataImporter = dataImporter;
            this.callbackRepository = callbackRepository;
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Import()
        {
            return View();
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                foreach (var file in files.Where(obj => obj.ContentLength > 0))
                {
                    this.dataImporter.Import(file.InputStream);
                }

                this.dataImporter.Comit();
            }
            catch (Exception e)
            {
                this.Logger.Error(e);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Logs(string logLevel)
        {
            var level = logLevel.Trim().ToLower();
            if (!supportedLogLevels.Contains(level))
            {
                return View(model:"Log level is not supported");
            }

            string str;
            using (var stream = new FileStream(Server.MapPath(string.Format("../logs/{0}.log", logLevel)), FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    str = streamReader.ReadToEnd();
                }
            }

            var formatted = str.Replace("\r\n", "<br/>");
            return View(model:formatted);
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Callbacks()
        {
            return View(this.callbackRepository.GetAll());
        }
    }
}
