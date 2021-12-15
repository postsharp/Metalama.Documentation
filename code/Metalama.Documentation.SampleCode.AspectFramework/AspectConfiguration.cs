using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using System;
using System.Linq;

namespace Metalama.Documentation.SampleCode.AspectFramework.AspectConfiguration
{
    // The project fabric configures the project at compile time.
    public class Fabric : ProjectFabric
    {
        public override void AmendProject(IProjectAmender amender)
        {
            amender.Project.LoggingOptions().DefaultCategory = "MyCategory";


            // Adds the aspect to all members.
            amender.WithMembers(c => c.Types.SelectMany(t => t.Methods)).AddAspect<LogAttribute>();
        }
    }

    // Some target code.
    public class SomeClass
    {
        public void SomeMethod() { }
    }

}
