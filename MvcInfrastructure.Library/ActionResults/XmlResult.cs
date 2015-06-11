namespace MvcInfrastructure.Library.ActionResults
{
    using System.Web;
    using System.Web.Mvc;
    using System.Xml.Serialization;
    using Microsoft.Practices.Unity.Utility;

    public class XmlResult : ActionResult
    {
        public string ContentType
        {
            get;
            set;
        }

        public object Data
        {
            get;
            set;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Guard.ArgumentNotNull(context, "context");

            HttpResponseBase httpResponse = context.HttpContext.Response;

            httpResponse.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/xml";

            if (Data != null)
            {
                XmlSerializer serializer = new XmlSerializer(Data.GetType());
                serializer.Serialize(httpResponse.Output, Data);
            }
        }
    }
}
