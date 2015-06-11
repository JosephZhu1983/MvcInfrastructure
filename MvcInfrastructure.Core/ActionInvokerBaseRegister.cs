namespace MvcInfrastructure.Core
{
    using System.Web.Routing;
    using Microsoft.Practices.Unity;

    public abstract class ActionInvokerBaseRegister : BootstrapperTask
    {
        protected abstract void Register(ActionInvokerRegistry registry);

        public override TaskContinuation Execute(IUnityContainer container)
        {
            Register(container.Resolve<ActionInvokerRegistry>());

            return TaskContinuation.Continue;
        }
    }
}
