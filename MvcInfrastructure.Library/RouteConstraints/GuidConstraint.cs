namespace MvcInfrastructure.Library.RouteConstraints
{
    using System;
    using System.Web;
    using System.Web.Routing;
    using Microsoft.Practices.Unity.Utility;

    public class GuidConstraint : IRouteConstraint
    {
        private readonly bool optional;

        public GuidConstraint()
            : this(false)
        {
        }

        public GuidConstraint(bool optional)
        {
            this.optional = optional;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            Guard.ArgumentNotNull(values, "values");

            if (values.ContainsKey(parameterName))
            {
                object value = values[parameterName];

                if (value == null)
                {
                    return optional;
                }

                bool matched = false;

                try
                {
                    Guid guid = new Guid(value.ToString());

                    matched = guid != Guid.Empty;
                }
                catch
                {
                }

                return matched;
            }

            return optional;
        }
    }
}
