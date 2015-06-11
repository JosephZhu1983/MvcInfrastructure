namespace MvcInfrastructure.Core
{
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Utility;
    using System;
    using System.Linq;

    public class ValueProviderFactoryRegister : BootstrapperTask
    {
        protected ValueProviderFactoryCollection ValueProviderFactories
        { 
            get;
            private set;
        }
        public ValueProviderFactoryRegister(ValueProviderFactoryCollection valueProviderFactories)
        {
            Guard.ArgumentNotNull(valueProviderFactories, "valueProviderFactories");
            ValueProviderFactories = valueProviderFactories;
        }

        public override TaskContinuation Execute(IUnityContainer container)
        {
            Func<Type, bool> filter = type => KnownTypes.ValueProviderFactoryType.IsAssignableFrom(type) &&
                                                 !ValueProviderFactories.Any(factory => factory.GetType() == type);

            BuildManagerWrapper.Current.ConcreteTypes.Where(filter)
                     .Each(type => container.RegisterTypeAsSingleton(KnownTypes.ValueProviderFactoryType, type));

            container.ResolveAll<ValueProviderFactory>()
                     .Each(factory => ValueProviderFactories.Add(factory));

            return TaskContinuation.Continue;
        }
    }
}
