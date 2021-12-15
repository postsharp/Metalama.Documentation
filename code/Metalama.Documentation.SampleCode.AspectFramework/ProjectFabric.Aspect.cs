using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caravela.Framework.Fabrics;
using Caravela.Framework.Aspects;

namespace Caravela.Documentation.SampleCode.AspectFramework.ProjectFabric_
{
     internal class Fabric : ProjectFabric
    {
        public override void AmendProject(IProjectAmender project)
        {
            project.WithMembers(p => p.Types.SelectMany( t => t.Methods )).AddAspect<Log>();
        }
    }

    public class Log : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            Console.WriteLine($"Executing {meta.Target.Method}.");
            try
            {
                return meta.Proceed();
            }
            finally
            {
                Console.WriteLine($"Exiting {meta.Target.Method}.");
            }
        }
    }
}
