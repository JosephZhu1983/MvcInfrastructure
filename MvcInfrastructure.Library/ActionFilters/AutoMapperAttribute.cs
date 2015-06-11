namespace MvcInfrastructure.Library.ActionFilters
{
    using System;
    using System.Web.Mvc;
    using AutoMapper;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AutoMapperAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model;

            object viewModel = Mapper.Map(model, SrcType, DestType);

            filterContext.Controller.ViewData.Model = viewModel;
        }

        public Type SrcType
        {
            get;
            set;
        }

        public Type DestType
        {
            get;
            set;
        }

    }
}
