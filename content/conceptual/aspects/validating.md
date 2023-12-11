---
uid: aspect-validating
level: 300
summary: "The document explains how to validate code from an aspect using Metalama Framework, covering validation before or after aspects and validating code references. It includes examples and steps to follow."
---

# Validating code from an aspect

Validating source code and providing meaningful error messages is a critical feature of most aspects. Failure to do so can result in confusing error messages for the aspect's user or even invalid behavior at runtime.

The first two techniques for validating code involve defining eligibility (see <xref:eligibility>) and reporting errors from the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method (see <xref:diagnostics>). In this article, we introduce two additional techniques:

* Validating the code _before_ applying any aspect, or _after_ applying all aspects.
* Validating _references_ to the target declaration of the aspect.

## Validating code before or after aspects

By default, the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> receives the version of the code model _before_ applying the current aspect. However, there may be instances where you need to validate a different version of the code model. Metalama allows you to validate three versions:

* Before the current aspect has been applied,
* _Before any_ aspect has been applied, or
* _After all_ aspects have been applied.

To validate a different version of the code model, follow these steps:

1. Define one or more static fields of type <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> as explained in <xref:diagnostics>.
2. Create a method with the signature `void Validate(in DeclarationValidationContext context)`. Implement the validation logic in this method. All the data you need is in the <xref:Metalama.Framework.Validation.DeclarationValidationContext> object. When you detect a rule violation, report a diagnostic as described in <xref:diagnostics>.
3. Override or implement the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method of your aspect. From this method:
   1. Access the <xref:Metalama.Framework.Aspects.IAspectBuilder`1.Outbound*?text=builder.Outbound> property,
   2. Call the <xref:Metalama.Framework.Validation.IValidatorReceiver`1.AfterAllAspects> or <xref:Metalama.Framework.Validation.IValidatorReceiver`1.BeforeAnyAspect> method to select the version of the code model,
   3. Select declarations to be validated using the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SelectMany*> and <xref:Metalama.Framework.Aspects.IAspectReceiver`1.Select*> methods,
   4. Call the <xref:Metalama.Framework.Validation.IValidatorReceiver.ValidateReferences*> method and pass a delegate to the validation method.

### Example: requiring a later aspect to be applied

The following example demonstrates how to validate that the target type of the `Log` aspect contains a field named `_logger`. The implementation allows the `_logger` field to be introduced _after_ the `Log` aspect has been applied, thanks to a call to <xref:Metalama.Framework.Validation.IValidatorReceiver`1.AfterAllAspects>.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/ValidateAfterAllAspects.cs]

## Validating code references

Aspects can validate not only the declaration to which they are applied but also how this target declaration is used. In other words, aspects can validate _code references_.

To create an aspect that validates references:

1. In the aspect class, define one or more static fields of type <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> as explained in <xref:diagnostics>.
2. Create a method of arbitrary name with the signature `void ValidateReference( ReferenceValidationContext context )`. Implement the validation logic in this method. All the data you need is in the <xref:Metalama.Framework.Validation.ReferenceValidationContext> object. When you detect a rule violation, report a diagnostic as described in <xref:diagnostics>. Alternatively, you can create a class implementing the <xref:Metalama.Framework.Validation.ReferenceValidator> abstract class.
3. Override or implement the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method of your aspect. From this method:
   1. Access the <xref:Metalama.Framework.Aspects.IAspectBuilder`1.Outbound*?text=builder.Outbound> property,
   2. Select declarations to be validated using the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SelectMany*> and <xref:Metalama.Framework.Aspects.IAspectReceiver`1.Select*> methods,
   3. Call the <xref:Metalama.Framework.Validation.IValidatorReceiver.ValidateReferences*> method and pass a delegate to the validation method or an instance of the validator class.

### Example: ForTestOnly, aspect implementation

The following example implements a custom attribute `[ForTestOnly]` that enforces that the target of this attribute can only be used from a namespace that ends with `.Tests.`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ForTestOnly.cs name="For Test Only, Aspect Implementation"]

