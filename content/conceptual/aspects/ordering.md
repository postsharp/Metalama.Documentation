---
uid: ordering-aspects
---

# Ordering Aspects

When several aspect classes are defined, their order of execution is important.

## Concepts

### Per-project ordering

In Metalama, the order of execution is _static_. It is principally a concern of the aspect library author, not a concern of the users of the aspect library.

Each aspect library should define the order of execution of aspects it defines, not only with regards to other aspects of the same library, but also to aspects defined in referenced aspect libraries.

When a project uses two unrelated aspect libraries or when a project defines its own aspects, it has to define the ordering in the project itself.

### Order of application versus order of execution

Metalama follows what we call the "matryoshka" model: your source code is the innermost doll, and aspects are added _around_ it. The fully compiled code, with all aspects, is like the fully assembled matryoshka. Executing a method is like disassembling the matryoshka: you start with the outermost shell, and you continue to the original implementation.

![](matryoshka.png "CC BY-SA 3.0 by Wikipedia user Fanghong")

It is important to remember that Metalama builds the matryoshka from the inside to the outside, but the code is executed from the outside to the inside; in other words, the source code is executed _last_.

Therefore, the aspect application order and the aspect execution order are _opposite_.

## Specifying the execution order

Aspects must be ordered using the <xref:Metalama.Framework.Aspects.AspectOrderAttribute> assembly-level custom attribute. The order of the aspect classes in the attribute corresponds to their order of _execution_.

```cs
using Metalama.Framework.Aspects;
[assembly: AspectOrder( typeof(Aspect1), typeof(Aspect2), typeof(Aspect3))]
```

You can specify _partial_ order relationships. The aspect framework will merge all partial relationships and determine the global order for the current project.

For instance, the following code snippet is equivalent to the previous one:

```cs
using Metalama.Framework.Aspects;
[assembly: AspectOrder( typeof(Aspect1), typeof(Aspect2))]
[assembly: AspectOrder( typeof(Aspect2), typeof(Aspect3))]
```

This is like in mathematics: if we have `a < b` and `b < c`, then we have `a < c` and the ordered sequence is `{a, b, c}`.

If you specify conflicting relationships or import aspect library that defines a conflicting ordering, Metalama will emit a compilation error.

> [!NOTE]
> Metalama will merge all `[assembly: AspectOrder(...)]` attributes that it finds not only in the current project, but also in all referenced projects or libraries. Therefore, you don't need to repeat the `[assembly: AspectOrder(...)]` attributes in all projects that use aspects. It is sufficient to define them in projects that define aspects.

[comment]: # (TODO: mention what happens when the ordering is not fully specified?)

### Example

The following code snippet shows two aspects that both add a method to the target type and display the list of methods that were defined on the target type before the aspect was applied. The order of execution is defined as `Aspect1 < Aspect2`. You can see from this example that the order of application of aspects is opposite. `Aspect2` is applied first and sees the source code, then `Aspect1` is applied and sees the method added by `Aspect1`. The modified method body of `SourceMethod` shows that the aspects are executed in this order: `Aspect1`, `Aspect2`, then the original method.

[!metalama-sample  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Ordering.cs name="Ordering"]


## Several instances of the same aspect type on the same declaration

When there are several instances of the same aspect type on the same declaration, a single instance of the aspect, which is named the _primary_ instance, gets applied to the target. The other instances, named _secondary_ instances, are exposed on the <xref:Metalama.Framework.Aspects.IAspectInstance.SecondaryInstances?text=IAspectInstance.SecondaryInstances> property, which you can access from <xref:Metalama.Framework.Aspects.meta.AspectInstance?text=meta.AspectInstance> or <xref:Metalama.Framework.Aspects.IAspectBuilder.AspectInstance?text=builder.AspectInstance>. It is the responsibility of the aspect implementation to decide what to do with the secondary aspect instances.

The primary aspect instance is the instance that has been applied to the "closest" to the target declaration. The sorting criteria are the following:
    1. Aspects defined using a _custom attribute_.
    2. Aspects added by another aspect (child aspects).
    3. Aspects inherited from another declaration.
    4. Aspects added by a fabric.

Within these individual categories, the ordering is currently undefined, which means that the build may be nondeterministic if the aspect implementation relies on that ordering.

[comment]: # (TODO: Example of handling secondary instances)


