namespace MvcInfrastructure.Core
{
    using System.Linq;
    using Microsoft.Practices.Unity;
    using System.Web.Routing;

    public class RouteHandlerRegister : BootstrapperTask
    {
        public RouteHandlerRegister()
        {
            Order = 0;
        }

        public override TaskContinuation Execute(IUnityContainer container)
        {
            BuildManagerWrapper.Current.ConcreteTypes
                     .Where(type => KnownTypes.RouteHandlerType.IsAssignableFrom(type))
                     .Each(type =>
                         {
                             container.RegisterTypeAsTransient(KnownTypes.RouteHandlerType, type);
                         });

            return TaskContinuation.Continue;
        }
    }
}
