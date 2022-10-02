using Metalama.Framework.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Doc.EnumerateMethodInfos
{
    internal class EnumerateMethodAspect : TypeAspect
    {
        [Introduce]
        public IReadOnlyList<MethodInfo> GetMethods()
        {
            var methods = new List<MethodInfo>();

            foreach ( var method in meta.Target.Type.Methods )
            {
                methods.Add( method.ToMethodInfo() );
            }

            return methods;

        }
    }
}
