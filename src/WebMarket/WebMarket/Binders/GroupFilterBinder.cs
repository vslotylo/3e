using System.Web.Mvc;
using WebMarket.Filters;

namespace WebMarket.Binders
{
    public class GroupFilterBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof (GroupFilter))
            {
                var groupValue = controllerContext.HttpContext.Request.Params.Get("gr");
                if (string.IsNullOrEmpty(groupValue))
                {
                    return base.BindModel(controllerContext, bindingContext);
                }

                return new GroupFilter {Group = groupValue};
            }

            return base.BindModel(controllerContext, bindingContext);
        }
}
}
