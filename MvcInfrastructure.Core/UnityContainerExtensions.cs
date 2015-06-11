namespace MvcInfrastructure.Core
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Utility;

    public static class UnityContainerExtensions
    {
        [DebuggerStepThrough]
        public static IUnityContainer RegisterTypeAsSingleton(this IUnityContainer instance, Type fromType, Type toType)
        {
            Guard.ArgumentNotNull(instance, "instance");
            Guard.ArgumentNotNull(fromType, "fromType");
            Guard.ArgumentNotNull(toType, "toType");

            return instance.RegisterType(fromType, toType, toType.FullName, new ContainerControlledLifetimeManager());
        }

        [DebuggerStepThrough]
        public static IUnityContainer RegisterTypeAsSingleton<TFrom, TTo>(this IUnityContainer instance) where TTo : TFrom
        {
            Guard.ArgumentNotNull(instance, "instance");

            return instance.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }

        [DebuggerStepThrough]
        public static IUnityContainer RegisterTypeAsPerRequest(this IUnityContainer instance, Type fromType, Type toType)
        {
            Guard.ArgumentNotNull(instance, "instance");
            Guard.ArgumentNotNull(fromType, "fromType");
            Guard.ArgumentNotNull(toType, "toType");

            return instance.RegisterType(fromType, toType, toType.FullName, new PerRequestLifetimeManager());
        }

        [DebuggerStepThrough]
        public static IUnityContainer RegisterTypeAsPerRequest<TFrom, TTo>(this IUnityContainer instance) where TTo : TFrom
        {
            Guard.ArgumentNotNull(instance, "instance");

            return instance.RegisterType<TFrom, TTo>(new PerRequestLifetimeManager());
        }

        [DebuggerStepThrough]
        public static IUnityContainer RegisterTypeAsTransient(this IUnityContainer instance, Type fromType, Type toType)
        {
            Guard.ArgumentNotNull(instance, "instance");
            Guard.ArgumentNotNull(fromType, "fromType");
            Guard.ArgumentNotNull(toType, "toType");

            return instance.RegisterType(fromType, toType, toType.FullName, new TransientLifetimeManager());
        }

        [DebuggerStepThrough]
        public static IUnityContainer RegisterTypeAsTransient<TFrom, TTo>(this IUnityContainer instance) where TTo : TFrom
        {
            Guard.ArgumentNotNull(instance, "instance");

            return instance.RegisterType<TFrom, TTo>(new TransientLifetimeManager());
        }

        [DebuggerStepThrough]
        public static IUnityContainer RegisterInstanceAsTransient<TFrom>(this IUnityContainer instance, TFrom toObject)
        {
            Guard.ArgumentNotNull(instance, "instance");
            Guard.ArgumentNotNull(toObject, "toObject");
            return instance.RegisterInstance<TFrom>(toObject, new TransientLifetimeManager());
        }

        [DebuggerStepThrough]
        public static IUnityContainer RegisterInstanceAsPerRequest<TFrom>(this IUnityContainer instance, TFrom toObject)
        {
            Guard.ArgumentNotNull(instance, "instance");
            Guard.ArgumentNotNull(toObject, "toObject");
            return instance.RegisterInstance<TFrom>(toObject, new PerRequestLifetimeManager());
        }

        [DebuggerStepThrough]
        public static IUnityContainer RegisterInstanceAsSingleton<TFrom>(this IUnityContainer instance, TFrom toObject)
        {
            Guard.ArgumentNotNull(instance, "instance");
            Guard.ArgumentNotNull(toObject, "toObject");
            return instance.RegisterInstance<TFrom>(toObject, new ContainerControlledLifetimeManager());
        }

        [DebuggerStepThrough]
        public static T ResolveOne<T>(this IUnityContainer instance)
        {
            Guard.ArgumentNotNull(instance, "instance");

            return instance.ResolveAll<T>().FirstOrDefault();
        }
    }
}
