using Doc.Architecture.Fabric_InternalNamespace.VerifiedNamespace;
using Metalama.Extensions.Architecture.Fabrics;
using Metalama.Extensions.Architecture.Predicates;
using Metalama.Framework.Fabrics;

namespace Doc.Architecture.Fabric_InternalNamespace
{
    namespace VerifiedNamespace
    {
        internal class Fabric : NamespaceFabric
        {
            public override void AmendNamespace( INamespaceAmender amender )
            {
                amender.Verify().CanOnlyBeUsedFrom( r => r.CurrentNamespace() );
            }
        }

        internal class Foo { }

        internal class AllowedInheritor : Foo { }
    }

    namespace OtherNamespace
    {
        internal class ForbiddenInheritor : Foo { }
    }

}
