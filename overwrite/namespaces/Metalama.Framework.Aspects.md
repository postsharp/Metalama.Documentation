---
uid: Metalama.Framework.Aspects
summary: *content
---

This namespace enables you to build aspects. Aspects represent an algorithmic approach to code transformation or validation.

For instance, tasks such as adding logging to a method or implementing `INotifyPropertyChanged` can largely be expressed as an algorithm and, therefore, implemented as an aspect.

## Conceptual Documentation

Refer to <xref:aspects> for more information.

## Overview

To create an aspect, you need to create a class that derives from <xref:System.Attribute> and implement the <xref:Metalama.Framework.Aspects.IAspect`1> interface. Alternatively, you can use one of the following classes, which already derive from <xref:System.Attribute>, have the appropriate <xref:System.AttributeUsageAttribute>, and implement the <xref:Metalama.Framework.Aspects.IAspect`1> interface:

* <xref:Metalama.Framework.Aspects.CompilationAspect>
* <xref:Metalama.Framework.Aspects.ConstructorAspect>
* <xref:Metalama.Framework.Aspects.EventAspect>
* <xref:Metalama.Framework.Aspects.FieldAspect>
* <xref:Metalama.Framework.Aspects.FieldOrPropertyAspect>
* <xref:Metalama.Framework.Aspects.MethodAspect>
* <xref:Metalama.Framework.Aspects.ParameterAspect>
* <xref:Metalama.Framework.Aspects.PropertyAspect>
* <xref:Metalama.Framework.Aspects.TypeAspect>
* <xref:Metalama.Framework.Aspects.TypeParameterAspect>

## Class Diagrams

### Aspect builders

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
        Override(...)
        Introduce*(...)
        AddInitializer*(...)
    }

    class ScopedDiagnosticSink {
        Report(...)
        Suppress(...)
    }

    IAspectBuilder <-- IAspect : BuildAspect() receives
    IAdviceFactory <-- IAspectBuilder : exposes
    ScopedDiagnosticSink <-- IAspectBuilder : exposes

```

### Scope custom attributes

```mermaid
classDiagram

ScopeAttribute <|-- CompileTimeAttribute  : derives from
ScopeAttribute <|-- RunTimeOnlyAttribute  : derives from
CompileTimeAttribute <|-- TemplateAttribute  : derives from
TemplateAttribute <|-- AdviceAttribute  : derives from
AdviceAttribute <|-- IntroduceAttribute  : derives from
TemplateAttribute <|-- InterfaceMemberAttribute  : derives from

class CompileTimeAttribute
class RunTimeOnlyAttribute

```

### Adding child aspects and validators

```mermaid
classDiagram

    IAspectBuilder <-- IAspect : receives

    class IAspectBuilder {
    }

    class IDeclarationSelector {
        With()
    }

    IDeclarationSelector <|-- IAspectBuilder : derives from

    class IDeclarationSelection {
        AddAspect()
        AddAnnotation()
        RegisterValidator()
        RequireAspect()
    }

    IDeclarationSelection <-- IDeclarationSelector : creates
```

### IAspectInstance, IAspectPredecessor

The <xref:Metalama.Framework.Aspects.IAspectPredecessor> facility allows aspects to access their parent (i.e. the artifact that created them): a parent aspect, a fabric, or a custom attribute.

```mermaid
classDiagram

   class AspectPredecessorKind {
       <<enum>>
       Attribute
       ChildAspect
       RequiredAspect
       Inherited
       Fabric
   }

    class IAspect {
        BuildAspect(IAspectBuilder)
    }

    class IAspectBuilder {
        AspectInstance
    }

    class  IAspectState {

    }

    class IAttribute {
        ContainingDeclaration
        Type
        Constructor
        ConstructorArguments
        NamedArguments
    }

    IAspectInstance <-- IAspectBuilder : exposes

    IAspectBuilder <-- IAspect : receives

    IAspectPredecessor <|-- IAttribute : derives from

    IAspectPredecessor <|-- IFabricInstance : derives from

    class IAspectPredecessor {

    }

    class IFabricInstance {
        Fabric
        TargetDeclaration
    }

   class IAspectInstance {
       Aspect
       AspectClass
       IsSkipped
       Procecessors
       SecondaryInstances
       State
       TargetDeclaration
   }

    IAspectState <-- IAspectInstance : exposes
    IAspect <-- IAspectInstance : exposes
    IAspectPredecessor <|-- IAspectInstance : derives from
    IAspectState <-- IAspect : reads & writes

   IAspectInstance *-- AspectPredecessor : has

   class AspectPredecessor {
       Kind
       Instance
   }

   IAspectPredecessor <-- AspectPredecessor : exposes
   AspectPredecessorKind <-- AspectPredecessor : has

```

## Namespace members
