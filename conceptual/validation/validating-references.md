---
uid: validating-references
---

# Validating References

It is often useful to validate how a declaration is _used_ outside of its parent type or namespace. With Metalama, you can analyze _code references_ and:

* report errors and warnings for the code reference;
* suppress warnings reported by the C# compiler or by another analyzer for the code reference;
* suggest a code fix for the code reference.

You can validate references from an aspect or from a fabric, and both approaches are very similar.

## Validating references from an aspect

Additionally or instead of transforming the code of the target declaration, an aspect can validate how the target declaration is being _used_, i.e. it can validate references to its target.

To create an aspect that validates references:

1. Create an aspect class by inheriting one of the following classes, according to the kind of declarations you want to validate: <xref:Metalama.Framework.Aspects.CompilationAspect>, 
<xref:Metalama.Framework.Aspects.ConstructorAspect>, <xref:Metalama.Framework.Aspects.EventAspect>,
<xref:Metalama.Framework.Aspects.FieldAspect>, <xref:Metalama.Framework.Aspects.FieldOrPropertyAspect>, 
<xref:Metalama.Framework.Aspects.MethodAspect>, <xref:Metalama.Framework.Aspects.ParameterAspect>, 
<xref:Metalama.Framework.Aspects.PropertyAspect>, <xref:Metalama.Framework.Aspects.TypeAspect> or <xref:Metalama.Framework.Aspects.TypeParameterAspect>. If you want to validate several kinds of declarations, you can inherit your class from <xref:System.Attribute> and implement as many generic constructions of the <xref:Metalama.Framework.Aspects.IAspect`1> interface as needed.
2. In the aspect class, define one or more static fields of type <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> as explained in <xref:diagnostics>.
3. Create a method of arbitrary name with the signature `void ValidateReference( in ReferenceValidationContext context )`. Implement the validation logic in this method. All the data you need is in the <xref:Metalama.Framework.Validation.ReferenceValidationContext> object. When you detect a rule violation, report a diagnostic as described inin <xref:diagnostics>.
4. Override or implement the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method of your aspect.
5. From `BuildAspect`, call <xref:Metalama.Framework.Validation.IValidatorReceiverSelector`1.With*?text=builder.With>, then chain with a call to <xref:Metalama.Framework.Validation.IValidatorReceiver`1.ValidateReferences*>. Pass two parameters:
   - The new method that you have defined in the previous step. *It must be a method. It cannot be a delegate, lambda, or a local function.*
   - The kinds of references that you want to validate for instance `All` to validate all references, `TypeOf` to validate references in a `typeof` expression, and so on. You can select multiple kinds of references using the `|` operator.

### Example: ForTestOnly, aspect implementation

The following example implements a custom attribute `[ForTestOnly]` that enforces that the target of this attribute can be used only from a namespace that ends with `.Tests.`.

[!include[For Test Only, Aspect Implementation](../../code/Metalama.Documentation.SampleCode.AspectFramework/ForTestOnly.cs)]


##  Validating references from a fabric

To validate code from a fabric, the steps are almost the same as from an aspect. The difference is that you do not create an aspect class but a fabric class.


1. Create a fabric class by inheriting one of the following classes, according to the kind of declarations you want to validate: 
   * <xref:Metalama.Framework.Fabrics.TypeFabric> (the fabric class must be a nested class) to validate the nesting class;
   * <xref:Metalama.Framework.Fabrics.NamespaceFabric> to validate the containing namespace;
   * <xref:Metalama.Framework.Fabrics.ProjectFabric> to validate the current project;
   * <xref:Metalama.Framework.Fabrics.TransitiveProjectFabric> to validate any project referencing the current project.
2. In the fabric class, define one or more static fields of type <xref:Metalama.Framework.Diagnostics.DiagnosticDefinition> as explained in <xref:diagnostics>.
3. Create a method of arbitrary name with the signature `void ValidateReference( in ReferenceValidationContext context )`. Implement the validation logic in this method. All the data you need is in the <xref:Metalama.Framework.Validation.ReferenceValidationContext> object. When you detect a rule violation, report a diagnostic as described in <xref:diagnostics>.
4. Override the <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*>, <xref:Metalama.Framework.Fabrics.NamespaceFabric.AmendNamespace*> or <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*> method of your fabric.
5. From the `Amend*` method, call <xref:Metalama.Framework.Validation.IValidatorReceiverSelector`1.With*?text=builder.With>, then chain with a call to <xref:Metalama.Framework.Validation.IValidatorReceiver`1.ValidateReferences*>. Pass two parameters:
   - The new method that you have defined in the previous step. *It must be a method. It cannot be a delegate, lambda, or a local function.*
   - The kinds of references that you want to validate for instance `All` to validate all references, `TypeOf` to validate references in a `typeof` expression, and so on. You can select multiple kinds of references using the `|` operator.

### Example: ForTestOnly, fabric implementation

The following example implements the same logic as above, but using a fabric.

[!include[For Test Only](../../code/Metalama.Documentation.SampleCode.AspectFramework/ForTestOnly_Fabric.cs)]


## Cross-project validation

When a validator is added to a non-private member, the scope is not limited to the current project. Code references in referenced projects will also be validated.

To implement this feature, the aspects are _serialized_ during the build and stored as an embedded resource in each binary assembly. This resource is then _deserialized_ when the assembly is referenced in another project.

The cross-project scenario and the need for serialization are the reasons why the validation code must be in a stand-alone method of the aspect or fabric class.


## Passing state to the validation method

You must use <xref:Metalama.Framework.Aspects.IAspectBuilder.AspectState?text=builder.AspectState>. Do not store in an aspect field state that depends on the target of the aspect.