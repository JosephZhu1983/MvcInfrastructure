namespace MvcInfrastructure.Core
{
    using System.Linq;
    using Microsoft.Practices.Unity;

    public class ActionFilterRegister : BootstrapperTask
    {
        public override TaskContinuation Execute(IUnityContainer container)
        {
            BuildManagerWrapper.Current.ConcreteTypes
                    .Where(type => KnownTypes.FilterAttributeType.IsAssignableFrom(type))
                    .Each(type =>
                        {
                            container.RegisterTypeAsTransient(type, type);
                        });

            return TaskContinuation.Continue;
        }
    }
}
