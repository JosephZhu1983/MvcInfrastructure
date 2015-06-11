namespace MvcInfrastructure.Core
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.Practices.Unity;
    using System;

    public abstract class RouteBaseRegister : BootstrapperTask
    {
        public RouteBaseRegister()
        {
            Order = 1;
        }

        private Action<RouteCollection> action;
        protected abstract Action<RouteCollection> Register();

        private void RegisterInternal(IUnityContainer container, RouteCollection routes)
        {
            RouteConfigProvider.Init(container, routes, action);
        }

        public override TaskContinuation Execute(IUnityContainer container)
        {
            var routes = container.Resolve<RouteCollection>();

            action = Register();
            RegisterInternal(container, routes);
            return TaskContinuation.Continue;
        }
    }
}
