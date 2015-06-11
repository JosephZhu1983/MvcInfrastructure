namespace MvcInfrastructure.Core
{
    using System;
    using System.Collections;
    using System.Configuration;
    using System.IO;
    using System.Web.Mvc;
    using Commons.Collections;
    using NVelocity;
    using NVelocity.App;
    using NVelocity.Runtime;

    public class NVelocityViewEngine : VirtualPathProviderViewEngine
    {
        public NVelocityViewEngine()
        {
            base.ViewLocationFormats = new string[] { "~/Views/{1}/{0}.htm", "~/Views/Shared/{0}.htm", "~/Views/{1}/{0}.html", "~/Views/Shared/{0}.html" };
            base.AreaViewLocationFormats = new string[] { "~/Areas/{2}/Views/{1}/{0}.htm", "~/Areas/{2}/Views/Shared/{0}.htm", "~/Areas/{2}/Views/{1}/{0}.html", "~/Areas/{2}/Views/Shared/{0}.html" };
            base.MasterLocationFormats = base.PartialViewLocationFormats = base.ViewLocationFormats;
            base.AreaMasterLocationFormats = base.AreaPartialViewLocationFormats = base.AreaViewLocationFormats;
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return CreateView(controllerContext, partialPath, "");
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new NVelocityView(controllerContext, viewPath, masterPath);
        }
    }

}