namespace MvcInfrastructure.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Practices.Unity.Utility;

    public static class TypeExtensions
    {
        [DebuggerStepThrough]
        public static bool HasDefaultConstructor(this Type instance)
        {
            Guard.ArgumentNotNull(instance, "instance");

            return instance.GetConstructors(BindingFlags.Instance | BindingFlags.Public).Any(ctor => ctor.GetParameters().Length == 0);
        }

        [DebuggerStepThrough]
        public static IEnumerable<Type> PublicTypes(this Assembly instance)
        {
            IEnumerable<Type> types = null;

            if (instance != null)
            {
                try
                {
                    types = instance.GetTypes().Where(type => (type != null) && type.IsPublic && type.IsVisible).ToList();
                }
                catch (ReflectionTypeLoadException e)
                {
                    types = e.Types;
                }
            }

            return types ?? Enumerable.Empty<Type>();
        }

        [DebuggerStepThrough]
        public static IEnumerable<Type> PublicTypes(this IEnumerable<Assembly> instance)
        {
            return (instance == null) ?
                   Enumerable.Empty<Type>() :
                   instance.SelectMany(assembly => assembly.PublicTypes());
        }

        [DebuggerStepThrough]
        public static IEnumerable<Type> ConcreteTypes(this Assembly instance)
        {
            return (instance == null) ?
                   Enumerable.Empty<Type>() :
                   instance.PublicTypes()
                           .Where(type => (type != null) && type.IsClass && !type.IsAbstract && !type.IsInterface && !type.IsGenericType).ToList();
        }

        [DebuggerStepThrough]
        public static IEnumerable<Type> ConcreteTypes(this IEnumerable<Assembly> instance)
        {
            return (instance == null) ?
                   Enumerable.Empty<Type>() :
                   instance.SelectMany(assembly => assembly.ConcreteTypes());
        }
    }
}
