using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Validation;
using System;

namespace Doc.ForTestOnly
{

    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Struct |
                     AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Property |
                     AttributeTargets.Event )]
    public class ForTestOnlyAttribute : Attribute, IAspect<IMember>
    {
        private static DiagnosticDefinition<IDeclaration> _warning = new("MY001",
            Severity.Warning, "'{0}' can only be invoked from a namespace that ends with Tests.");

        public void BuildAspect( IAspectBuilder<IMember> builder )
        {
            builder.WithTarget().ValidateReferences( this.ValidateReference, ReferenceKinds.All );
        }

        private void ValidateReference( in ReferenceValidationContext context )
        {
            if (
                context.ReferencingType != context.ReferencedDeclaration.GetDeclaringType() &&
                !context.ReferencingType.Namespace.FullName.EndsWith( "Test" ) )
            {
                context.Diagnostics.Report( _warning.WithArguments( context.ReferencedDeclaration ) );
            }
        }
    }
}