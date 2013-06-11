using System;
using System.IO;
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
        public ActionResult Upload(HttpPostedFileBase file)
        {
            try
            {
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    var importer = new DataImporter();
                    importer.Import(file.InputStream, this.DbContext);
                    this.DbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                this.logger.Error(e);
            }
            
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }
    }
}
