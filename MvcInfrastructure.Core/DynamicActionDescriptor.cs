namespace MvcInfrastructure.Core
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class DynamicActionDescriptor : ActionDescriptor
    {
        private readonly string actionName;
        private readonly ControllerDescriptor controllerDescriptor;

        public DynamicActionDescriptor(string actionName, ControllerDescriptor controllerDescriptor)
        {
            this.actionName = actionName;
            this.controllerDescriptor = controllerDescriptor;
        }

        public override string ActionName
        {
            get { return actionName; }
        }

        public override ControllerDescriptor ControllerDescriptor
        {
            get { return controllerDescriptor; }
        }

        public override object Execute(ControllerContext controllerContext,
                                       IDictionary<string, object> parameters)
        {
            return new ViewResult { ViewName = ActionName };
        }

        public override ParameterDescriptor[] GetParameters()
        {
            return new ParameterDescriptor[] { };
        }
    }
}
