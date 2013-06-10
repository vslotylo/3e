using System.Web.Mvc;
using log4net;

namespace WebMarket.Attributes
{
    public class ActionTrackAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var logger = LogManager.GetLogger(this.GetType());
            var request = filterContext.RequestContext.HttpContext.Request;
            logger.Info(string.Format("{0} {1} {2}", request.UserHostName, request.UserHostAddress, request.UserAgent));
            base.OnActionExecuted(filterContext);
        }
    }
}