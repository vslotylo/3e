using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;

namespace WebMarket
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Session_Start(Object sender, EventArgs e)
        {
           // GlobalFilters.Filters.Add(new ActionTrackAttribute());
            Application.Lock();
            Application["sessioncounter"] = (int)Application["sessioncounter"] + 1;
            Application.UnLock();
            Session["init"] = 0;
        }
        
        protected void Session_End(Object sender, EventArgs e)
        {
            Application.Lock();
            Application["sessioncounter"] = (int)Application["sessioncounter"] - 1;
            Application.UnLock();
        }

        protected void Application_Start()
        {
            Application["sessioncounter"] = 0;
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            //ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder()); 
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = this.Server.GetLastError();

            if (exception == null)
            {
                return;
            }

            this.Server.ClearError();
            var logger = LogManager.GetLogger(typeof(MvcApplication));
            logger.Error(exception);
        }
    }
}