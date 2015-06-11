namespace MvcInfrastructure.Library.RouteConstraints
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Routing;
    using Microsoft.Practices.Unity.Utility;
    using System.Text.RegularExpressions;

    public class RegexConstraint : IRouteConstraint
    {
        private readonly Regex expression;
        private readonly bool optional;

        public RegexConstraint(string expression)
            : this(expression, false)
        {
        }

        public RegexConstraint(string expression, bool optional)
        {
            Guard.ArgumentNotNullOrEmpty(expression, "expression");

            string pattern = "^(" + expression + ")$";

            this.expression = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            this.optional = optional;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            Guard.ArgumentNotNull(values, "values");

            if (values.ContainsKey(parameterName))
            {
                object value = values[parameterName];

                return value == null ? optional : expression.IsMatch(value.ToString());
            }

            return optional;
        }
    }
}
