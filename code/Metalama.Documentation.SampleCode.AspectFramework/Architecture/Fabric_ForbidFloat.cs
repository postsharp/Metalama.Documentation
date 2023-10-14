// This is public domain Metalama sample code.

using Metalama.Extensions.Architecture.Fabrics;
using Metalama.Extensions.Architecture.Predicates;
using Metalama.Framework.Fabrics;

namespace Doc.Architecture.Fabric_ForbidFloat
{
    internal class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            amender.Verify()
                .SelectTypes( typeof(float), typeof(double) )
                .CannotBeUsedFrom( r => r.Namespace( "**.Invoicing" ), "Use decimal numbers instead." );
        }
    }

    namespace Invoicing
    {
        internal class Invoice
        {
            public double Amount { get; set; }
        }
    }
}