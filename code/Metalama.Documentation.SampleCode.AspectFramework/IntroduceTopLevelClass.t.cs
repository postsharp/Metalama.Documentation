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
} // --- IntroduceTopLevelClass.Aspect.cs ---
using  Metalama . Framework . Advising ;  using  Metalama . Framework . Aspects ;  using  Metalama . Framework . Code ;
namespace Doc.IntroduceTopLevelClass;
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
public class BuilderAttribute : TypeAspect
{
  public override void BuildAspect(IAspectBuilder<INamedType> builder) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
}
// --- IntroduceTopLevelClass.cs ---
namespace Doc.IntroduceTopLevelClass;
[Builder]
internal class Material
{
  public string Name { get; }
  public double Density { get; }
}
// --- Doc.IntroduceTopLevelClass.Builders.MaterialBuilder.cs ---
namespace Doc.IntroduceTopLevelClass.Builders
{
  class MaterialBuilder : object
  {
  }
}