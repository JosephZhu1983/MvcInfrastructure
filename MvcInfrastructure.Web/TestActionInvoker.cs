using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcInfrastructure.Core;
using Microsoft.Practices.Unity;
using MvcInfrastructure.Sample.Business;

namespace MvcInfrastructure.Sample.Web
{
    public class TestActionInvoker : ExtendedSyncControllerActionInvoker
    {
        private readonly ITestService testService;

        public TestActionInvoker(IUnityContainer container, ITestService testService)
            : base(container)
        {
            this.testService = testService;
        }

        protected override System.Web.Mvc.ActionDescriptor FindAction(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ControllerDescriptor controllerDescriptor, string actionName)
        {
            controllerContext.Controller.ViewData["TestActionInvoker"] = testService.Hello();
            return base.FindAction(controllerContext, controllerDescriptor, actionName);
        }
    }
}
