namespace MvcInfrastructure.Core
{
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;
    using System.Configuration;

    public class Bootstrapper : Disposable
    {
        private IUnityContainer container;

        public bool IsLoading { get; private set; }

        public Bootstrapper(IUnityContainer container)
        {
            IsLoading = true;
            this.container = container;
            this.container.LoadConfiguration();

            container.RegisterInstance<ControllerBuilder>(ControllerBuilder.Current)
                     .RegisterInstance<ValueProviderFactoryCollection>(ValueProviderFactories.Factories)
                     .RegisterInstance<RouteCollection>(RouteTable.Routes)
                     .RegisterInstance<ModelBinderDictionary>(ModelBinders.Binders)
                     .RegisterInstance<IUnityContainer>(container)
                     .RegisterTypeAsSingleton<FilterRegistry, FilterRegistry>()
                     .RegisterTypeAsSingleton<ActionInvokerRegistry, ActionInvokerRegistry>();

            BuildManagerWrapper.Current.ConcreteTypes
                       .Where(type => KnownTypes.BootstrapperTaskType.IsAssignableFrom(type))
                       .Each(type => container.RegisterTypeAsSingleton(KnownTypes.BootstrapperTaskType, type));
            ViewEngines.Engines.Add(new NVelocityViewEngine());
        }


        public void Execute()
        {
            new Thread(() =>
            {
                var tasks = container.ResolveAll<BootstrapperTask>();

                foreach (var task in tasks.OrderBy(t => t.Order))
                {
                    if (task.Execute(container) == TaskContinuation.Break)
                        break;
                }

                IsLoading = false;
            }).Start();
        }

        protected override void DisposeCore()
        {
            container.ResolveAll<BootstrapperTask>().OrderByDescending(t => t.Order).Each(task => task.Dispose());
        }
    }
}
