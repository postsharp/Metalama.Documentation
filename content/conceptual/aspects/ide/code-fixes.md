---
uid: code-fixes
---

# Offering code fixes and refactorings

## Attaching code fixes to diagnostics

Whenever an aspect or fabric reports a diagnostic, it can attach a set of code fixes to this diagnostic by calling the <xref:Metalama.Framework.Diagnostics.IDiagnostic.WithCodeFixes*?text=IDiagnostic.WithCodeFixes> method. You can use the <xref:Metalama.Framework.CodeFixes.CodeFixFactory> class to create single-step code fixes.


## Suggesting code refactorings without diagnostics

An aspect or fabric can also suggest a code refactoring without reporting a diagnostic by calling the <xref:Metalama.Framework.Diagnostics.IDiagnosticSink.Suggest*> method.

### Example

The following example shows an aspect that implements the `ToString` method. By default, it includes all public properties of the class in the `ToString` result. However, the developer using the aspect can opt out by adding `[NotToString]` to any property.

The aspect uses the <xref:Metalama.Framework.Diagnostics.IDiagnosticSink.Suggest*> method to add a code fix suggestion for all properties that are not yet annotated with `[NotToString]`.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/ToStringWithSimpleCodeFix.cs name="ToString aspect with simple code fix"]

## Building multi-step code fixes

To create a custom code fix, instantiate the <xref:Metalama.Framework.CodeFixes.CodeFix> class using the constructor instead of the <xref:Metalama.Framework.CodeFixes.CodeFixFactory> class.

The <xref:Metalama.Framework.CodeFixes.CodeFix> constructor accepts two arguments:

* The _title_ of the code fix, which will be displayed to the user, and
* A _delegate_ of type `Func<ICodeActionBuilder, Task>`, which will apply the code fix when it is selected by the user.

The title must be globally unique for the target declaration. Even two different aspects cannot provide two code fixes with the same title to the same declaration.

The delegate will typically use one of the following methods of the <xref:Metalama.Framework.CodeFixes.ICodeActionBuilder> interface:

| Method | Description |
|------|----|
| <xref:Metalama.Framework.CodeFixes.ICodeActionBuilder.AddAttributeAsync*> | Adds a custom attribute to a declaration.
| <xref:Metalama.Framework.CodeFixes.ICodeActionBuilder.RemoveAttributesAsync*> | Removes all custom attributes of a given type to a given declaration and all contained declarations.
| <xref:Metalama.Framework.CodeFixes.ICodeActionBuilder.ApplyAspectAsync*> | Transforms the source code using an aspect (as if it were applied as a live template).

### Example

We are continuing the previous example, but instead of a single-step code fix, we want to offer the user the ability to switch from an aspect-oriented implementation of `ToString` by applying the aspect to the source code itself.

The custom code fix does the following:

* Apply the aspect using the <xref:Metalama.Framework.CodeFixes.ICodeActionBuilder.ApplyAspectAsync*> method.
* Remove the `[ToString]` custom attribute.
* Remove the `[NotToString]` custom attributes.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/ToStringWithComplexCodeFix.cs name="ToString aspect with complex code fix"]

## Performance considerations

* Code fixes and refactorings are only useful at design time. At compile time, all code fixes will be ignored. If you want to avoid generating code fixes at compile time, you can make your logic conditional upon the `MetalamaExecutionContext.Current.ExecutionScenario.CapturesCodeFixTitles` expression.

* The `Func<ICodeActionBuilder, Task>`  delegate is only executed when the code fix or refactoring is selected by the user. However, the entire aspect will be executed again which has two implications:
  * The logic that _creates_ the delegate must be very efficient, because it is rarely used. Any expensive logic should be moved to the _implementation_ of the delegate itself.
  * If you want to avoid generating the delegate, you can make it conditional upon the `MetalamaExecutionContext.Current.ExecutionScenario.CapturesCodeFixImplementations` expression.

* At design time, all code fix titles, including those added by the <xref:Metalama.Framework.Diagnostics.IDiagnosticSink.Suggest*> method, are cached for the whole solution. Therefore, you should avoid adding a large number of suggestions. The current Metalama design is not suited for this scenario.

