namespace MvcInfrastructure.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Web.Compilation;

    public class BuildManagerWrapper
    {
        private static readonly BuildManagerWrapper current = new BuildManagerWrapper();
        private IEnumerable<Assembly> referencedAssemblies;
        private IEnumerable<Type> publicTypes;
        private IEnumerable<Type> concreteTypes;

        public static BuildManagerWrapper Current
        {
            [DebuggerStepThrough]
            get
            {
                return current;
            }
        }

        public virtual IEnumerable<Assembly> Assemblies
        {
            get
            {
                return referencedAssemblies ?? (referencedAssemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(assembly => !assembly.GlobalAssemblyCache).ToList());
            }
        }

        public IEnumerable<Type> PublicTypes
        {
            get
            {
                return publicTypes ?? (publicTypes = Assemblies.PublicTypes().ToList());
            }
        }

        public IEnumerable<Type> ConcreteTypes
        {
            get
            {
                return concreteTypes ?? (concreteTypes = Assemblies.ConcreteTypes().ToList());
            }
        }
    }
}
