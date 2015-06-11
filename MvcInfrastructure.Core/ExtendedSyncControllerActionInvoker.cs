namespace MvcInfrastructure.Core
{
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Utility;
    using System;

    public class ExtendedSyncControllerActionInvoker : ControllerActionInvoker
    {
        protected IUnityContainer Container
        {
            get;
            private set;
        }

        public ExtendedSyncControllerActionInvoker(IUnityContainer container)
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

        protected override ActionDescriptor FindAction(ControllerContext controllerContext, ControllerDescriptor controllerDescriptor, string actionName)
        {
            ActionDescriptor foundAction;

            try
            {
                foundAction = base.FindAction(controllerContext, controllerDescriptor, actionName);
            }
            catch
            {
                foundAction = null;
            }

            if (foundAction == null)
            {
                foundAction = new DynamicActionDescriptor(actionName, controllerDescriptor);
            }

            return foundAction;
        }

        public override bool InvokeAction(ControllerContext controllerContext, string actionName)
        {
            var controller = controllerContext.Controller as DefaultController;
            if (controller != null)
            {
                var action = controller.GetDynamicAction(actionName);
                if (action != null)
                {
                    base.InvokeActionResult(controllerContext, action());
                    return true;
                }
            }
            
            base.InvokeAction(controllerContext, actionName);
            return true;
        }
    }
}
