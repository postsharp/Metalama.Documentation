using System;
using Metalama.Framework.Fabrics;

public class SharedFabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.Outbound
            .SelectMany( p => p.AllTypes )
            .SelectMany( t => t.Methods )
            .AddAspectIfEligible<LogAttribute>();
    }
}
