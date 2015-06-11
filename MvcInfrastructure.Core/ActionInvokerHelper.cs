namespace MvcInfrastructure.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;

    public static class ActionInvokerHelper
    {
        public static FilterInfo Merge(IUnityContainer container, FilterInfo decoratedFilters, FilterInfo registeredFilters)
        {
            FilterInfo mergedFilters = new FilterInfo();
            MergeFilterAttriute(mergedFilters, decoratedFilters, registeredFilters);
            MergeNonFilterAttriute(mergedFilters, decoratedFilters, registeredFilters);
            return mergedFilters;
        }

        private static void MergeFilterAttriute(FilterInfo mergedFilters, FilterInfo decoratedFilters, FilterInfo registeredFilters)
        {
            Merge(decoratedFilters.AuthorizationFilters, registeredFilters.AuthorizationFilters, IsFilterAttriute)
                .Cast<FilterAttribute>()
                .OrderBy(filter => filter.Order)
                .Cast<IAuthorizationFilter>().Each(filter => mergedFilters.AuthorizationFilters.Add(filter));

            Merge(decoratedFilters.ActionFilters, registeredFilters.ActionFilters, IsFilterAttriute)
                .Cast<FilterAttribute>()
                .OrderBy(filter => filter.Order)
                .Cast<IActionFilter>().Each(filter => mergedFilters.ActionFilters.Add(filter));

            Merge(decoratedFilters.ResultFilters, registeredFilters.ResultFilters, IsFilterAttriute)
                .Cast<FilterAttribute>()
                .OrderBy(filter => filter.Order)
                .Cast<IResultFilter>().Each(filter => mergedFilters.ResultFilters.Add(filter));

            Merge(decoratedFilters.ExceptionFilters, registeredFilters.ExceptionFilters, IsFilterAttriute)
                .Cast<FilterAttribute>()
                .OrderBy(filter => filter.Order)
                .Cast<IExceptionFilter>().Each(filter => mergedFilters.ExceptionFilters.Add(filter));
        }

        private static void MergeNonFilterAttriute(FilterInfo mergedFilters, FilterInfo decoratedFilters, FilterInfo registeredFilters)
        {
            Merge(decoratedFilters.AuthorizationFilters, registeredFilters.AuthorizationFilters, IsNonFilterAttriute)
                .Reverse().Each(filter => mergedFilters.AuthorizationFilters.Insert(0, filter));

            Merge(decoratedFilters.ActionFilters, registeredFilters.ActionFilters, IsNonFilterAttriute)
                .Reverse().Each(filter => mergedFilters.ActionFilters.Insert(0, filter));

            Merge(decoratedFilters.ResultFilters, registeredFilters.ResultFilters, IsNonFilterAttriute)
                .Reverse().Each(filter => mergedFilters.ResultFilters.Insert(0, filter));

            Merge(decoratedFilters.ExceptionFilters, registeredFilters.ExceptionFilters, IsNonFilterAttriute)
                .Reverse().Each(filter => mergedFilters.ExceptionFilters.Insert(0, filter));
        }

        private static IEnumerable<T> Merge<T>(IEnumerable<T> source1, IEnumerable<T> source2, Func<T, bool> filter)
        {
            return source1.Where(filter).Concat(source2.Where(filter));
        }

        private static bool IsFilterAttriute<TFilter>(TFilter filter)
        {
            return KnownTypes.FilterAttributeType.IsAssignableFrom(filter.GetType());
        }

        private static bool IsNonFilterAttriute<TFilter>(TFilter filter)
        {
            return !KnownTypes.FilterAttributeType.IsAssignableFrom(filter.GetType());
        }
    }
}
