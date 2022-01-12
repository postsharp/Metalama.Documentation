using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;
using System.Linq;

namespace Metalama.Documentation.SampleCode.AspectFramework.TypeFabric_
{
#pragma warning disable CS0067
    internal class MyClass
    {
        private void Method1() { }

        public void Method2()
        {
            Console.WriteLine($"Executing MyClass.Method2().");
            return;
        }
#pragma warning disable CS0067

        private class Fabric : TypeFabric
        {
            public override void AmendType(ITypeAmender amender) => throw new System.NotSupportedException("Compile-time only code cannot be called at run-time.");

        }
#pragma warning restore CS0067
    }
#pragma warning restore CS0067
}