using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Fabrics;
using Metalama.Framework.Validation;

namespace Doc.ForTestOnly_Fabric
{
    namespace ValidatedNamespace
    {

        public class Fabric : NamespaceFabric
        {
            private static DiagnosticDefinition<IDeclaration> _warning = new( "MY001",
                Severity.Warning, "'{0}' can only be invoked from a namespace that ends with '.Tests'." );

            public override void AmendNamespace( INamespaceAmender amender )
            {
                amender.WithTarget().ValidateReferences( this.ValidateReference, ReferenceKinds.All );
            }

            private void ValidateReference( in ReferenceValidationContext context )
            {
                if (
                    context.ReferencingType.Namespace != context.ReferencedDeclaration &&
                    !context.ReferencingType.Namespace.FullName.EndsWith( ".Tests" ) )
                {
                    context.Diagnostics.Report( _warning.WithArguments( context.ReferencedDeclaration ) );
                }
            }
        }
    }
}