using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcInfrastructure.Core;
using MvcInfrastructure.Sample.Web.Controllers;

namespace MvcInfrastructure.Sample.Web
{
    public class ActionInvokerRegister : ActionInvokerBaseRegister
    {
        protected override void Register(ActionInvokerRegistry registry)
        {
            registry.RegisterSyncActionInvoker<HomeController, TestActionInvoker>();
        }
    }
}
