using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using Metalama.Patterns.Immutability;
using Metalama.Patterns.Immutability.Configuration;
using System;

namespace Metalama.Documentation.SampleCode.Immutability.Fabric;

[Immutable( ImmutabilityKind.Deep )]
public class Person
{
    public required string FirstName { get; init; }
    
    public required string LastName { get; init; }
    
    public Uri? HomePage { get; init; }
}

internal class Fabric : ProjectFabric
{
    public override void AmendProject( IProjectAmender amender )
    {
        amender.SelectReflectionType( typeof(Uri) ).ConfigureImmutability( ImmutabilityKind.Deep );
    }
}