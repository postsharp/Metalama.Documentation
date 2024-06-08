---
uid: immutability
---

Immutability is a well-known and very useful concept in software programming. An immutable type is a type whose instances cannot be modified after they have been created. Designs prioritizing immutable types tend to be simpler to understand than those relying more heavily on mutating objects. Examples of immutable types in C# are intrinsic types like `int`, `float` or `string`, enums, delegates, most system value types like `DateTime`, and collections of the `System.Collections.Immutable` namespace.

Metalama implements the Immutable Type concept in the `Metalama.Patterns.Immutability` package, specifically through the `[Immutable]` aspect and the `ConfigureImmutability` fabric method. 

This package has three purposes:

* Expose the immutability of a type to other aspects, such as Observable (see <xref:observability>), to facilitate the code analysis,
* Represent the design intent in a visible way and make the code more readable -- instead of letting the reader guess the immutability from the type implementaton,
* Report warnings if a type marked as immutable has mutable fields.


## Kinds of immutability

`Metalama.Patterns.Immutability` package understands two kinds of immutability, represented by the `ImmutabilityKind` type:


* _Shallow_ immutability means that all instance fields are _read-only_ and that no automatic property has a setter.
* _Deep_ immutability requires, recursively, that all instance fields and automatic properties are of a deeply immutable type.

Deep immutability ensures that there all objects that are reachable by resursively evaluating the fields or properties are immutable, which gives guarantees to some code analysis such as the one performed by the `Metalama.Patterns.Observability` package.

## System-supported types

The `Metalama.Patterns.Immutability` package contains rules that define the following types as _deeply immutable_:

* Intrinsic types like `bool`, `byte`, `int`, or `string`,
* Structs of the `System` namespace,
* Delegates and enums,
* Immutable collections of the `System.Collections.Immutable` namespace, when all type parameters are themselves deeply immutable.

Additionally, the following types are implificly classified as _shallowly immutable_:
* Read-only structs
* Immutable collections of the `System.Collections.Immutable` namespace, when any type parameter is not deeply immutable.

> [!WARNING]
> The `Metalama.Patterns.Immutability` package does _not_ try to guess the immutability of types by analyzing its fields. It relies purely on the rules defined above and the types manually marked as immutable, as described below.


## Marking types as immutable in source code

If you own the source code of a type, you can mark it as immutable by applying the `[Immutable]` aspect to it. By default, the `[Immutable]` aspect represents _shallow_ immutability. To represent deep immutability, supply the `ImmutabilityKind.Deep` argument.

The `[Immutable]` aspect reports warnings when fields are not read-only or then automatic properties have a setter. You can either resolve the warning, or ignore the warning using `#pragma warning disable`.

Note that the `Immutable` aspect is automatically inherited to derived types.

You can use a fabric method and <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspectIfEligible*> to add this aspect in bulk. See <xref:fabrics-adding-aspects> for details.

## Marking types for which you don't own the source code.

To assign an `ImmutabilityKind` to types to which you can't add the `[Immutable]` aspect, you can use the `ConfigureExtensibility` fabric extension method. You can pass either an `ImmutableKind`, if the type has always the same `ImmutableKind`, or an `IImmutabilityClassifier` if you want to determine the `ImmutableKind` dynamically. This mechanism is useful in generic types when their immutability depends on the immutability of type arguments.



