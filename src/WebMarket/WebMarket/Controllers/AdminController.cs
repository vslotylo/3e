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
        private readonly ICallbackRepository callbackRepository;
        private readonly IDataImporter dataImporter;
        private readonly string[] supportedLogLevels = new[] {"error", "info"};

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
                foreach (HttpPostedFileBase file in files.Where(obj => obj.ContentLength > 0))
                {
                    dataImporter.Import(file.InputStream);
                }

                dataImporter.Commit();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Logs(string logLevel)
        {
            string level = logLevel.Trim().ToLower();
            if (!supportedLogLevels.Contains(level))
            {
                return View(model: "Log level is not supported");
            }

            string str;
            using (
                var stream = new FileStream(Server.MapPath(string.Format("../logs/{0}.log", logLevel)), FileMode.Open,
                                            FileAccess.Read, FileShare.None))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    str = streamReader.ReadToEnd();
                }
            }

            string formatted = str.Replace("\r\n", "<br/>");
            return View(model: formatted);
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Callbacks()
        {
            return View(callbackRepository.All());
        }
    }
}