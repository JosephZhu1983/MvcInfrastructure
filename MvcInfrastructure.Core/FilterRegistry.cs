namespace MvcInfrastructure.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Utility;

    public class FilterRegistry
    {
        private readonly IList<FilterRegistryItem> items;

        public FilterRegistry(IUnityContainer container)
        {
            Guard.ArgumentNotNull(container, "container");

            Container = container;
            items = new List<FilterRegistryItem>();
        }

        public IUnityContainer Container
        {
            get;
            private set;
        }

        public IList<FilterRegistryItem> Items
        {
            [DebuggerStepThrough]
            get
            {
                return items;
            }
        }

        public virtual FilterRegistry Register<TController, TFilter>(Func<TFilter> configFilter)
            where TController : Controller
            where TFilter : FilterAttribute
        {
            Guard.ArgumentNotNull(configFilter, "configFilter");

            Items.Add(new ControllerFilterRegistryItem<TController>(new Func<FilterAttribute>(() => configFilter())));

            return this;
        }

        public virtual FilterRegistry Register<TController, TFilter>(Expression<Action<TController>> action, Func<TFilter> configFilter)
            where TController : Controller
            where TFilter : FilterAttribute
        {
            Guard.ArgumentNotNull(action, "action");
            Guard.ArgumentNotNull(configFilter, "configFilter");

            Items.Add(new ActionFilterRegistryItem<TController>(action, new Func<FilterAttribute>(() => configFilter())));

            return this;
        }

        public virtual FilterInfo Matching(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            Guard.ArgumentNotNull(controllerContext, "controllerContext");
            Guard.ArgumentNotNull(actionDescriptor, "actionDescriptor");

            FilterInfo filterInfo = new FilterInfo();

            List<FilterAttribute> filters = new List<FilterAttribute>();
            Items.Each(item =>
            {
                if (item.IsMatching(controllerContext, actionDescriptor))
                {
                    var filter = item.ConfigFilter();
                    if (!filters.Contains(filter))
                        filters.Add(filter);
                }
            });

            filters.OfType<IAuthorizationFilter>()
                .Cast<FilterAttribute>().OrderBy(filter => filter.Order)
                .Cast<IAuthorizationFilter>().Each(filterInfo.AuthorizationFilters.Add);

            filters.OfType<IActionFilter>()
                .Cast<FilterAttribute>().OrderBy(filter => filter.Order)
               .Cast<IActionFilter>().Each(filterInfo.ActionFilters.Add);

            filters.OfType<IResultFilter>()
                .Cast<FilterAttribute>().OrderBy(filter => filter.Order)
                .Cast<IResultFilter>().Each(filterInfo.ResultFilters.Add);

            filters.OfType<IExceptionFilter>()
                .Cast<FilterAttribute>().OrderBy(filter => filter.Order)
                .Cast<IExceptionFilter>().Each(filterInfo.ExceptionFilters.Add);

            return filterInfo;
        }
    }
}
