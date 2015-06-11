namespace MvcInfrastructure.Core
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public class HtmlExtensionDuck : ExtensionDuck
    {
        private static readonly List<Type> _extensionTypes = new List<Type>
		{                 
			typeof(System.Web.Mvc.Html.ChildActionExtensions),    
			typeof(System.Web.Mvc.Html.DisplayExtensions),
			typeof(System.Web.Mvc.Html.DisplayTextExtensions),
			typeof(System.Web.Mvc.Html.EditorExtensions),
			typeof(System.Web.Mvc.Html.FormExtensions),
			typeof(System.Web.Mvc.Html.InputExtensions),
			typeof(System.Web.Mvc.Html.LabelExtensions),
			typeof(System.Web.Mvc.Html.LinkExtensions),
            typeof(System.Web.Mvc.Html.PartialExtensions),
            typeof(System.Web.Mvc.Html.RenderPartialExtensions),
            typeof(System.Web.Mvc.Html.SelectExtensions),
            typeof(System.Web.Mvc.Html.TextAreaExtensions),
            typeof(System.Web.Mvc.Html.ValidationExtensions)
		};

        public HtmlExtensionDuck(ViewContext viewContext, IViewDataContainer container)
            : this(new HtmlHelper(viewContext, container))
        {
        }

        public HtmlExtensionDuck(HtmlHelper htmlHelper)
            : this(htmlHelper, _extensionTypes.ToArray())
        {
        }

        public HtmlExtensionDuck(HtmlHelper htmlHelper, params Type[] extentionTypes)
            : base(htmlHelper, extentionTypes)
        {
        }

        public static void AddExtension(Type type)
        {
            if (!_extensionTypes.Contains(type))
                _extensionTypes.Add(type);
        }
    }
}