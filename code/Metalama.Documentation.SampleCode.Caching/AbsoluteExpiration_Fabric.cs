// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Caching.Aspects.Configuration;
using System;

namespace Doc.AbsoluteExpiration_Fabric
{
    public class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            amender.Outbound.Select( x => x.GlobalNamespace.GetDescendant( "MyProduct.MyNamespace" )! )
                .ConfigureCaching( caching => caching.AbsoluteExpiration = TimeSpan.FromMinutes( 20 ) );
        }
    }
}