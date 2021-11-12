using Caravela.Framework.Aspects;
using Caravela.Framework.Fabrics;
using System;
using System.Linq;

namespace Caravela.Documentation.SampleCode.AspectFramework.AspectConfiguration
{
#pragma warning disable CS0067
    // The project fabric configures the project at compile time.
    public class Fabric : ProjectFabric
    {
        public override void AmendProject(IProjectAmender amender) => throw new System.NotSupportedException("Compile-time only code cannot be called at run-time.");

    }
#pragma warning restore CS0067

    // Some target code.
    public class SomeClass
    {
        public void SomeMethod()
        {
            Console.WriteLine($"MyCategory: Executing Caravela.Documentation.SampleCode.AspectFramework.AspectConfiguration.SomeClass.SomeMethod().");
            return;
        }
    }

}
