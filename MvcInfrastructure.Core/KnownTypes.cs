namespace MvcInfrastructure.Core
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class KnownTypes
    {

        public static readonly Type BootstrapperTaskType = typeof(BootstrapperTask);

        public static readonly Type PerRequestTaskType = typeof(PerRequestTask);

        public static readonly Type ControllerFactoryType = typeof(ControllerFactory);

        public static readonly Type ControllerType = typeof(Controller);

        public static readonly Type FilterAttributeType = typeof(FilterAttribute);

        public static readonly Type SyncActionInvokerType = typeof(ExtendedSyncControllerActionInvoker);

        public static readonly Type AsyncActionInvokerType = typeof(ExtendedAsyncControllerActionInvoker);

        public static readonly Type ActionResultType = typeof(ActionResult);

        public static readonly Type ActionInvokerType = typeof(IActionInvoker);

        public static readonly Type RouteHandlerType = typeof(IRouteHandler);

        public static readonly Type ModelBinderType = typeof(IModelBinder);

        public static readonly Type ValueProviderFactoryType = typeof(ValueProviderFactory);
       
    }
}
