namespace MvcInfrastructure.Library.RouteConstraints
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Routing;
    using Microsoft.Practices.Unity.Utility;

    public class RangeConstraint<TValue> : IRouteConstraint where TValue : IComparable, IConvertible
    {
        private readonly TValue min;
        private readonly TValue max;
        private readonly bool optional;

        public RangeConstraint(TValue min, TValue max)
            : this(min, max, false)
        {
        }

        public RangeConstraint(TValue min, TValue max, bool optional)
        {
            this.min = min;
            this.max = max;
            this.optional = optional;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            Guard.ArgumentNotNull(values, "values");

            if (values.ContainsKey(parameterName))
            {
                if (values[parameterName] == null)
                {
                    return optional;
                }

                TValue value;

                try
                {
                    value = (TValue)Convert.ChangeType(values[parameterName], typeof(TValue));
                }
                catch
                {
                    return false;
                }

                bool matched = (min.CompareTo(value) <= 0) && (max.CompareTo(value) >= 0);

                return matched;
            }

            return optional;
        }
    }
}
