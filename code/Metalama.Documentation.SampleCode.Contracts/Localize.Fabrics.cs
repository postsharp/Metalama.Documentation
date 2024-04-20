// This is public domain Metalama sample code.

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
            amender.SetOptions( new ContractOptions { Templates = new FrenchTemplates() } );
        }
    }
}