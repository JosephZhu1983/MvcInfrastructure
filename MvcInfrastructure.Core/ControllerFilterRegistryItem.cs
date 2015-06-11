namespace MvcInfrastructure.Core
{
    using System;
    using System.Web.Mvc;

    public class ControllerFilterRegistryItem<TController> : FilterRegistryItem where TController : Controller
    {
        private readonly Type controllerType = typeof(TController);

        public ControllerFilterRegistryItem(Func<FilterAttribute> configFilter)
            : base(configFilter)
        {
        }

        public override bool IsMatching(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            return (controllerContext != null) && controllerType.IsAssignableFrom(controllerContext.Controller.GetType());
        }
    }
}
