using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.DAL.Data.Import;
using WebMarket.Filters;

namespace WebMarket.Controllers
{
    [InitializeSimpleMembership]
    public class AdminController : ControllerBase
    {
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
                var importer = new DataImporter();
                foreach (var file in files.Where(obj => obj.ContentLength > 0))
                {
                    importer.Import(file.InputStream, this.DbContext);
                }

                this.DbContext.SaveChanges();
            }
            catch (Exception e)
            {
                this.Logger.Error(e);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Logs()
        {
            string str;
            using (var stream = new FileStream(Server.MapPath("../info.log"), FileMode.Open, FileAccess.Read, FileShare.None))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    str = streamReader.ReadToEnd();
                }
            }

            return View((object)str);
        }

        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Callbacks()
        {
            var callbacks = DbContext.Callbacks.ToList();
            return View(callbacks);
        }
    }
}
