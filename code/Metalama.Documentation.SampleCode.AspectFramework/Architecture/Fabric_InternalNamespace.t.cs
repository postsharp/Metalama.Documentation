// Warning LAMA0905 on `Foo`: `The 'Doc.Architecture.Fabric_InternalNamespace.VerifiedNamespace' namespace cannot be used by the 'ForbiddenInheritor' type.`
using Doc.Architecture.Fabric_InternalNamespace.VerifiedNamespace;
using Metalama.Extensions.Architecture.Fabrics;
using Metalama.Extensions.Architecture.Predicates;
using Metalama.Framework.Fabrics;
namespace Doc.Architecture.Fabric_InternalNamespace
{
    namespace VerifiedNamespace
    {
#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
        internal class Fabric : NamespaceFabric
        {
            public override void AmendNamespace( INamespaceAmender amender ) => throw new System.NotSupportedException( "Compile-time-only code cannot be called at run-time." );
        }
#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052
        internal class Foo
        {
        }
        internal class AllowedInheritor : Foo
        {
        }
    }
    namespace OtherNamespace
    {
        internal class ForbiddenInheritor : Foo
        {
        }
    }
}