---
uid: validating-declarations
---

# Validating Declarations



Aspects generally validate their target declaration in the state it is before the aspect is applied. However, aspects sometimes need to validate the state after all aspects have been applied. This can be done by registering a _final validator_.

To validate the final state of the target declaration:

1. Define one or more static fields of type <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> as explained in <xref:diagnostics>.
2. Create a method of arbitrary name with the signature `void Validate( DeclarationValidationContext context )`. Implement the validation logic in this method. All the data you need is in the <xref:Metalama.Framework.Validation.DeclarationValidationContext> object. When you detect a rule violation, report a diagnostic as described in <xref:diagnostics>
3. Override or implement the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method of your aspect.
4. From `BuildAspect`, call <xref:Metalama.Framework.Validation.IValidatorReceiverSelector`1.With*?text=builder.With>, then chain with a call to <xref:Metalama.Framework.Validation.IValidatorReceiver`1.Validate*>. Pass the name of the new method that you have defined in the previous step. *It must be a method. It cannot be a delegate, lambda, or a local function.*

### Other ways to validate code from aspects

* <xref:eligibility>: this is the preferred way to validate how an aspect can be used.
* <xref:diagnostics>: this approach is equivalent to the current feature but it does not let you analyze the source (initial) code model or the final code model, only the code model before the aspect has been applied.


## Choosing which revision of the code model is validated
Since aspects can modify the code model, it can be useful to be aware of which revision of the code model is validated.

* The <xref:Metalama.Framework.Validation.IValidatorReceiver`1.ValidateReferences*> always validates the source code. References introduced by aspects cannot be validated.

* By default, fabrics validate the _source_ code. By calling <xref:Metalama.Framework.Validation.IValidatorReceiver`1.AfterAllAspects>, fabrics can validate the code model after all aspects have been applied.
  
* By default, aspects validate the code as it is before they are executed (see <xref:aspect-composition>). Call <xref:Metalama.Framework.Validation.IValidatorReceiver`1.AfterAllAspects> to validate the code after all aspects have been applied, or <xref:Metalama.Framework.Validation.IValidatorReceiver`1.BeforeAnyAspect> to validate the source code.