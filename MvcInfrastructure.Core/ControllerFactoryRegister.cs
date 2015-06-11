namespace MvcInfrastructure.Core
{
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Utility;

    public class ControllerFactoryRegister : BootstrapperTask
    {
        protected ControllerBuilder ControllerBuilder
        {
            get;
            private set;
        }

        public ControllerFactoryRegister(ControllerBuilder controllerBuilder)
        {
            Guard.ArgumentNotNull(controllerBuilder, "controllerBuilder");

            ControllerBuilder = controllerBuilder;
        }

        public override TaskContinuation Execute(IUnityContainer container)
        {
            container.RegisterTypeAsSingleton(typeof(IControllerFactory), KnownTypes.ControllerFactoryType);

            ControllerBuilder.SetControllerFactory(container.ResolveOne<IControllerFactory>());

            return TaskContinuation.Continue;
        }
    }
}
