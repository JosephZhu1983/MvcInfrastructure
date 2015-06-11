using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MvcInfrastructure.Sample.Web
{
    public static class TestExtensions
    {
        public static MvcHtmlString Hello(this HtmlHelper html, string name)
        {
            return MvcHtmlString.Create("hello" + name);   
        }
    }
}
