namespace MvcInfrastructure.Core
{
    using System;
    using System.Web.Mvc;
    using System.Web.Mvc.Async;
    using System.Web.Routing;
    using Microsoft.Practices.Unity;

    public class ControllerFactory : DefaultControllerFactory
    {
        public ControllerFactory(IUnityContainer container)
        {
            Container = container;
        }

        protected IUnityContainer Container
        {
            get;
            private set;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return CreateController(controllerType) ?? base.GetControllerInstance(requestContext, controllerType);
        }

        private Controller CreateController(Type controllerType)
        {
            Controller controller = null;

            if (controllerType != null)
            {
                controller = Container.Resolve(controllerType) as Controller;
                if (!(controller is DefaultController))
                    throw new Exception("所有Controller必须继承DefaultController");
                
                ActionInvokerRegistry actionInvokerRegistry = Container.Resolve<ActionInvokerRegistry>();
                Type actionInvokerType = actionInvokerRegistry.Matching(controllerType);
                controller.ActionInvoker = Container.Resolve(actionInvokerType) as IActionInvoker;
                controller.TempDataProvider = new NullTempDataProvider();
            }

            return controller;
        }
    }
}
