// This is public domain Metalama sample code.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using Metalama.Framework.Validation;
using System;

namespace Doc.ForTestOnly;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Struct |
    AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Property |
    AttributeTargets.Event )]
public class ForTestOnlyAttribute : Attribute, IAspect<IMember>
{
    private static readonly DiagnosticDefinition<IDeclaration> _warning = new(
        "MY001",
        Severity.Warning,
        "'{0}' can only be invoked from a namespace that ends with Tests." );

    public void BuildAspect( IAspectBuilder<IMember> builder )
    {
        builder.Outbound.ValidateOutboundReferences( this.ValidateReference, ReferenceGranularity.Namespace );
    }

    private void ValidateReference( ReferenceValidationContext context )
    {
        if ( !context.Origin.Namespace.FullName.EndsWith( ".Tests", StringComparison.Ordinal ) )
        {
            context.Diagnostics.Report(
                r => r.ReferencingDeclaration.IsContainedIn( context.Destination.Type ) ? null : _warning.WithArguments( context.Destination.Namespace ) );
        }
    }
}