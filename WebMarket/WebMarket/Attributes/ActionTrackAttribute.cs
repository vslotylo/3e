using System.Web.Mvc;
using log4net;

namespace WebMarket.Attributes
{
    public class ActionTrackAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;
            var logger = LogManager.GetLogger(request.Url.ToString());
            logger.Info(string.Format("{0} {1} {2} {3}",request.UserHostAddress, request.UserHostName, request.UserHostAddress, request.UserAgent));
            base.OnActionExecuted(filterContext);
        }
    }
}