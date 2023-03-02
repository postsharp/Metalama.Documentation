using Metalama.Framework.Fabrics;
using System.Linq;

namespace Doc.ProjectFabric_TwoAspects
{
    internal class Fabric : ProjectFabric
    {
        // This method is the compile-time entry point of your project.
        // It executes within the compiler or IDE.
        public override void AmendProject( IProjectAmender project )
        {
            AddLogging( project );
            AddProfiling( project );
        }

        private static void AddLogging( IProjectAmender project  )
        {
            project.Outbound
                .SelectMany( p => p.Types )
                .SelectMany( t => t.Methods )
                .AddAspectIfEligible<Log>();
        }

        private static void AddProfiling( IProjectAmender project )
        {
            project.Outbound
                .SelectMany( p => p.Types.Where( t => t.Accessibility == Metalama.Framework.Code.Accessibility.Public ) )
                .SelectMany( t => t.Methods.Where( m => m.Accessibility == Metalama.Framework.Code.Accessibility.Public ) )
                .AddAspectIfEligible<Profile>();

        }
    }
}