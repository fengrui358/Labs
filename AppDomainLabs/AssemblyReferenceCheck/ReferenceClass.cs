using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyReferenceCheck
{
    public class ReferenceClass : MarshalByRefObject
    {
        public AssemblyName[] ReflectionAssemblyPath(string assemblyPath)
        {
            var assembly = Assembly.ReflectionOnlyLoadFrom(assemblyPath);
            return assembly.GetReferencedAssemblies();
        }
    }
}
