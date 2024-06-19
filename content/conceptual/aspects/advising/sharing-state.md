---
uid: sharing-state-with-advice
level: 400
summary: "The document provides strategies for sharing compile-time state between different pieces of advice or the BuildAspect method and the advice. It discusses sharing state with compile-time template parameters, the Tags property, and the AspectState property."
---

# Sharing state with advice

When you need to share _compile-time_ state between different pieces of advice, or between your implementation of the `BuildAspect` method and the advice, there are several strategies available to you.

> [!NOTE]
> If you need to share _run-time_ state with advice, a different strategy must be adopted. For instance, you could introduce a field in the target type and utilize it from several advice methods.

> [!WARNING]
> **DO NOT share state with an aspect field** if that state depends on the target declaration of the aspect. In scenarios involving inherited aspects or cross-project validators, the same instance of the aspect class will be reused across all inherited targets. Always design aspects as immutable classes.

## Sharing state with compile-time template parameters

This is the most direct approach for passing values from your `BuildAspect` method to a template method. However, it is only applicable with method templates. For more details, refer to <xref:template-parameters>.

## Sharing state with the Tags property

Compile-time template parameters are not available for event, property, or field templates. A straightforward alternative is to use tags, which are arbitrary name-value pairs.

To define and use tags:

1. In your implementation of the `BuildAspect` method, when adding the advice by calling a method of the <xref:Metalama.Framework.Advising.AdviserExtensions> class, pass the tags as an anonymous object to the `tags` argument. For example, `tags: new { A = 5, B = "x", C = builder.Target.DeclaringType }`. In this instance, `A`, `B`, and `C` are three arbitrary names.

2. In your template method, the tags are accessible under the `meta.Tags` dictionary. For instance, you would use the `meta.Tags["A"]` expression to access the tag named `A` that you defined in the previous step.

### Example

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Tags.cs name="Tags"]

## Sharing state with the AspectState property

You can utilize the <xref:Metalama.Framework.Aspects.IAspectBuilder.AspectState?text=IAspectBuilder.AspectState> property to store any aspect state that depends on the target declaration. This object is exposed on the <xref:Metalama.Framework.Aspects.IAspectInstance.AspectState?text=IAspectInstance.AspectState> property and is visible to child aspects and aspects that inherit from them.



