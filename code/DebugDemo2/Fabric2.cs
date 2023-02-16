using Metalama.Documentation.QuickStart;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System.ComponentModel;

namespace DebugDemo2
{
    internal class AddLoggingToAllMethodsInGivenNamespace : ProjectFabric
    {
        public override void AmendProject(IProjectAmender project)
        {

            //Adding Log attribute to all mehtods of all types 
            //that are available inside "Outer.Inner" namespace 
            
            /*
            project.Outbound.SelectMany(p => p.GlobalNamespace.GetDescendant("Outer.Inner")
                                            .DescendantsAndSelf()
                                            .SelectMany(p => p.Types.SelectMany(m => m.Methods)))
                            .AddAspectIfEligible<LogAttribute>();
            */

            // TODO: Fix that code.
            
        }
    }

}
