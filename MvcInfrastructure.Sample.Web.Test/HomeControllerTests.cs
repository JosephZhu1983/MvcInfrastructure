using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Moq;
using MvcInfrastructure.Sample.Business;
using MvcInfrastructure.Sample.Web.Controllers;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
namespace MvcInfrastructure.Sample.Web.Test
{
    public class HomeControllerTests
    {
        private readonly HomeController controller;
        private readonly Mock<ITestService> testService;

        public HomeControllerTests()
        {
            testService = new Mock<ITestService>();
            controller = new HomeController(testService.Object);
            var httpContext = new Mock<HttpContextBase>();
            var httpResponse = new Mock<HttpResponseBase>();
            httpResponse.SetupGet(m => m.Cookies).Returns(new HttpCookieCollection());
            httpContext.Setup(m => m.Response).Returns(httpResponse.Object);
            controller.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), controller);
            testService.Setup(m => m.Hello()).Returns("OK");
        }

        [Fact]
        public void test_Index()
        {
            controller.Index();
            Assert.Equal(controller.ViewData["TestController"], "OK");
        }
    }
}
