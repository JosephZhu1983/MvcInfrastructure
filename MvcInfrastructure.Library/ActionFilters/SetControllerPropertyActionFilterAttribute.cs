namespace MvcInfrastructure.Library.ActionFilters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    public class SetControllerPropertyActionFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        public Dictionary<string, Func<object>> Properties = new Dictionary<string, Func<object>>();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller;

            foreach (var item in Properties)
            {
                var p = controller.GetType().GetProperty(item.Key, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                if (p != null)
                    p.SetValue(controller, Properties[item.Key](), null);
            }
        }
    }
}
