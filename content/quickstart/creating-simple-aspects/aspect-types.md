---
uid: aspect-types-class-diag
---

## Different types invloved in aspect creation  
The following diagram shows the relationship between several types available in `Metalama.Framework.Aspects` namespace to create simple aspects easily. 

```mermaid
classDiagram
    IAspect <|-- MethodAspect
    IAspect <|-- FieldAspect
    IAspect <|-- ConstructorAspect
    MethodAspect <|-- OverrideMethodAspect
    FieldAspect <|-- OverrideFieldOrPropertyAspect 
    IAspect <|-- EventAspect
    EventAspect <|-- OverrideEventAspect
```

As you can see at the top there is the `IAspect` interface the is implemented across many different types to offer overriding functionality for different parts of the code. 



## Selection of Aspect type 
The following table summaries what aspect class you might need to build a simple aspect based on the target 


|Target | Aspect class | Purpose 
|-------|-------------|------------
| Method | `OverrideMethodAspect` | To override a target method 
| Field  | `OverrideFieldOrPropertyAspect` | To override the getter/setter of a field or property 
| Constructor | `ConstructorAspect` | To override a constructor  
| Events | `OverrideEventsAspect` | To override the target event  