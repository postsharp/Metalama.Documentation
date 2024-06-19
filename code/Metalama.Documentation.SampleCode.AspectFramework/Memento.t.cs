// --- @@Intrinsics.cs ---
using System;
namespace Metalama.Compiler
{
  internal static class Intrinsics
  {
    public static RuntimeMethodHandle GetRuntimeMethodHandle(string documentationId) => throw new InvalidOperationException("Code calling this method has to be compiled by the Metalama compiler.");
    public static RuntimeFieldHandle GetRuntimeFieldHandle(string documentationId) => throw new InvalidOperationException("Code calling this method has to be compiled by the Metalama compiler.");
    public static RuntimeTypeHandle GetRuntimeTypeHandle(string documentationId) => throw new InvalidOperationException("Code calling this method has to be compiled by the Metalama compiler.");
  }
} // --- Memento.Aspect.cs ---
using  Metalama . Framework . Advising ;  using  Metalama . Framework . Aspects ;  using  Metalama . Framework . Code ;  using  System . Collections . Generic ;  using  System . Linq ;
namespace Doc.Memento;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
public class MementoAttribute : TypeAspect
{
  [CompileTime]
  private sealed record Tags(INamedType SnapshopType, IReadOnlyList<(IFieldOrProperty Source, IField Snapshot)> Fields);
  public override void BuildAspect(IAspectBuilder<INamedType> builder) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  [Introduce]
  public object Save() => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  [Introduce]
  public void Restore(object snapshot) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
  [Template]
  public void MementoConstructorTemplate() => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}
// --- Memento.cs ---
namespace Doc.Memento;
[Memento]
public class Vehicle
{
  public string Name { get; }
  public decimal Payload { get; set; }
  public string Fuel { get; set; }
  public void Restore(object snapshot)
  {
    Payload = ((Snapshot)snapshot).Payload;
    Fuel = ((Snapshot)snapshot).Fuel;
  }
  public object Save()
  {
    return new Snapshot(Payload, Fuel);
  }
  private class Snapshot : object
  {
    public readonly string Fuel;
    public readonly decimal Payload;
    public Snapshot(decimal Payload, string Fuel)
    {
      this.Payload = Payload;
      this.Fuel = Fuel;
    }
  }
}