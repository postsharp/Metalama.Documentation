// Warning LAMA0900 on `ExperimentalApi.Foo`: `The 'ExperimentalApi' type is experimental.`
using Doc.Architecture.Experimental_Fabric.ExperimentalNamespace;
using Metalama.Extensions.Architecture.Fabrics;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System.Linq;
namespace Doc.Architecture.Experimental_Fabric
{
  namespace ExperimentalNamespace
  {
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
    internal class Fabric : NamespaceFabric
    {
      public override void AmendNamespace(INamespaceAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");
    }
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
    public static class ExperimentalApi
    {
      public static void Foo()
      {
      }
    }
  }
  internal static class ProductionCode
  {
    public static void Dummy()
    {
      // This call is reported.
      ExperimentalApi.Foo();
    }
  }
}