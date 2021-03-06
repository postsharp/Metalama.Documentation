---
uid: aspect-abilities
---

# Abilities of Aspects

An aspect is, by definition, a class that implements the <xref:Metalama.Framework.Aspects.IAspect`1> generic interface. The generic parameter of this interface is the type of declaration  <!--- I may be wrong in thinking that the singular is more appropriate here --> to which that aspect can be applied. For instance, an aspect that can be applied to a method must implement the `IAspect<IMethod>` interface and an aspect that can be applied to a named type must implement `IAspect<INamedType>`.

Aspects have different abilities which are expounded below. The aspect author can use or configure these abilities in the following method <!--- as you only list one method this should be singular -->inherited from the <xref:Metalama.Framework.Aspects.IAspect`1> interface:

* <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> builds the aspect _instance_ applied on a specific _target declaration_, thanks to a <xref:Metalama.Framework.Aspects.IAspectBuilder`1>;

```mermaid
classDiagram
    
    class IAspect {
        BuildAspect(IAspectBuilder)
    }

    class IAspectBuilder {
        SkipAspect()
        TargetDeclaration
        AdviceFactory
    }

    class IAdviceFactory {
        Override*(...)
        Introduce*(...)
        AddInitializer*(...)
    }

    class IDiagnosticSink {
        Report(...)
        Suppress(...)
        Suggest(...)
    }

    IAspect --> IAspectBuilder : BuildAspect() receives
    IAspectBuilder --> IAdviceFactory : exposes
    IAspectBuilder --> IDiagnosticSink : exposes

```


## Transforming code

Aspects can perform the following transformations to code:

* Apply a template to an existing method, i.e. add generated code to user-written code.
* Introduce a new generated member to an existing type.
* Implement an interface into a type.

For details, see <xref:advising-code>


## Reporting and suppressing diagnostics

Aspects can report diagnostics (a single word for errors, warnings and information messages), and can suppress diagnostics reported by the C# compiler, analyzers, or other aspects.

For details about this feature, see <xref:diagnostics>.

## Suggest code fixes

Aspects can suggest code fixes to any diagnostic it reports, or suggest code refactorings.


## Validating the code that references the target declaration

Aspects can validate not only the target code, but also any _reference_ to the target declaration.

See <xref:validation>.


## Eligibility

Aspects can define on which declarations they can be legally applied.

See <xref:eligibility>.


## Adding other aspects to be applied

Aspects can add other aspects to the target code.

See <xref:child-aspects>.


## Disabling itself

If an aspect instance decides that it cannot be applied to the target to which it has been applied, its implementation of the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method can call the <xref:Metalama.Framework.Aspects.IAspectBuilder.SkipAspect> method. The effect of this method is to prevent the aspect from providing any advice or child aspect and to set the <xref:Metalama.Framework.Aspects.IAspectInstance.IsSkipped> to `true`.

The aspect may or may not report a diagnostic before calling <xref:Metalama.Framework.Aspects.IAspectBuilder.SkipAspect>. Calling this method does not report any diagnostic.

## Customize its appareance in the IDE.

By default, an aspect class is represented in the IDE by the name of this class without the `Attribute` suffix, if any. To override the default name, annotate the aspect class with the <xref:System.ComponentModel.DisplayNameAttribute> annotation.



## Examples



### Example: an aspect targeting methods, fields and properties

The following example shows an aspect that targets methods, fields and properties with a single implementation class.

[!include[Aspect Targeting Methods, Fields and Properties](../../../code/Metalama.Documentation.SampleCode.AspectFramework/LogMethodAndProperty.cs)]

## Code model versioning
