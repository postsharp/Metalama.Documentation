---
uid: Metalama.Framework.Aspects
summary: *content
---

This is namespace allows you to build aspects. Aspects are an algorithmic representation of a code transformation or validation.

For instance, adding logging to a method, or implementing `INotifyPropertyChanged`, can to a great extent be expressed as
an algorithm and therefore implemented as an aspect.

## Conceptual Documentation

See <xref:aspects>.

## Overview

To create an aspect, create a class that derives from <xref:System.Attribute> and implement the <xref:Metalama.Framework.Aspects.IAspect`1> interface. Alternatively, you can use one of the following classes, which already XX derive from <xref:System.Attribute>, have the proper  <xref:System.AttributeUsageAttribute>, and implement the <xref:Metalama.Framework.Aspects.IAspect`1> interface:

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
        OverrideMethod(...)
        IntroduceMethod(...)
        OverrideFieldOrProperty(...)
        IntroduceFieldOrProperty(...)
    }

    class IDiagnosticSink {
        Report(...)
        Suppress(...)
    }

    IAspect --> IAspectBuilder : BuildAspect() receives
    IAspectBuilder --> IAdviceFactory : exposes
    IAspectBuilder --> IDiagnosticSink : exposes

```

### Scope custom attributes

```mermaid
classDiagram

CompileTimeAttribute --|> ScopeAttribute : derives from
CompileTimeAttribute --|> ScopeAttribute  : derives from
RunTimeOnlyAttribute --|> ScopeAttribute  : derives from
TemplateAttribute --|> CompileTimeAttribute  : derives from
AdviceAttribute --|> TemplateAttribute  : derives from
IntroduceAttribute --|> AdviceAttribute  : derives from
InterfaceMemberAttribute --|> TemplateAttribute  : derives from

class CompileTimeAttribute
class RunTimeOnlyAttribute

```

### Adding child aspects and validators

```mermaid
classDiagram

    IAspect --> IAspectBuilder : receives
    
    class IAspectBuilder {
    }

    class IDeclarationSelector {
        WithTarget()
        withTargetMembers()
    }

    IAspectBuilder --|> IDeclarationSelector : derives from

    class IDeclarationSelection {
        AddAspect()
        AddAnnotation()
        RegisterValidator()
        RequireAspect()
    }

    IDeclarationSelector --> IDeclarationSelection : creates
```

### IAspectInstance, IAspectPredecessor

The <xref:Metalama.Framework.Aspects.IAspectPredecessor> facility allows aspects to access their parent (i.e. the artifact that created them): a parent aspect, a fabric, or a custom attribute.

```mermaid
classDiagram

    IAspect --> IAspectBuilder : receives
    
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

    IAspectBuilder --> IAspectInstance : exposes

    IAttribute --|> IAspectPredecessor : derives from

    IFabricInstance --|> IAspectPredecessor : derives from

    class IAspectPredecessor {

    }

    class IFabricInstance {
        Fabric
        TargetDeclaration
    }

    IAspectInstance --|> IAspectPredecessor : derives from

   class IAspectInstance {
       Aspect
       AspectClass
       IsSkipped
       Procecessors
       SecondaryInstances
       State
       TargetDeclaration
   }

    IAspectInstance --> IAspectState : exposes
    IAspectInstance --> IAspect : exposes
    IAspect --> IAspectState : reads & writes

   IAspectInstance *-- AspectPredecessor : has

   class AspectPredecessor {
       Kind
       Instance
   }

   AspectPredecessor --> IAspectPredecessor : exposes
   AspectPredecessor --> AspectPredecessorKind : has

   class AspectPredecessorKind {
       <<enum>>
       Attribute
       ChildAspect
       RequiredAspect
       Inherited
       Fabric
   }

   
```

## Namespace members