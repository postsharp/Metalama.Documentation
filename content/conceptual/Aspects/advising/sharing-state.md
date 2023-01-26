---
uid: sharing-state-with-advice
---

# Sharing State with Advice

If you need to share _compile-time_ state between different pieces of advice, or between your implementation of the `BuildAspect` method and between advice, a few strategies are available to you.

> [!NOTE]
> If you need to share _run-time_ state with advice, you have to choose another strategy, for instance introducing a field in the target type and using it from several advice methods.

> [!WARNING]
> **DO NOT share state with an aspect field** if that state depends on the target declaration of the aspect. In case of inherited aspects, the same instance of the aspect class will be reused for all inherited targets.

## Sharing state with compile-time template parameters

This is the most straightforward way to pass values from your `BuildAspect` method to a template method, but it works only with method templates. For details, see <xref:template-parameters>.

## Sharing state with the Tags property

For event, properties or field templates, compile-time template parameters are not available. The simplest alternative is to use tags. Tags are arbitrary values assigned to arbitrary names.

To define and use tags:

1. In your implementation of the `BuildAspect` method, when adding the advice by calling a method of the <xref:Metalama.Framework.Advising.IAdviceFactory> interface, pass the tags as an anonymous object to the `tags` argument like this: `args: new { A = 5, B = "x", C = builder.Target.DeclaringType }` where `A`, `B` and `C` are three arbitrary names.

2. In your template method, the tags are available under the `meta.Tags` dictionary. You would for instance use the `meta.Tags["A"]` expression to access the tag named `A` that you defined in the previous step.

### Example

[!include[Tags](../../../../code/Metalama.Documentation.SampleCode.AspectFramework/Tags.cs)]

## Sharing state with the State property

You can use the <xref:Metalama.Framework.Aspects.IAspectBuilder.AspectState?text=IAspectBuilder.AspectState> property to store any aspect state that depends on the target declaration. This object is exposed on the <xref:Metalama.Framework.Aspects.IAspectInstance.AspectState?text=IAspectInstance.AspectState> property and is therefore also visible to inheritors and children aspects.
