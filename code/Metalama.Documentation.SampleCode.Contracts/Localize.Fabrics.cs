// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Fabrics;
using Metalama.Patterns.Contracts;
using System;

// ReSharper disable StringLiteralTypo

namespace Doc.Localize
{
    internal class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            amender.Outbound.SetOptions( new ContractOptions { Templates = new FrenchTemplates() } );
        }
    }
}