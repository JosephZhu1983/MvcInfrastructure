using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcInfrastructure.Core;
using MvcInfrastructure.Sample.Business;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Utility;
using System.Diagnostics;

namespace MvcInfrastructure.Sample.Web
{
    public class TestPerRequestTask : PerRequestTask
    {
        private readonly ITestService testService;

        public TestPerRequestTask(ITestService testService)
        {
            this.testService = testService;
        }

        public override TaskContinuation BeginRequest(HttpContextBase context, IUnityContainer container)
        {
            context.Items["TestPerRequestTask"] = testService.Hello();
            context.Items["stopwatch"] = Stopwatch.StartNew();
            return TaskContinuation.Continue;
        }

        public override void EndRequest(HttpContextBase context, IUnityContainer container)
        {
            Stopwatch sw = context.Items["stopwatch"] as Stopwatch;
            if (sw != null && context.Request.Headers["x-requested-with"] == null)
                context.Response.Write("<!--时间：" + sw.ElapsedMilliseconds + "-->");
        }
    }
}
