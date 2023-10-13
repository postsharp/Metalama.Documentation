---
uid: memoization
---

# Memoization

Memoization is an optimization technique that improves the performance of deterministic methods by caching their results. Metalama offers a simple and high-performance implementation of this technique through the <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> aspect.

This aspect is currently limited by to get-only properties and parameterless methods. The cached value of memoized methods and properties is stored in a field of the object itself. This allows for a high-performance implementation using `Interlocked.CompareExchange`. It is an alternative to the <xref:System.Lazy`1`> class, but is much simpler to use and has better performance characteristics.

To memoize a property or a method:

1. Add the [Metalama.Patterns.Memoization](https://www.nuget.org/packages/Metalama.Patterns.Memoization/) package into your project.
2. Add the <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> attribute to the get-only property or parameterless method.


> [!WARNING]
> The current implementation of the <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> aspect does not guarantee that the method is executed only once. However, it does guarantee that it always return the same value or object.

> [!NOTE]
> For nullable reference types and for value types, the cached value is stored in a <xref:System.Runtime.CompilerServices.StrongBox`1>, adding some memory allocation overhead in cases where many memoized properties or methods are evaluated. However this allows for minimal memory allocation when none or few of them are evaluated

## Example: memoization

The following example shows a typical use of the <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> aspect. It shows a `HashedBuffer` class, for which we need to optimize the performance of the `Hash` property and the `ToString` method. We assume that these members are only evaluated for a minority of instances of the `HashedBuffer` class, therefore the hash should not be pre-computed on the constructor. However, when they are evaluated, then we assume they are evaluated often, which means that we should cache the result. The <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> aspect offers a solution that is both simpler and of higher performance than the <xref:System.Lazy`1`> class.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Memoization/Memoize.cs]


## Memoization vs Caching

You can think of memoization as a simple form of caching. The <xref:Metalama.Patterns.Memoization.MemoizeAttribute?text=[Memoize]> aspect is most of the time a no-brainer, extremely simple to use and requiring no infrastructure.


| Factor                     | Memoization                               | Caching                                |
|----------------------------|-------------------------------------------|----------------------------------------|
| **Scope**                  | Local to a single class instance within the current process.  | Either local or shared, when runner as an external service such as Redis. |
| **Unicity of cache items** | Specific to the current instance or type. | Based on explicit `string` cache keys.
| **Complexity & overhead**  | Minimal overhead.                         | Significant overhead related the generation of cache keys and, in case of distributed caching, serialization.  |
| **Expiration & invalidation** | No expiration or invalidation .      | Advanced and configurable expiration policies and invalidation APIs.  |
