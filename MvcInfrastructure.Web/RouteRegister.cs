using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcInfrastructure.Core;
using System.Web.Mvc;
using System.Web.Routing;
using MvcInfrastructure.Library.RouteConstraints;

namespace MvcInfrastructure.Sample.Web
{
    public class RouteRegister : RouteBaseRegister
    {     

        protected override Action<RouteCollection> Register()
        {
            return routes =>
            {
                routes.MapRoute(
                  "rtest",
                  "Home.mvc/Index",
                   new { controller = "Home", action = "Route"});

            };

        }
    }
}
