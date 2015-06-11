namespace MvcInfrastructure.Core
{
    using System;
    using System.Linq;
    using Microsoft.Practices.Unity;

    public class ControllerRegister : BootstrapperTask
    {
        public ControllerRegister()
        {
            Order = 3;
        }

        public override TaskContinuation Execute(IUnityContainer container)
        {
            Func<Type, bool> filter = type => KnownTypes.ControllerType.IsAssignableFrom(type) && type.Assembly != KnownTypes.ControllerType.Assembly;

            BuildManagerWrapper.Current.ConcreteTypes.Where(filter).Each(type => container.RegisterTypeAsTransient(KnownTypes.ControllerType, type));

            return TaskContinuation.Continue;
        }
    }
}
