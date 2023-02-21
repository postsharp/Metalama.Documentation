using Metalama.Documentation.QuickStart;
using Metalama.Framework.Fabrics;
using Metalama.Framework.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugDemo3
{
    public class Fabric : ProjectFabric
    {
        public override void AmendProject(IProjectAmender project)
        {

            //Adding Log attribute to all mehtods of all types 
            //that are available inside "Outer.Inner" namespace 


            project.Outbound.Select(t => t.GlobalNamespace)
                                    .Where(z => z.Name == "Outer.Inner")
                                    .SelectMany(ns => ns.DescendantsAndSelf())
                                    .SelectMany(ns => ns.Types)
                                    .SelectMany(currentType => currentType.Methods)
                                    .AddAspectIfEligible<LogAttribute>();
            
        }
    }
}
