using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMarket.Common;
using WebMarket.DAL.Data.Import;

namespace WebMarket.Controllers
{
    public class AdminController : ControllerBase
    {
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Index()
        {
            var count = HttpContext.Session.Count;
            return View(count);
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
                this.logger.Error(e);
            }
            
            return RedirectToAction("Index");
        }
    }
}
