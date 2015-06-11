using System;
using System.Web.Mvc;
using MvcInfrastructure.Core;
using MvcInfrastructure.Sample.Business;
using MvcInfrastructure.Sample.Web.Models;
using System.Linq;
using System.Collections.Generic;
using System.Web.Routing;

namespace MvcInfrastructure.Sample.Web.Controllers
{
    public class HomeController : DefaultController
    {
        private readonly ITestService testService;

        public HomeController(ITestService testService)
        {
            this.testService = testService;
            RegisterDynamicAction("Dynamic", () => View());
            RegisterDynamicAction("API", () => Json(new { Name = "朱晔", Age = 28 }, JsonRequestBehavior.AllowGet));
        }

        public ActionResult Index()
        {
            Response.Cookies.Add(new System.Web.HttpCookie("FirstName", "Ye"));

            ViewData["TestController"] = testService.Hello();
            return View();
        }

        public ActionResult Filter()
        {
            TempData["aa"] = "aa";
            throw new Exception("测试异常");
        }

        public ActionResult Route(string operation)
        {
            return Content(operation + "<a href=/browse/test.aspx>测试动态路由</a>");
        }

        public ActionResult Model()
        {
            return ViewWithAutoMapper<User, Product, TestViewModel>(testService.GetUser(), testService.GetProduct());
        }

        public ActionResult Cookie(MvcInfrastructure.Sample.Business.IUser user)
        {
            return Content(user.FirstName ?? string.Empty);
        }

        public ActionResult Velocity()
        {
            ViewData["title"] = "标题";
            ViewData["User"] = new
            {
                Name = "朱晔",
                Age = 28
            };
            return View("content", "template",  testService.GetUsers());
        }

        public ActionResult ViewDataTest()
        {
            ViewData["User"] = new
            {
                Name = "朱晔",
                Address = new { City = "上海", Street = "街道名" },
                ListData = new[] { new { A = "A1", B = "B1" }, new { A = "A2", B = "B2" } }
            };
            return View();
        }
    }
}
