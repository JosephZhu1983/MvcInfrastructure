namespace MvcInfrastructure.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.CompilerServices;
    using System.Web.Routing;
    using System.Web.Mvc;
    using System.Web;
    using System.Web.Caching;
    using System.IO;
    using System.Xml.Linq;
    using Microsoft.Practices.Unity;

    public class RouteConfigProvider
    {
        private static readonly string ConfigFilePathDependencyKey = "ConfigFilePathDependencyKey";
        private static readonly string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config/route.config");
        private static RouteCollection routes;
        private static Action<RouteCollection> action;
        private static IUnityContainer container;

        [MethodImpl(MethodImplOptions.Synchronized)]
        internal static void Init(IUnityContainer c, RouteCollection r, Action<RouteCollection> a)
        {
            routes = r;
            action = a;
            container = c;
            Config();
        }

        private static void Config()
        {
            HttpRuntime.Cache.Insert(ConfigFilePathDependencyKey, "",
                new CacheDependency(configFilePath),
                Cache.NoAbsoluteExpiration,
                Cache.NoSlidingExpiration,
                CacheItemPriority.Default, AppInfoCenterConfigFileUpdated);

            routes.Clear();
            routes.RouteExistingFiles = true;
            action(routes);

            //Routes.Add("w1", new Route("{page}.aspx", new AspxRouteHandler()));
            //Routes.Add("w2", new Route("{folder}/{page}.aspx", new AspxRouteHandler()));
            //Routes.Add("w3", new Route("{page}.ashx", new AspxRouteHandler()));
            //Routes.Add("w4", new Route("{folder}/{page}.ashx", new AspxRouteHandler()));
            //Routes.Add("w5", new Route("{page}.asmx", new AspxRouteHandler()));
            //Routes.Add("w6", new Route("{folder}/{page}.asmx", new AspxRouteHandler()));
            var config = XDocument.Load(configFilePath);
            using (routes.GetWriteLock())
            {
                var ignoreUrls = config.Descendants("routeTable").Descendants("ignore").Select(item => item.Attribute("url").Value).ToList();
                ignoreUrls.ForEach(url => routes.IgnoreRoute(url));

                var addItems = config.Descendants("routeTable").Descendants("add").Select(item =>
                    new
                    {
                        Name = item.Attribute("name").Value,
                        Url = item.Attribute("url").Value,
                        Engine = item.Attribute("engine") == null ? "mvc" : item.Attribute("engine").Value.ToLower(),
                        Defaults = item.Descendants("defaults").Elements().Select(sub => new
                        {
                            Name = sub.Name.ToString(),
                            DefaultValue = sub.Value
                        }).ToList(),
                        Constraints = item.Descendants("constraints").Elements().Select(sub => new
                        {
                            Name = sub.Name.ToString(),
                            Pattern = sub.Value
                        }).ToList()
                    }).ToList();

                foreach (var item in addItems)
                {
                    var defaults = new RouteValueDictionary();
                    foreach (var d in item.Defaults)
                    {
                        if (!defaults.ContainsKey(d.Name))
                        {
                            if (string.IsNullOrEmpty(d.DefaultValue))
                            {
                                defaults.Add(d.Name, UrlParameter.Optional);
                            }
                            else
                            {
                                defaults.Add(d.Name, d.DefaultValue);
                            }
                        }
                    }
                    var constraints = new RouteValueDictionary();
                    foreach (var d in item.Constraints)
                        if (!constraints.ContainsKey(d.Name))
                            constraints.Add(d.Name, d.Pattern);
                    if (routes[item.Name] == null)
                    {
                        IRouteHandler handler = new MvcRouteHandler();
                        if (item.Engine != "mvc")
                            handler = new AspxRouteHandler();
                        routes.Add(item.Name, new Route(item.Url, defaults, constraints, handler));
                    }
                }

                var removeItems = config.Descendants("routeTable").Descendants("remove").Select(item => item.Attribute("name").Value).ToList();
                removeItems.ForEach(name =>
                {
                    if (routes[name] != null)
                        routes.Remove(routes[name]);
                });

            }
        }

        private static void AppInfoCenterConfigFileUpdated(string key, object value, CacheItemRemovedReason reason)
        {
            Config();
        }
    }
}
