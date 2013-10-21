using System.Web.Mvc;
using WebMarket.Filters;

namespace WebMarket.Controllers
{
    public class GroupFilterBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof (GroupFilter))
            {
                var grValue = controllerContext.HttpContext.Request.Params.Get("gr");
                if (string.IsNullOrEmpty(grValue))
                {
                    return base.BindModel(controllerContext, bindingContext);
                }

                return new GroupFilter {Group = grValue};
            }

            return base.BindModel(controllerContext, bindingContext);
        }
}
}
