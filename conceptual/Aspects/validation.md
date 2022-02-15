---
uid: validation
---

# Validating Code


## Validating References

Additionally or instead of transforming the code of the target declaration, an aspect can validate how the target declaration is being _used_, i.e., it can validate references to its target.

To validate references:

1. Define one or more static fields of type <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> as explained in <xref:diagnostics>.
2. Create a method of arbitrary name with the signature `void Validate( ReferenceValidationContext context )`. Implement the validation logic in this method. All the data you need is in the <xref:Metalama.Framework.Validation.ReferenceValidationContext> object. When you detect a rule violation, report a diagnostic as described inin <xref:diagnostics>
3. Override or implement the <xref:Metalama.Framework.Aspects.IAspect%601.BuildAspect%2A> method of your aspect.
4. From `BuildAspect`, call <xref:Metalama.Framework.Aspects.IDeclarationSelector%601.WithTarget%2A?text=builder.WithTarget> or <xref:Metalama.Framework.Aspects.IDeclarationSelector%601.WithTargetMembers%2A?text=builder.WithTargetMembers>, then chain with a call to <xref:Metalama.Framework.Aspects.IDeclarationSelection`1.RegisterReferenceValidator*>. Pass two parameters:
   - The name of the new method that you have defined in the previous step. *It must be a method. It cannot be a delegate, lambda, or a local function.*
   - The kinds of references that you want to validate for instance `All` to validate all references, `TypeOf` to validate references in a `typeof` expression, and so on.


### Example

[!include[For Test Only](../../code/Metalama.Documentation.SampleCode.AspectFramework/ForTestOnly.cs)]

### Cross-project validation

When a validator is added to a non-private member, the scope is not limited to the current project. Code references in referenced projects will also be validated.

To implement this feature, the aspects are _serialized_ during build and stored as an embedded resource in each binary assembly. This resource is then _deserialized_ when the assembly is referenced in another project.

## Validating the Final Compilation

Aspects generally validate their target declaration in the state it is before the aspect is applied. However, aspects sometimes need to validate the state after all aspects have been applied. This can be done by registering a _final validator_.

To validate the final state of the target declaration:

1. Define one or more static fields of type <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> as explained in <xref:diagnostics>.
2. Create a method of arbitrary name with the signature `void Validate( DeclarationValidationContext context )`. Implement the validation logic in this method. All the data you need is in the <xref:Metalama.Framework.Validation.ReferenceValidationContext> object. When you detect a rule violation, report a diagnostic as described inin <xref:diagnostics>
3. Override or implement the <xref:Metalama.Framework.Aspects.IAspect%601.BuildAspect%2A> method of your aspect.
4. From `BuildAspect`, call <xref:Metalama.Framework.Aspects.IDeclarationSelector%601.WithTarget%2A?text=builder.WithTarget> or <xref:Metalama.Framework.Aspects.IDeclarationSelector%601.WithTargetMembers%2A?text=builder.WithTargetMembers>, then chain with a call to <xref:Metalama.Framework.Aspects.IDeclarationSelection`1.RegisterFinalValidator*>. Pass the name of the new method that you have defined in the previous step. *It must be a method. It cannot be a delegate, lambda, or a local function.*


## Passing state to the validation method

You must use `IAspectBuilder.AspectState`. Do not store in an aspect field state that depends on the target of the aspect.