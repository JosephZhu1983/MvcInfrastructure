namespace MvcInfrastructure.Library.RouteConstraints
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Routing;
    using Microsoft.Practices.Unity.Utility;

    public class EnumConstraint<TEnum> : IRouteConstraint
        where TEnum : IComparable, IFormattable, IConvertible
    {
        private readonly bool optional;

        public EnumConstraint()
            : this(false)
        {
        }

        public EnumConstraint(bool optional)
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

                return Enum.GetNames(typeof(TEnum)).Contains(value.ToString(), StringComparer.OrdinalIgnoreCase);
            }

            return optional;
        }
    }
}
