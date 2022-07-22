using Metalama.Framework.Fabrics;
using System;
using System.Linq;

namespace Doc.AspectConfiguration
{
#pragma warning disable CS0067, CS8618, CA1822, CS0162, CS0169, CS0414
    // The project fabric configures the project at compile time.
    public class Fabric : ProjectFabric
    {
        public override void AmendProject(IProjectAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");

    }
#pragma warning restore CS0067, CS8618, CA1822, CS0162, CS0169, CS0414

    // Some target code.
    public class SomeClass
    {
        public void SomeMethod()
        {
            Console.WriteLine($"MyCategory: Executing SomeClass.SomeMethod().");
            return;
        }
    }
}