// This is public domain Metalama sample code.

using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace Doc.GettingStarted_Fabric
{
    public class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            amender.Outbound
                .SelectMany( compilation => compilation.AllTypes )
                .Where( type => type.Accessibility is Accessibility.Public )
                .SelectMany( type => type.Methods )
                .Where( method => method.Accessibility is Accessibility.Public )
                .AddAspectIfEligible<LogAttribute>();

        }
    }
}