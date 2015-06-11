namespace MvcInfrastructure.Core
{
    using Microsoft.Practices.Unity;

    public class ModelMetadataRegister : BootstrapperTask
    {
        public override TaskContinuation Execute(IUnityContainer container)
        {
            return TaskContinuation.Continue;
        }
    }
}
