using Caravela.Framework.Fabrics;
using System;
using System.Linq;

namespace Caravela.Documentation.SampleCode.AspectFramework.TypeFabric_
{
#pragma warning disable CS0067
    internal class MyClass
    {
        private void Method1() { }

        public void Method2()
        {
            Console.WriteLine($"Executing Caravela.Documentation.SampleCode.AspectFramework.TypeFabric_.MyClass.Method2().");
            return;
        }
#pragma warning disable CS0067

        class Fabric : TypeFabric
        {
            public override void AmendType(ITypeAmender amender) => throw new System.NotSupportedException("Compile-time only code cannot be called at run-time.");

        }
#pragma warning restore CS0067
    }
#pragma warning restore CS0067
}
