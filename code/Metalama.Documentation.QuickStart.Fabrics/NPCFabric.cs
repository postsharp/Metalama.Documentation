// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using Metalama.Documentation.QuickStart;

namespace Metalama.Documentation.QuickStart.Fabrics
{
    internal class NotifyPropertyChangedForAllPublicTypes : NamespaceFabric
    {
        public override void AmendNamespace( INamespaceAmender project )
        {
            //Add NotifyPropertyChanged to all public types 
            project.Outbound.SelectMany( t => t.Types.Where( m => m.Accessibility == Accessibility.Public ) )
                .AddAspectIfEligible<NotifyPropertyChangedAttribute>();
        }
    }
}