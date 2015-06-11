namespace MvcInfrastructure.Library.ValueProviderFactory
{
    using System.Globalization;
    using System.Web;
    using System.Web.Mvc;

    public class CookieValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            return new CookieValueProvider(controllerContext.HttpContext.Request.Cookies);
        }

        private class CookieValueProvider : IValueProvider
        {
            private readonly HttpCookieCollection _cookieCollection;

            public CookieValueProvider(HttpCookieCollection cookieCollection)
            {
                _cookieCollection = cookieCollection;
            }

            public bool ContainsPrefix(string prefix)
            {
                return _cookieCollection[prefix] != null;
            }

            public ValueProviderResult GetValue(string key)
            {
                HttpCookie cookie = _cookieCollection[key];
                return cookie != null ? new ValueProviderResult(cookie.Value, cookie.Value, CultureInfo.CurrentUICulture): null;
            }
        }
    }

}
