namespace MvcInfrastructure.Core
{
    using System.Web.Mvc;
    using System.Web.Mvc.Async;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Utility;
    using System;

    public class ExtendedAsyncControllerActionInvoker : AsyncControllerActionInvoker
    {
        protected IUnityContainer Container
        {
            get;
            private set;
        }

        public ExtendedAsyncControllerActionInvoker(IUnityContainer container)
        {
            Guard.ArgumentNotNull(container, "container");

            Container = container;
        }

        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            Guard.ArgumentNotNull(controllerContext, "controllerContext");
            Guard.ArgumentNotNull(actionDescriptor, "actionDescriptor");

            FilterInfo decoratedFilters = base.GetFilters(controllerContext, actionDescriptor);
            FilterInfo registeredFilters = Container.Resolve<FilterRegistry>().Matching(controllerContext, actionDescriptor);

            return ActionInvokerHelper.Merge(Container, decoratedFilters, registeredFilters);
        }
    }
}
