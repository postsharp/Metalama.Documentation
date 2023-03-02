
using Metalama.Framework.Fabrics;
using System.Linq;

namespace Doc.AspectConfiguration
{
    // The project fabric configures the project at compile time.
    public class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            amender.Project.LoggingOptions().DefaultCategory = "MyCategory";

            // Adds the aspect to all members.
            amender.Outbound
                .SelectMany( c => c.Types.SelectMany( t => t.Methods ) )
                .AddAspectIfEligible<LogAttribute>();
        }
    }

}