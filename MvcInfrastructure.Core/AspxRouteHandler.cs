namespace MvcInfrastructure.Core
{
    using System.Web;
    using System.Web.Compilation;
    using System.Web.Routing;
    using System.Web.UI;
    using System.Web.Mvc;

    public class AspxRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            //var page = requestContext.RouteData.Values.ContainsKey("page") ? requestContext.RouteData.GetRequiredString("page") : "";
            //var folder = requestContext.RouteData.Values.ContainsKey("folder") ? requestContext.RouteData.GetRequiredString("folder") : "";
            return BuildManager.CreateInstanceFromVirtualPath(requestContext.HttpContext.Request.Url.AbsolutePath, typeof(Page)) as IHttpHandler;
        }
    }
}
