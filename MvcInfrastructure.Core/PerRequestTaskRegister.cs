namespace MvcInfrastructure.Core
{
    using System.Linq;
    using Microsoft.Practices.Unity;

    public class PerRequestTaskRegister : BootstrapperTask
    {
        public override TaskContinuation Execute(IUnityContainer container)
        {
            BuildManagerWrapper.Current.ConcreteTypes
                    .Where(type => KnownTypes.PerRequestTaskType.IsAssignableFrom(type))
                    .Each(type => container.RegisterTypeAsTransient(KnownTypes.PerRequestTaskType, type));

            return TaskContinuation.Continue;
        }
    }
}
