// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

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