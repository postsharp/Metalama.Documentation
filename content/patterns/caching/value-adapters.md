---
uid: caching-value-adapters
title: "Caching Special Types with Value Adapters"
product: "postsharp"
categories: "Metalama;AOP;Metaprogramming"
---
# Caching Special Types with Value Adapters

Some types, for instance the <xref:System.Collections.Generic.IEnumerator`1> interface or the <xref:System.IO.Stream> class cannot be directly cached because the position of the enumerator or stream can be changed by the caller. Some other interfaces like <xref:System.Collections.Generic.IEnumerable`1> cannot be cached because the real value may be a LINQ expression, and it is not useful to cache the LINQ expression itself. However, you may still want to cache methods returning these types. If you wrote the code manually, you would simply cache a different type, for instance a list or an array of bytes. 

Metalama addresses this problem by the concept of a *value adapter*. A value adapter allows you to store another type than the one than the return type of the cached method. The method return value is called the *exposed value* because this is the value exposed by your API. The exposed value must be type-compatible with the method return type. The value that is actually stored in cache is called the *stored value*. For instance, for a method returning a <xref:System.IO.Stream>, the stored value is an array of bytes and the exposed value is a <xref:System.IO.MemoryStream>. 


## Standard value adapters

The following value adapters are used automatically by default:

| Return type | Stored type | Exposed type | Comments |
|-------------------------------------------------|-------------|--------------|----------|
| <xref:System.Collections.Generic.IEnumerable`1> | <xref:System.Collections.Generic.List`1> | <xref:System.Collections.Generic.List`1> |  |
| <xref:System.Collections.Generic.IEnumerator`1> | <xref:System.Collections.Generic.List`1> | <xref:System.Collections.Generic.List`1.Enumerator> | The <xref:System.Collections.IEnumerator.Reset> method is not supported by the exposed value.  |
| <xref:System.IO.Stream> | <xref:System.Byte> []  | <xref:System.IO.MemoryStream> |  |


## Implementing a custom value adapter


### To implement a custom value adapter:

1. Add a reference to the [Metalama.Patterns.Caching](https://www.nuget.org/packages/Metalama.Patterns.Caching/) package. 


2. Create a class implementing the <xref:Metalama.Patterns.Caching.ValueAdapters.IValueAdapter`1> interface or the <xref:Metalama.Patterns.Caching.ValueAdapters.IValueAdapter> interface. 


3. Register the class from the previous step using one of the overloads of the <xref:Metalama.Patterns.Caching.ValueAdapters.ValueAdapterFactory.Register*> method. 

    Each caching backend has its own instance of the <xref:Metalama.Patterns.Caching.ValueAdapters.ValueAdapterFactory> class available via the <xref:Metalama.Patterns.Caching.CachingService.ValueAdapters> property. 


> [!NOTE]
> Null values are handled automatically outside of the value adapters.


### Example

In this example, we create the ``EnumerableValueAdapter`1`` class, which transforms instances of the <xref:System.Collections.Generic.IEnumerable`1> interface into an array. 

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Backends;
using Metalama.Patterns.Caching.ValueAdapters;

namespace Metalama.Samples.Caching.ValueAdapters
{
    class EnumerableValueAdapter<T> : ValueAdapter<IEnumerable<T>>
    {
        public override IEnumerable<T> GetExposedValue(object storedValue)
        {
            return (IEnumerable<T>)storedValue;
        }

        public override object GetStoredValue(IEnumerable<T> value)
        {
            Console.WriteLine("Caching enumerable.");
            return value.ToArray();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MemoryCachingBackend memoryCachingBackend = new MemoryCachingBackend();
            memoryCachingBackend.ValueAdapters.Register(typeof(IEnumerable<>), typeof(EnumerableValueAdapter<>));
            CachingServices.DefaultBackend = memoryCachingBackend;

            Console.WriteLine("Cache miss:");
            foreach (int value in GetValues())
            {
                Console.WriteLine("Value {0} obtained.", value);
            }

            Console.WriteLine("Cache hit:");
            foreach (int value in GetValues())
            {
                Console.WriteLine("Value {0} obtained.", value);
            }

            Console.WriteLine("Cache miss for null:");
            Console.WriteLine(GetNull() == null);

            Console.WriteLine("Cache hit for null:");
            Console.WriteLine(GetNull() == null);
        }

        [Cache]
        static IEnumerable<int> GetValues()
        {
            for (int value = 0; value < 3; value++)
            {
                Console.WriteLine("Returning value {0}.", value);
                yield return value;
            }
        }

        [Cache]
        static IEnumerable<int> GetNull()
        {
            return null;
        }
    }
}
```

The output of this sample is:

```
Cache miss:
Caching enumerable.
Returning value 0.
Returning value 1.
Returning value 2.
Value 0 obtained.
Value 1 obtained.
Value 2 obtained.
Cache hit:
Value 0 obtained.
Value 1 obtained.
Value 2 obtained.
Cache miss for null:
True
Cache hit for null:
True
```

