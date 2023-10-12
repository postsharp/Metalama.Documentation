// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Caching.Aspects.Configuration;

namespace Doc.ParameterFilter
{
    internal class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
            => amender.Outbound.ConfigureCaching( caching => caching.AddParameterClassifier( "ILogger", new LoggerParameterClassifier() ) );
    }
}