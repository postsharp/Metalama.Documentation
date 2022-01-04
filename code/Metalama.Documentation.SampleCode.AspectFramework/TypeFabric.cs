using Metalama.Framework.Fabrics;
using System.Linq;

namespace Metalama.Documentation.SampleCode.AspectFramework.TypeFabric_
{
    internal class MyClass
    {
        private void Method1() { }

        public void Method2() { }

        class Fabric : TypeFabric
        {
            public override void AmendType(ITypeAmender amender)
            {
                // Adds logging aspect to all public methods.
                amender.WithMembers(t => t.Methods.Where(m => m.Accessibility == Framework.Code.Accessibility.Public))
                    .AddAspect<LogAttribute>();
            }
        }
    }
}
