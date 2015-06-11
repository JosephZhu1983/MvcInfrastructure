namespace MvcInfrastructure.Core
{
    using System.Web;
    using Microsoft.Practices.Unity;

    public abstract class PerRequestTask : Disposable
    {
        public abstract TaskContinuation BeginRequest(HttpContextBase context, IUnityContainer container);

        public virtual void EndRequest(HttpContextBase context, IUnityContainer container)
        {
            Dispose();
        }
    }
}
