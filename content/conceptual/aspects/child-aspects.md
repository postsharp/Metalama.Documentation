---
uid: child-aspects
level: 300
---

# Adding child aspects

An aspect can introduce other aspects to child declarations. These aspects are known as _child aspects_. Child aspects must adhere to two conditions:

* The child aspect class should be processed _after_ the parent aspect class. In other words, the child aspect class must be listed _before_ the parent class in the <xref:Metalama.Framework.Aspects.AspectOrderAttribute> ordering definition. Please refer to <xref:ordering-aspects> for more details.
* The target declaration of the child aspect class must be included in the target type of the parent aspect. For example, a type-level aspect can introduce aspects to methods of the current type, but not to methods of a different type. A parameter-level aspect can add a child aspect to the parent method or another method of the same type, but not of a different type.

An aspect can add child aspects from the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method by using the <xref:Metalama.Framework.Aspects.IAspectBuilder`1.Outbound?text=builder.Outbound> property, followed by calling the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspect*> method. To introduce child aspects on members of the current declaration, use the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.Select*> or <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SelectMany*> method, and then invoke <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspect*>.

## Accessing the parent aspect

Parent aspects are enumerated in the <xref:Metalama.Framework.Aspects.IAspectPredecessor.Predecessors?text=IAspectPredecessor.Predecessors> property. The child aspect can access this from <xref:Metalama.Framework.Aspects.meta.AspectInstance?text=meta.AspectInstance> or <xref:Metalama.Framework.Aspects.IAspectBuilder.AspectInstance?text=builder.AspectInstance>.
