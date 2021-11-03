---
uid: Caravela.Framework.Diagnostics
summary: *content
---
This is namespace allows you to report or suppress diagnostics from your aspect code.

To report a diagnostic, you must first define a static field of type <xref:Caravela.Framework.Diagnostics.DiagnosticDefinition> or 
<xref:Caravela.Framework.Diagnostics.DiagnosticDefinition%601> in your aspect class. Note that this step is mandatory.

You can then use report a diagnostic from your implementation of <xref:Caravela.Framework.Aspects.IAspect%601.BuildAspect%2A>
by calling the <xref:Caravela.Framework.Diagnostics.IDiagnosticSink.Report%2A> method 
exposed on the consuming <xref:Caravela.Framework.Aspects.IAspectLayerBuilder.Diagnostics> property of <xref:Caravela.Framework.Aspects.IAspectBuilder>.

To suppress of diagnostic, you must first define it as a static field of type <xref:Caravela.Framework.Diagnostics.SuppressionDefinition> in your aspect class.
You can then suppress a diagnostic from any declaration from an aspect using the 
<xref:Caravela.Framework.Diagnostics.IDiagnosticSink.Suppress%2A>
method.

For more information, see <xref:diagnostics>.
