using System.Web.Mvc;
using System.Web.Routing;

namespace WebMarket
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("default", "{controller}/{action}",
                new { controller = "home", action = "index" }, 
                new RouteValueDictionary
                    {
                        { "controller", "home|account|admin|cart|error|order|search|sitemap|seo|producer" }
                    });

            routes.MapRoute("categoryAction", "{category}/{action}/{name}", 
                new {controller = "product", action = "index", name = UrlParameter.Optional},
                new RouteValueDictionary
                    {
                        {"action", "index|edit|create|details|delete"}
                    });

            routes.MapRoute("categoryBase", "{category}/{producers}", new { controller = "product", action = "index", producers = UrlParameter.Optional });
            
        }
    }
}