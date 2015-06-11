namespace MvcInfrastructure.Core
{
    using Microsoft.Practices.Unity;

    public abstract class ActionFilterBaseRegister : BootstrapperTask
    {
        protected abstract void Register(FilterRegistry registry);

        private void RegisterInternal(FilterRegistry registry)
        {
        }

        public override TaskContinuation Execute(IUnityContainer container)
        {
            var filterRegistry = container.Resolve<FilterRegistry>();
            RegisterInternal(filterRegistry);
            Register(filterRegistry);
            return TaskContinuation.Continue;
        }
    }
}
