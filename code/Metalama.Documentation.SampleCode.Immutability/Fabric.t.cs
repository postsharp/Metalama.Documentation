using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using Metalama.Patterns.Immutability;
using Metalama.Patterns.Immutability.Configuration;
using System;
namespace Metalama.Documentation.SampleCode.Immutability.Fabric;
[Immutable(ImmutabilityKind.Deep)]
public class Person
{
  public required string FirstName { get; init; }
  public required string LastName { get; init; }
  public Uri? HomePage { get; init; }
}
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
internal class Fabric : ProjectFabric
{
  public override void AmendProject(IProjectAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052