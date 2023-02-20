---
uid: child-aspects
---

# Adding child aspects

An aspect can add other aspects to child declarations. Such aspects are called _child aspects_. Child aspects must satisfy two conditions:

* The child aspect class must be processed _after_ the parent aspect class; that is, the child aspect class must be positioned _before_ the parent class in the <xref:Metalama.Framework.Aspects.AspectOrderAttribute> ordering definition. See <xref:ordering-aspects> for details.
* The target declaration of the child aspect class must be contained in the target type of the parent aspect. For instance, a type-level aspect can add aspects to methods of the current type, but not to methods of a different type. A parameter-level aspect can add a child aspect to the parent method or to another method of the same type, but not of a different type.


An aspect can add child aspects from the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method by using the <xref:Metalama.Framework.Aspects.IAspectBuilder`1.Outbound?text=builder.Outbound> property, and then calling the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspect*> method. To add child aspects on members of the current declaration, use the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.Select*> or <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SelectMany*> method, and then call <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspect*>.


## Accessing the parent aspect

Parent aspects are listed in the <xref:Metalama.Framework.Aspects.IAspectPredecessor.Predecessors?text=IAspectPredecessor.Predecessors> property, which the child aspect can access from <xref:Metalama.Framework.Aspects.meta.AspectInstance?text=meta.AspectInstance> or <xref:Metalama.Framework.Aspects.IAspectBuilder.AspectInstance?text=builder.AspectInstance>.

