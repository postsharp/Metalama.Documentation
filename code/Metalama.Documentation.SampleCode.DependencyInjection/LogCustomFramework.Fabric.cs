// This is public domain Metalama sample code.

using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Fabrics;

namespace Doc.LogCustomFramework;
#pragma warning disable CS0649, CS8618
public class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.ConfigureDependencyInjection(
            dependencyInjection
                => dependencyInjection.RegisterFramework<LoggerDependencyInjectionFramework>() );
    }
}