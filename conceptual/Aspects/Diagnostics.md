---
uid: diagnostics
---
# Reporting and suppressing diagnostics

This article explains how to report a diagnostic (error, warning or information message) from an aspect, or to _suppress_ a diagnostic reported by the C# compiler or another aspect.

## Reporting a diagnostic

[comment]: # (TODO: When to report a diagnostic? Eligibility vs diagnostic.)

To report a diagnostic:

1. Import the <xref:Metalama.Framework.Diagnostics> namespace.

2. Define a `static` field of type <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> in your aspect class. <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> specifies the diagnostic id, the severity, and the message formatting string.

    - For a message without formatting parameters or with weakly-typed formatting parameters, use the non-generic <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> class.
    - For a message with a single strongly-typed formatting parameter, use the generic <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition%601> class, e.g. `DiagnosticDefinition<int>`.
    - For a message with several strongly-typed formatting parameters, use the generic <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition%601> with a tuple, e.g. `DiagnosticDefinition<(int,string)>` for a message with two formatting parameters expecting a value of type `int` and `string`.

    > [!WARNING]
    > The aspect framework relies on the fact that diagnostics are defined as static fields of aspect classes. You will not be able to report a diagnostic that has not been declared on an aspect class of the current project.

3. To report a diagnostic, use the <xref:Metalama.Framework.Diagnostics.IDiagnosticSink.Report%2A?text=builder.Diagnostics.Report> method.

    The first parameter of the `Report` method is optional: it specifies the declaration to which the diagnostic relates. The aspect framework computes the file, line and column of the diagnostic based on this declaration. If you don't give a value for this parameter, the diagnostic will be reported for the target declaration of the aspect.


## Suppressing a diagnostic

Sometimes the C# compiler or other analyzers may report warnings to the target code of your aspects. Since neither the C# compiler nor the analyzers know about your aspect, some of these warnings may be irrelevant. As an aspect author, it is a good practice to prevent the report of irrelevant warnings.

To suppress a diagnostic:

1. Import the <xref:Metalama.Framework.Diagnostics> namespace.

2. Define a `static` field of type <xref:Metalama.Framework.Diagnostics.SuppressionDefinition> in your aspect class. <xref:Metalama.Framework.Diagnostics.SuppressionDefinition> specifies the identifier of the diagnostic to suppress.

3. Call the <xref:Metalama.Framework.Diagnostics.IDiagnosticSink.Suppress%2A> method using `builder.Diagnostics.Suppress(...)` in the `BuildAspect` method.

## Example

The following aspect can be added to a field or property. It overrides the getter so that its value is retrieved from a service locator. This aspect assumes that the target class has a field named `_serviceProvider` and of type `IServiceProvider`. The aspect reports errors if this field is absent or of a wrong type. The C# compiler may report an error `CS0169` because it looks from source code that the `_serviceProvider` field is unused. Therefore, the aspect must suppress this diagnostic.

[!include[Import Service](../../code/Metalama.Documentation.SampleCode.AspectFramework/LocalImport.cs)]
