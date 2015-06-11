namespace MvcInfrastructure.Core
{
    using System.Linq;
    using Microsoft.Practices.Unity;
    using System.Web.Mvc;

    public class ModelBinderRegister : BootstrapperTask
    {
        public override TaskContinuation Execute(IUnityContainer container)
        {
            BuildManagerWrapper.Current.ConcreteTypes
                    .Where(type => KnownTypes.ModelBinderType.IsAssignableFrom(type))
                    .Each(type =>
                    {
                        container.RegisterTypeAsSingleton(KnownTypes.ModelBinderType, type);
                    });

            container.Resolve<ModelBinderDictionary>().DefaultBinder = container.ResolveOne<IModelBinder>();

            return TaskContinuation.Continue;
        }
    }
}
