namespace MvcInfrastructure.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Utility;

    public static class FilterRegistryExtensions
    {
        private static readonly Type genericControllerItemType = typeof(ControllerFilterRegistryItem<>);

        public static FilterRegistry RegisterOnController<TController, TFilter>(this FilterRegistry instance)
            where TController : Controller
            where TFilter : FilterAttribute
        {
            return RegisterOnController<TController, TFilter>(instance, (TFilter filter) => { });
        }

        public static FilterRegistry RegisterOnAction<TController, TFilter>(this FilterRegistry instance, Expression<Action<TController>> action)
            where TController : Controller
            where TFilter : FilterAttribute
        {
            Guard.ArgumentNotNull(instance, "instance");

            return RegisterOnAction<TController, TFilter>(instance, action, filter => { });
        }

        public static FilterRegistry RegisterOnController<TController, TFilter>(this FilterRegistry instance, Action<TFilter> configureFilter)
            where TController : Controller
            where TFilter : FilterAttribute
        {
            Guard.ArgumentNotNull(instance, "instance");
            Guard.ArgumentNotNull(configureFilter, "configureFilter");

            instance.Register<TController, FilterAttribute>(CreateAndConfigureFilter(instance, configureFilter));

            return instance;
        }

        public static FilterRegistry RegisterOnAction<TController, TFilter>(this FilterRegistry instance, Expression<Action<TController>> action, Action<TFilter> configureFilter)
            where TController : Controller
            where TFilter : FilterAttribute
        {
            Guard.ArgumentNotNull(instance, "instance");
            Guard.ArgumentNotNull(action, "action");
            Guard.ArgumentNotNull(configureFilter, "configureFilter");

            return instance.Register<TController, FilterAttribute>(action, CreateAndConfigureFilter(instance, configureFilter));
        }

        public static FilterRegistry RegisterOnGlobal<TFilter>(this FilterRegistry instance)
            where TFilter : FilterAttribute
        {
            Guard.ArgumentNotNull(instance, "instance");

            return RegisterOnGlobal<TFilter>(instance, (TFilter filter) => { });
        }

        public static FilterRegistry RegisterOnGlobal<TFilter>(this FilterRegistry instance, Action<TFilter> configureFilter)
            where TFilter : FilterAttribute
        {
            Guard.ArgumentNotNull(instance, "instance");
            Guard.ArgumentNotNull(configureFilter, "configureFilter");

            foreach (Type itemType in instance.Container.ResolveAll<Controller>().Select(c => c.GetType()).Select(controllerType => genericControllerItemType.MakeGenericType(controllerType)))
            {
                instance.Items.Add(Activator.CreateInstance(itemType, new object[] { CreateAndConfigureFilter(instance, configureFilter) }) as FilterRegistryItem);
            }

            return instance;
        }

        private static Func<FilterAttribute> CreateAndConfigureFilter<TFilter>(FilterRegistry registry, Action<TFilter> configureFilter) where TFilter : FilterAttribute
        {
            return new Func<FilterAttribute>(() =>
            {
                TFilter filter = registry.Container.ResolveOne<TFilter>();

                configureFilter(filter);

                return filter;
            });
        }
    }
}
