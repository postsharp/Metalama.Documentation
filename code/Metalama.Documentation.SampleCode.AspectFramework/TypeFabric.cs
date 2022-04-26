using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System.Linq;

namespace Doc.TypeFabric_
{
    internal class MyClass
    {
        private void Method1() { }

        public void Method2() { }

        private class Fabric : TypeFabric
        {
            public override void AmendType( ITypeAmender amender )
            {
                // Adds logging aspect to all public methods.
                amender.With( t => t.Methods.Where( m => m.Accessibility == Accessibility.Public ) )
                    .AddAspect<LogAttribute>();
            }
        }
    }
}