using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MvcInfrastructure.Sample.Business;

namespace MvcInfrastructure.Sample.Web
{
    public class TestActionFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        public string A
        {
            get;
            set;
        }

        public string B
        {
            get;
            set;
        }

        private readonly ITestService testService;

        public TestActionFilterAttribute(ITestService testService)
        {
            this.testService = testService;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewData["TestActionFilter"] = (A ?? string.Empty) + " " + (B ?? string.Empty) + " " + testService.Hello();
            base.OnActionExecuted(filterContext);
        }
    }
}
