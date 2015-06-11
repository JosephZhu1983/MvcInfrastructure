namespace MvcInfrastructure.Core
{
    using System;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity.Utility;

    public abstract class FilterRegistryItem
    {
        protected FilterRegistryItem(Func<FilterAttribute> configFilter)
        {
            Guard.ArgumentNotNull(configFilter, "configFilter");

            ConfigFilter = configFilter;
        }

        public Func<FilterAttribute> ConfigFilter
        {
            get;
            protected set;
        }

        public abstract bool IsMatching(ControllerContext controllerContext, ActionDescriptor actionDescriptor);
    }
}
