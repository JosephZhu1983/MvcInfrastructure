using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcInfrastructure.Core;
using MvcInfrastructure.Sample.Web.Controllers;
using MvcInfrastructure.Library.ActionFilters;
using MvcInfrastructure.Sample.Business;
using MvcInfrastructure.Sample.Web.Models;

namespace MvcInfrastructure.Sample.Web
{
    public class ActionFilterRegister : ActionFilterBaseRegister
    {
        protected override void Register(FilterRegistry registry)
        {
            registry.RegisterOnAction<HomeController, TestActionFilterAttribute>(c => c.Index(), a => { a.A = "a"; a.B = "b"; })
                    //.RegisterOnAction<HomeController, NVelocityViewAttribute>(c => c.Velocity(), a => a.ViewPath = "/Views/Home/template.htm")
                    .RegisterOnAction<HomeController, AutoMapperAttribute>(c => c.Velocity(), a => { a.SrcType = typeof(User[]); a.DestType = typeof(TestViewModel[]); })
                    .RegisterOnGlobal<TestExceptionFilterAttribute>()
            ;
        }
    }
}
