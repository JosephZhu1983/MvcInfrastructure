namespace MvcInfrastructure.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.Practices.Unity.Utility;

    public static class EnumerableExtensions
    {
        [DebuggerStepThrough]
        public static void Each<T>(this IEnumerable<T> instance, Action<T> action)
        {
            Guard.ArgumentNotNull(action, "action");

            if (instance != null)
            {
                foreach (T item in instance)
                {
                    action(item);
                }
            }
        }
    }
}
