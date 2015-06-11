namespace MvcInfrastructure.Core
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System;

    public class NullTempDataProvider : ITempDataProvider
    {
        public IDictionary<string, object> LoadTempData(ControllerContext controllerContext)
        {
            return null;
        }

        public void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
        {
            
        }
    }
}
