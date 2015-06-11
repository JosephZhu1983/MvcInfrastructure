namespace MvcInfrastructure.Library.ActionResults
{
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity.Utility;

    public class PermanentRedirectToActionResult : ActionResult
    {
        public PermanentRedirectToActionResult(string actionName) : this(actionName, null, null) { }

        public PermanentRedirectToActionResult(string actionName, object routeValues) : this(actionName, null, routeValues) { }

        public PermanentRedirectToActionResult(string actionName, string controllerName, object routeValues)
        {
            this.ActionName = actionName;
            this.ControllerName = controllerName;
            this.RouteValues = routeValues;
        }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public object RouteValues { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            Guard.ArgumentNotNull(context, "context");

            HttpResponseBase response = context.HttpContext.Response;
            var urlHelper = new UrlHelper(context.RequestContext);
            var url = urlHelper.Action(this.ActionName, this.ControllerName, this.RouteValues);

            response.Clear();
            response.StatusCode = (int)HttpStatusCode.MovedPermanently;
            response.RedirectLocation = url;
        }
    }
}
