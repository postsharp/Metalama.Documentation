---
uid: diagnostics
---
# Reporting and Suppressing Diagnostics

This article explains how to report a diagnostic (error, warning or information message) from an aspect, or to _suppress_ a diagnostic reported by the C# compiler or another aspect.

## Benefits

* **Avoid non-intuitive error messages**. Aspects that are applied to unexpected or untested kinds of declarations can throw very confusing exceptions or cause errors while compiling the transformed code. This confusion can be avoided by reporting clear error messages when the target of the aspect does not meet expectations. See also <xref:eligibility> for this use case.
* **Avoid confusing warnings**. The C# compiler and other analyzers are not aware that the code is being transformed by your aspect and they may, therefore, report irrelevant warnings. If you suppress these warnings from your aspects, developers using your aspect will be less confused and will not lose time suppressing the warnings manually.
* **Improve the productivity of the users of your aspect**. Overall, reporting and suppressing relevant diagnostics greatly improves the productivity of people using your aspect.
* **Diagnostic-only aspects**. You can also create aspects that _only_ report or suppress diagnostics, without transforming source code. See <xref:validation> for details and benefits.

## Reporting a diagnostic

[comment]: # (TODO: When to report a diagnostic? Eligibility vs diagnostic.)

To report a diagnostic:

1. Import the <xref:Metalama.Framework.Diagnostics> namespace.

2. Define a `static` field of type <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> in your aspect class. <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> specifies the diagnostic id, the severity, and the message formatting string.

    - For a message without formatting parameters or with weakly-typed formatting parameters, use the non-generic <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> class.
    - For a message with a single strongly-typed formatting parameter, use the generic <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition`1> class, e.g. `DiagnosticDefinition<int>`.
    - For a message with several strongly-typed formatting parameters, use the generic <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition`1> with a tuple, e.g. `DiagnosticDefinition<(int,string)>` for a message with two formatting parameters expecting a value of type `int` and `string`.

    > [!WARNING]
    > The aspect framework relies on the fact that diagnostics are defined as static fields of aspect classes. You will not be able to report a diagnostic that has not been declared on an aspect class of the current project.

3. To report a diagnostic, use the <xref:Metalama.Framework.Diagnostics.IDiagnosticSink.Report*?text=builder.Diagnostics.Report> method.

    The first parameter of the `Report` method is optional: it specifies the declaration to which the diagnostic relates. The aspect framework computes the file, line and column of the diagnostic based on this declaration. If you don't give a value for this parameter, the diagnostic will be reported for the target declaration of the aspect.

### Example

The following aspect needs a field named `_logger` to exist in the target type. Its `BuildAspect` method checks that this field exists and reports an error if it does not.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/ReportError.cs name="Report Error"]

## Suppressing a diagnostic

Sometimes the C# compiler or other analyzers may report warnings to the target code of your aspects. Since neither the C# compiler nor the analyzers know about your aspect, some of these warnings may be irrelevant. As an aspect author, it is a good practice to prevent the report of irrelevant warnings.

To suppress a diagnostic:

1. Import the <xref:Metalama.Framework.Diagnostics> namespace.

2. Define a `static` field of type <xref:Metalama.Framework.Diagnostics.SuppressionDefinition> in your aspect class. <xref:Metalama.Framework.Diagnostics.SuppressionDefinition> specifies the identifier of the diagnostic to suppress.

3. Call the <xref:Metalama.Framework.Diagnostics.IDiagnosticSink.Suppress*> method using `builder.Diagnostics.Suppress(...)` in the `BuildAspect` method.

### Example

The following logging aspect requires a `_logger` field to exist, but it is likely that this field will never be used in user code but only in generated code. Because the IDE does not see the generated code, it will report the `CS0169` warning, which is misleading and annoying to the user. The aspect suppresses this warning.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/SuppressWarning.cs name="Suppress Warning"]


## Advanced Example

The following aspect can be added to a field or property. It overrides the getter so that its value is retrieved from a service locator. This aspect assumes that the target class has a field named `_serviceProvider` and of type `IServiceProvider`. The aspect reports errors if this field is absent or of a wrong type. The C# compiler may report an error `CS0169` because it looks from source code that the `_serviceProvider` field is unused. Therefore, the aspect must suppress this diagnostic.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/LocalImport.cs name="Import Service"]

## Validating the target code after all aspects have been applied

When the `BuildAspect` method of your aspect is executed, it sees the code model as it was _before_ the aspect was applied.

If you need to validate the code after all aspects have been applied, see <xref:validation>.