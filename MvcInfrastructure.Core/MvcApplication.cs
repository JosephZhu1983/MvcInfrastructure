namespace MvcInfrastructure.Core
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity;
    using System.IO;

    public class MvcApplication : HttpApplication
    {
        private static IUnityContainer container = new UnityContainer();
        private static Bootstrapper bootstrapper;
        private static readonly object syncLock = new object();

        protected virtual bool IsPreLoading { get { return false; } }

        public Bootstrapper Bootstrapper
        {
            [DebuggerStepThrough]
            get
            {
                if (bootstrapper == null)
                {
                    lock (syncLock)
                    {
                        if (bootstrapper == null)
                        {
                            bootstrapper = new Bootstrapper(container);
                        }
                    }
                }

                return bootstrapper;
            }
        }

        private void MvcApplication_BeginRequest(object sender, EventArgs e)
        {
            if (Bootstrapper.IsLoading)
            {
                HttpContext.Current.Response.Write("MVC框架加载中");
                HttpContext.Current.Response.End();
            }
            if (IsPreLoading)
            {
                HttpContext.Current.Response.Write("AAF框架加载中");
                HttpContext.Current.Response.End();
            }
            OnBeginRequest(new HttpContextWrapper(Context));
        }

        private void MvcApplication_EndRequest(object sender, EventArgs e)
        {
            if (Bootstrapper.IsLoading) return;
            OnEndRequest(new HttpContextWrapper(Context));
        }

        protected virtual void OnStartApplication()
        {

        }

        protected virtual void OnEndApplication()
        {

        }

        protected virtual void OnBeginRequest(HttpContextBase context)
        {
            var tasks = container.ResolveAll<PerRequestTask>().ToList();
            foreach (var task in tasks)
            {
                if (task.BeginRequest(context, container) == TaskContinuation.Break)
                    break;
            }
        }

        protected virtual void OnEndRequest(HttpContextBase context)
        {
            var tasks = container.ResolveAll<PerRequestTask>().ToList();
            foreach (var task in tasks)
            {
                task.EndRequest(context, container);
            }
        }

        public void Application_Start()
        {
            OnStartApplication();
            Bootstrapper.Execute();
            AreaRegistration.RegisterAllAreas();
            
        }

        public void Application_End()
        {
            Bootstrapper.Dispose();
            OnEndApplication();

        }

        public override void Init()
        {
            base.Init();

            BeginRequest += new EventHandler(MvcApplication_BeginRequest);
            EndRequest += new EventHandler(MvcApplication_EndRequest);
        }
    }
}
