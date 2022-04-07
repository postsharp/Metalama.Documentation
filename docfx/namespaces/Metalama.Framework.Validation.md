---
uid: Metalama.Framework.Validation
summary: *content
---
This is namespace allows you to validate your code, the code that uses your aspects, or the code that references the code that uses your aspects.

## Overview

Aspect can register validators from their implementation of <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*?text=IAspect.BuildAspect>, and fabrics from their implementation of <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*>, <xref:Metalama.Framework.Fabrics.NamespaceFabric.AmendNamespace*> or <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*>. 

From these methods, call the <xref:Metalama.Framework.Validation.IValidatorReceiverSelector`1.WithTargetMembers*?text=amender.WithTargetMembers> or <xref:Metalama.Framework.Validation.IValidatorReceiverSelector`1.WithTarget*?text=amender.WithTarget> method exposed on the `builder` or `amender` parameter, then call <xref:Metalama.Framework.Validation.IValidatorReceiver`1.Validate*> or <xref:Metalama.Framework.Validation.IValidatorReceiver`1.ValidateReferences*>. These methods allow you to register a delegate. This delegate is then called and receive a context object of type <xref:Metalama.Framework.Validation.DeclarationValidationContext> or <xref:Metalama.Framework.Validation.ReferenceValidationContext>. The delegate can then analyze the code or reference, and report diagnostics.

The <xref:Metalama.Framework.Validation.IValidatorReceiver`1.ReportDiagnostic*>, <xref:Metalama.Framework.Validation.IValidatorReceiver`1.SuppressDiagnostic*> and <xref:Metalama.Framework.Validation.IValidatorReceiver`1.SuggestCodeFix*> methods are provided for convenience, and use <xref:Metalama.Framework.Validation.IValidatorReceiver`1.Validate*>.

To validate whether an aspect is eligible for a declaration (which involves validating the compilation _before_ the aspect has been applied), implement the <xref:Metalama.Framework.Eligibility.IEligible`1.BuildEligibility*> aspect method.

## Validated revision of the code model

Since aspects can modify the code model, it can be useful to be aware of which revision of the code model is validated.

* The <xref:Metalama.Framework.Validation.IValidatorReceiver`1.ValidateReferences*> always validates the source code. References introduced by aspects cannot be validated.

* By default, fabrics validate the _source_ code. By calling <xref:Metalama.Framework.Aspects.IAspectReceiverSelector`1.AfterAllAspects>, fabrics can validate the code modela after all aspects have been applied.
  
* By default, aspects validate the code as it is before they are executed (see <xref:aspect-composition>). Call <xref:Metalama.Framework.Aspects.IAspectReceiverSelector`1.AfterAllAspects> to validate the code after all aspects have been applied, or <xref:Metalama.Framework.Aspects.IAspectReceiverSelector`1.BeforeAnyAspect> to validate the source code.


## Class Diagram

```mermaid
classDiagram

    class ValidatorDelegate_Of_DeclarationValidationContext{
        <<delegate>>
        Invoke(context)
    }

     class ValidatorDelegate_Of_ReferenceValidationContext{
        <<delegate>>
        Invoke(context)
    }

    class DeclarationValidationContext {
        AspectState
        Declaration
        Diagnostics
    }

    class ReferenceValidationContext {
        AspectState
        DiagnosticLocation
        ReferencedDeclaration
        ReferencedType
        ReferenceKinds
        Syntax
    }

    
    class IValidatorReceiver {
        ValidateReferences()
        Validate()
        ReportDiagnostic()
        SuppressDiagnostic()
        SuggestCodeFix()
    }

    IValidatorReceiver --> ValidatorDelegate_Of_DeclarationValidationContext : registers
    IValidatorReceiver --> ValidatorDelegate_Of_ReferenceValidationContext : registers

ValidatorDelegate_Of_DeclarationValidationContext --> DeclarationValidationContext  : receives
    ValidatorDelegate_Of_ReferenceValidationContext --> ReferenceValidationContext : receives

    class IValidatorReceiverSelector {
        WithTarget()
        WithTargetMembers()
        AfterAllAspects()
        BeforeAnyAspect()
    }

    IValidatorReceiverSelector --> IValidatorReceiver : creates
    IValidatorReceiverSelector --> IValidatorReceiverSelector

    IAspectBuilder --|> IValidatorReceiverSelector : derives from
    IAmender --|> IValidatorReceiverSelector : derives from
    IAspect --> IAspectBuilder : receives
    Fabric --> IAmender : receives

```

## Namespace Members