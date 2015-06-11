namespace MvcInfrastructure.Core
{
    using System.Linq;
    using Microsoft.Practices.Unity;

    public class ActionInvokerRegister : BootstrapperTask
    {
        public override TaskContinuation Execute(IUnityContainer container)
        {
            BuildManagerWrapper.Current.ConcreteTypes
                     .Where(type => KnownTypes.ActionInvokerType.IsAssignableFrom(type))
                     .Each(type =>
                         {
                             container.RegisterTypeAsTransient(type, type);
                         });

            return TaskContinuation.Continue;
        }
    }
}
