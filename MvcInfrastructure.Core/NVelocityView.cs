namespace MvcInfrastructure.Core
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Web.Mvc;
    using NVelocity;
    using NVelocity.App;
    using System.Web;

    public class NVelocityView : IView, IViewDataContainer
    {
        private static VelocityEngine engine;
        private string viewPath;
        private string masterPath;
        private ControllerContext controllerContext;
        private ViewContext viewContext;

        static NVelocityView()
        {
            engine = new VelocityEngine();
            engine.SetProperty("resource.loader", "file");
            engine.SetProperty("file.resource.loader.path", HttpContext.Current.Server.MapPath("~"));
#if DEBUG
            engine.SetProperty("file.resource.loader.cache", false);
#else
            engine.SetProperty("file.resource.loader.cache", true);
            engine.SetProperty("file.resource.loader.modificationCheckInterval", 60L);
#endif

            engine.SetProperty("input.encoding", "utf-8");
            engine.Init();
        }

        public NVelocityView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            this.controllerContext = controllerContext;
            this.viewPath = viewPath.StartsWith("~") ? viewPath.Substring(1) : viewPath;
            this.masterPath = masterPath.StartsWith("~") ? masterPath.Substring(1) : masterPath;
        }

        public void Render(ViewContext viewContext, System.IO.TextWriter writer)
        {
            this.viewContext = viewContext;
            var context = CreateContext();

            var view = engine.GetTemplate(viewPath);
            if (!string.IsNullOrEmpty(masterPath))
            {
                var master = engine.GetTemplate(masterPath);
                context.Put("view", view.Name);
                master.Merge(context, writer);
            }
            else
            {
                view.Merge(context, writer);
            }
        }

        private VelocityContext CreateContext()
        {
            var entries = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            if (viewContext.ViewData != null)
            {
                foreach (var pair in viewContext.ViewData)
                {
                    entries[pair.Key] = pair.Value;
                }
            }
            entries["viewData"] = viewContext.ViewData;
            if (viewContext.TempData != null)
            {
                foreach (var pair in viewContext.TempData)
                {
                    entries[pair.Key] = pair.Value;
                }
            }
            entries["tempData"] = viewContext.TempData;
            entries["routeData"] = viewContext.RouteData;
            entries["controller"] = viewContext.Controller;
            entries["httpContext"] = viewContext.HttpContext;
            entries["viewModel"] = viewContext.ViewData.Model;

            CreateAndAddHelpers(entries);

            return new VelocityContext(entries);
        }

        private void CreateAndAddHelpers(Hashtable entries)
        {
            entries["html"] = new HtmlExtensionDuck(viewContext, this);
            entries["url"] = new UrlHelper(viewContext.RequestContext);
        }

        #region IViewDataContainer Members

        public ViewDataDictionary ViewData
        {
            get { return viewContext.ViewData; }
            set { throw new NotSupportedException(); }
        }

        #endregion
    }

}