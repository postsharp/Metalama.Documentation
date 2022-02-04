---
uid: Metalama.Framework.Code.Invokers
summary: *content
---
This namespace defines invokers, which are objects that generate syntax that invokes methods or accesses properties, fields or events.

Where it makes sense, declarations expose an invoker factory (<xref:Metalama.Framework.Code.Invokers.IInvokerFactory%601>) on their `Invokers` property. 
The invoker factory interface has two properties:

-  <xref:Metalama.Framework.Code.Invokers.IInvokerFactory%601.Final> is equivalent to the `this` keyword in C#. It allows you to access the last override
   of the semantic.

-  <xref:Metalama.Framework.Code.Invokers.IInvokerFactory%601.Base> is equivalent to the `base` keyword in C#. It allows you to access the implementation
   prior to the current aspect layer.

-  <xref:Metalama.Framework.Code.Invokers.IInvokerFactory%601.ConditionalFinal> and <xref:Metalama.Framework.Code.Invokers.IInvokerFactory%601.ConditionalBase> generate a `.?` null-conditional access instead of `.`.


## Class Diagram

```mermaid
classDiagram

class IInvokerFactory~T~ {
   T? Base
   T? ConditionalBase
   T Final
   T ConditionalBase
}

IInvokerFactory~T~ --> IInvoker: exposes

IMethod --> IInvokerFactory~IMethodInvoker~: exposes
IEvent --> IInvokerFactory~IEventInvoker~: exposes
IFieldOrProperty --> IInvokerFactory~IFieldOrPropertyInvoker~: exposes

IMethodInvoker <|-- IInvoker
IFieldOrPropertyInvoker <|-- IInvoker
IPropertyInvoker <|-- IFieldOrPropertyInvoker
IEventInvoker <|-- IInvoker

class IMethod {
   Invokers
}

class IEvent {
   Invokers
}

class IFieldOrProperty {
   Invokers
}



class IMethodInvoker {
 +Invoke()
}

class IFieldOrPropertyInvoker {
   +GetValue()
   +SetValue()
}

class IPropertyInvoker {
   +GetIndexerValue()
   +SetIndexerValue()
}

class IEventInvoker {
   +AddDelegate()
   +RemoveDelegate()
}

```

## Example

The following aspect prints the value of all fields and automatic properties.

[!include[Dynamic](../../code/Metalama.Documentation.SampleCode.AspectFramework/PrintFieldValues.cs)]

## Namespace members