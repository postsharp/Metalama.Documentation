// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Contracts;

namespace Doc.Invariants_Disable
{
    public class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            amender.Outbound.Select( c => c.GlobalNamespace.GetDescendant( "Doc.Invariants_Disable" )! )
                .SetOptions( new ContractOptions { AreInvariantsEnabled = false } );
        }
    }
}