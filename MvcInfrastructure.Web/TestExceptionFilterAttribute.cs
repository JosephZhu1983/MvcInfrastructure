using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.IO;

namespace MvcInfrastructure.Sample.Web
{
    public class TestExceptionFilterAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            string controller = filterContext.RouteData.Values["controller"] as string;
            string action = filterContext.RouteData.Values["action"] as string;

            filterContext.RequestContext.HttpContext.Response.Write(string.Format("Controller：{0}<br/>Action：{1}<br/>Message：{2}<br/>StackTrace：{3}",
                controller, action, filterContext.Exception.Message, filterContext.Exception.StackTrace.Replace("\r\n", "<br/>")));
            filterContext.ExceptionHandled = true;

        }
    }
}
