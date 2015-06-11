namespace MvcInfrastructure.Core
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using Microsoft.Practices.Unity.Utility;

    public class ActionFilterRegistryItem<TController> : FilterRegistryItem where TController : Controller
    {
        private readonly ReflectedActionDescriptor reflectedActionDescriptor;

        public ActionFilterRegistryItem(Expression<Action<TController>> action, Func<FilterAttribute> configFilter)
            : base(configFilter)
        {
            Guard.ArgumentNotNull(action, "action");

            MethodCallExpression methodCall = action.Body as MethodCallExpression;

            if ((methodCall == null) || !KnownTypes.ActionResultType.IsAssignableFrom(methodCall.Method.ReturnType))
            {
                throw new ArgumentException("表达式必须返回有效的action", "action");
            }

            reflectedActionDescriptor = new ReflectedActionDescriptor(methodCall.Method, methodCall.Method.Name, new ReflectedControllerDescriptor(methodCall.Object.Type));
        }

        public override bool IsMatching(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            if ((controllerContext != null) && (actionDescriptor != null))
            {
                ReflectedActionDescriptor matchingDescriptor = actionDescriptor as ReflectedActionDescriptor;

                return (matchingDescriptor != null) ?
                       reflectedActionDescriptor.MethodInfo == matchingDescriptor.MethodInfo :
                       IsSameAction(reflectedActionDescriptor, actionDescriptor);
            }

            return false;
        }

        private static bool IsSameAction(ActionDescriptor descriptor1, ActionDescriptor descriptor2)
        {
            ParameterDescriptor[] parameters1 = descriptor1.GetParameters();
            ParameterDescriptor[] parameters2 = descriptor2.GetParameters();

            bool same = descriptor1.ControllerDescriptor.ControllerName.Equals(descriptor2.ControllerDescriptor.ControllerName, StringComparison.OrdinalIgnoreCase) &&
                        descriptor1.ActionName.Equals(descriptor2.ActionName, StringComparison.OrdinalIgnoreCase) &&
                        (parameters1.Length == parameters2.Length);

            if (same)
            {
                for (int i = parameters1.Length - 1; i >= 0; i--)
                {
                    if (parameters1[i].ParameterType != parameters2[i].ParameterType)
                    {
                        same = false;
                        break;
                    }
                }
            }

            return same;
        }
    }
}
