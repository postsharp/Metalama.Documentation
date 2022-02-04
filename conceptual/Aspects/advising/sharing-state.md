---
uid: sharing-state-with-advices
---

# Sharing State with Advices

If you need to share _compile-time_ state between advices, or between advices and your implementation of the `BuildAspect` method, a few strategies are available to you.

> [!NOTE]
> If you need to share _run-time_ state between advices, you have to choose another strategy, for instance introducing a field in the target type and using it from several advices.

> [!WARNING]
> **DO NOT share state with an aspect field** if that state depends on the target declaration of the aspect. In case of inherited aspects, the same instance of the aspect class will be reused for all inherited targets.

## Sharing state with the State property

You can use the <xref:Metalama.Framework.Aspects.IAspectBuilder.State?text=IAspectBuilder.State> property to store any aspect state that depends on the target declaration. This object is exposed on the <xref:Metalama.Framework.Aspects.IAspectInstance.State?text=IAspectInstance.State> property and is therefore also visible to inheritors and children aspects.

## Sharing state with the Tags property

If your `BuildAspect` method needs to pass to a template method some state that is specific to an advice instance, you can construct an instance of the [Dictionary](xref:System.Collections.Generic.Dictionary%602)<[String](xref:System.String), [object](xref:System.Object)> class and assign all elements of state as tags in the name-value dictionary. In the template method, the tags are available under the `meta.Tags` dictionary.

### Example

[!include[Tags](../../../code/Metalama.Documentation.SampleCode.AspectFramework/Tags.cs)]