namespace MvcInfrastructure.Core
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AutoMapper;

    public class DefaultController : Controller
    {
        private Dictionary<string, Func<ActionResult>> dynamicActions = new Dictionary<string, Func<ActionResult>>();

        protected void RegisterDynamicAction(string actionName, Func<ActionResult> action)
        {
            if (!dynamicActions.ContainsKey(actionName))
                dynamicActions.Add(actionName, action);
            else
                dynamicActions[actionName] = action;
        }

        public Func<ActionResult> GetDynamicAction(string actionName)
        {
            if (dynamicActions.ContainsKey(actionName))
                return dynamicActions[actionName];
            return null;
        }

        protected static ActionResult ViewWithAutoMapper<TSrc1, TDest>(TSrc1 src1)
           where TDest : new()
        {
            ViewResult result = new ViewResult();
            TDest viewModel = new TDest();
            viewModel = Mapper.Map(src1, viewModel);
            result.ViewData.Model = viewModel;
            return result;
        }

        protected static ActionResult ViewWithAutoMapper<TSrc1, TSrc2, TDest>(TSrc1 src1, TSrc2 src2)
           where TDest : new()
        {
            ViewResult result = new ViewResult();
            TDest viewModel = new TDest();
            viewModel = Mapper.Map(src1, viewModel);
            viewModel = Mapper.Map(src2, viewModel);
            result.ViewData.Model = viewModel;
            return result;
        }
    }
}
