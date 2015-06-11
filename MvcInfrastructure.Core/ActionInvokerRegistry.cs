namespace MvcInfrastructure.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Diagnostics;
    using Microsoft.Practices.Unity.Utility;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using System.Web.Mvc.Async;

    public class ActionInvokerRegistry
    {
        private readonly IDictionary<Type, Type> syncMappings = new Dictionary<Type, Type>();
        private readonly IDictionary<Type, Type> asyncMappings = new Dictionary<Type, Type>();

        public IUnityContainer Container
        {
            get;
            private set;
        }

        public ActionInvokerRegistry(IUnityContainer container)
        {
            Container = container;
        }

        public void RegisterSyncActionInvoker<TController, TActionInvoker>()
            where TController : Controller
            where TActionInvoker : ExtendedSyncControllerActionInvoker
        {
            Type controllerType = typeof(TController);
            Type actionInvokerType = typeof(TActionInvoker);

            if (!syncMappings.ContainsKey(controllerType))
                syncMappings.Add(controllerType, actionInvokerType);
        }

        public void RegisterAsyncActionInvoker<TController, TActionInvoker>()
            where TController : Controller
            where TActionInvoker : ExtendedAsyncControllerActionInvoker
        {
            Type controllerType = typeof(TController);
            Type actionInvokerType = typeof(TActionInvoker);

            if (!asyncMappings.ContainsKey(controllerType))
                asyncMappings.Add(controllerType, actionInvokerType);
        }

        public Type Matching(Type controllerType)
        {
            Guard.ArgumentNotNull(controllerType, "controllerType");

            var controller = Container.Resolve(controllerType) as Controller;

            if (controller is IAsyncController && syncMappings.ContainsKey(controllerType))
            {
                return asyncMappings[controllerType];
            }
            else if (syncMappings.ContainsKey(controllerType))
            {
                return syncMappings[controllerType];
            }
            else
            {
                return controller is IAsyncController ? KnownTypes.AsyncActionInvokerType : KnownTypes.SyncActionInvokerType;
            }
        }
    }
}
