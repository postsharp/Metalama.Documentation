using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System;
using System.Linq;

namespace Doc.TypeFabric_
{

#pragma warning disable CS0067, CS8618, CA1822, CS0162, CS0169, CS0414
    internal class MyClass
    {
        private void Method1() { }

        public void Method2()
        {
            Console.WriteLine($"Executing MyClass.Method2().");
            return;
        }


#pragma warning disable CS0067, CS8618, CA1822, CS0162, CS0169, CS0414
        private class Fabric : TypeFabric
        {
            public override void AmendType(ITypeAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");

        }

#pragma warning restore CS0067, CS8618, CA1822, CS0162, CS0169, CS0414

    }

#pragma warning restore CS0067, CS8618, CA1822, CS0162, CS0169, CS0414

}