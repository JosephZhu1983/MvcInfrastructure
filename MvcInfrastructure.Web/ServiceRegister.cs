using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcInfrastructure.Core;
using Microsoft.Practices.Unity;
using MvcInfrastructure.Sample.Business;
using AutoMapper;

namespace MvcInfrastructure.Sample.Web
{
    public class ServiceRegister : ServiceBaseRegister
    {
        protected override void Register(IUnityContainer container)
        {
            HtmlExtensionDuck.AddExtension(typeof(TestExtensions));
            Mapper.Initialize(x => x.AddProfile<DefaultAutoMapperProfile>());
        }
    }
}
