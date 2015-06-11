namespace MvcInfrastructure.Core
{
    using Microsoft.Practices.Unity;

    public abstract class BootstrapperTask : Disposable
    {
        public BootstrapperTask()
        {
            Order = 99;
        }
        public int Order
        {
            get;
            protected set;
        }
 
        public abstract TaskContinuation Execute(IUnityContainer container);
    }
}
