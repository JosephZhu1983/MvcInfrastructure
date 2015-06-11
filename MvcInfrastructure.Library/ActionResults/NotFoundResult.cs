namespace MvcInfrastructure.Library.ActionResults
{
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity.Utility;

    public class NotFoundResult : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            Guard.ArgumentNotNull(context, "context");

            HttpResponseBase response = context.HttpContext.Response;

            response.Clear();
            response.StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}
