namespace MvcInfrastructure.Core
{
    using Microsoft.Practices.Unity;
    using System.Threading;

    public abstract class ServiceBaseRegister : BootstrapperTask
    {
        public ServiceBaseRegister()
        {
            Order = 2;
        }


        protected abstract void Register(IUnityContainer container);

        private void RegisterInternal(IUnityContainer container)
        {
            
        }   

        public override TaskContinuation Execute(IUnityContainer container)
        {
            RegisterInternal(container);
            Register(container);
            return TaskContinuation.Continue;
        }
    }
}
