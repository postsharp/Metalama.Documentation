---
uid: code-fixes
---

# Offering Code Fixes & Refactorings

## Attaching code fixes to diagnostics

Whenever an aspect or fabric reports a diagnostic, it can attach a set of code fixes to this diagnostic by calling the <xref:Metalama.Framework.Diagnostics.IDiagnostic.WithCodeFixes*?text=IDiagnostic.WithCodeFixes> method. To create one-step code fixes, you can use the <xref:Metalama.Framework.CodeFixes.CodeFixFactory> class.


## Suggesting code refactorings without diagnostics

An aspect or fabric can also suggest a code refactoring without reporting a diagnostic by calling the <xref:Metalama.Framework.Diagnostics.IDiagnosticSink.Suggest*> method.

### Example

The following example shows an aspect that implements the `ToString` method. By default, it includes all public properties of the class in the `ToString` result. However, the developer using the aspect can opt out by adding `[NotToString]` to any property.

The aspect uses the <xref:Metalama.Framework.Diagnostics.IDiagnosticSink.Suggest*> method to add a code fix suggestion for all properties that are not yet annotated with `[NotToString]`.

[!include[ToString aspect with simple code fix](../../../code/Metalama.Documentation.SampleCode.AspectFramework/ToStringWithSimpleCodeFix.cs)]

## Building multi-step code fixes

To create a custom code fix, instantiate the <xref:Metalama.Framework.CodeFixes.CodeFix> class using the constructor instead of the <xref:Metalama.Framework.CodeFixes.CodeFixFactory> class.

The <xref:Metalama.Framework.CodeFixes.CodeFix> constructor accepts two arguments:

* the _title_ of the code fix, as displayed to the user, and
* a _delegate_ of type `Func<ICodeActionBuilder, Task>` that applies the code fix when it is selected by the user.

The title must be globally unique for the target declaration. Even two different aspects cannot provide two code fixes of the same title to the same declaration.

The delegate will typically use one of following methods of the <xref:Metalama.Framework.CodeFixes.ICodeActionBuilder> interface:

| Method | Description |
|------|----|
| <xref:Metalama.Framework.CodeFixes.ICodeActionBuilder.AddAttributeAsync*> | Adds a custom attribute to a declaration.
| <xref:Metalama.Framework.CodeFixes.ICodeActionBuilder.RemoveAttributesAsync*> | Removes all custom attributes of a given type to a given declaration and all contained declarations.
| <xref:Metalama.Framework.CodeFixes.ICodeActionBuilder.ApplyAspectAsync*> | Transforms the source code using an aspect (as if it were applied as a live template).

### Example

We are continuing the previous example, but instead of a single-step code fix, we want to offer the user the ability to switch from an aspect-oriented implementation of `ToString` to source code. That is, apply the aspect to the source code itself.

The custom does the following:

* Apply the aspect itself using <xref:Metalama.Framework.CodeFixes.ICodeActionBuilder.ApplyAspectAsync*>.
* Remove the `[ToString]` custom attribute.
* Remove the `[NotToString]` custom attributes.

[!include[ToString aspect with complex code fix](../../../code/Metalama.Documentation.SampleCode.AspectFramework/ToStringWithComplexCodeFix.cs)]

## Performance considerations

* Code fixes and refactorings are only useful at design time. At compile time, all code fixes will be ignored. If you want to avoid generating code fixes at compile time, you can make your logic conditional to the `MetalamaExecutionContext.Current.ExecutionScenario.CapturesCodeFixTitles` expression.

* The `Func<ICodeActionBuilder, Task>`  delegate is only executed when the code fix or refactoring is selected by the user. However, the whole aspect will be executed again which has two implications:
  *  The logic that _creates_ the delegate must be very fast because it is rarely useful. Any expensive logic should be moved to the _implementation_ of the delegate itself.
  *  If you want to avoid generating the delegate, you can make it conditional to the `MetalamaExecutionContext.Current.ExecutionScenario.CapturesCodeFixImplementations` expression.

* At design time, all code fix titles, including those added by the <xref:Metalama.Framework.Diagnostics.IDiagnosticSink.Suggest*> method,  are cached for the whole solution. Therefore, you should avoid adding a large number of suggestions. The current Metalama design is not suited for this scenario.


