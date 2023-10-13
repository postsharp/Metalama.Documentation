---
uid: caching
title: "Caching"
product: "postsharp"
categories: "Metalama;AOP;Metaprogramming"
---
# Caching

Caching is a great way to improve the latency an application. Traditionally, when you implement caching, you need to play with the API of the caching framework (such as <xref:System.Runtime.Caching.MemoryCache>) or caching server (such as Redis) and to include moderately complex logic to your source code to generate the cache key, check the existence of the item in the cache, and add the item into the cache. Another source of complexity stems from removing items from the cache when the source data is updated. Implementing caching manually is not only time-consuming, but also is error-prone: it is easy to generate inconsistent cache keys between read and update methods. 

## Benefits 

Metalama Caching has the following benefits over manual caching:

* **Reduce boilerplate**.  Metalama Caching allows you to cache the return value of a method as a function of its arguments with just a custom attribute, namely the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> aspect. To invalidate the cache, add the <xref:Metalama.Patterns.Caching.Aspects.InvalidateCacheAttribute?text=[InvalidateCache]> aspect to the update methods. To use a custom class as a parameter of a cached method, use the <xref:Metalama.Patterns.Caching.Aspects.CacheKeyAttribute?text=[CacheKey]> aspect to mark the properties that uniquely identify the object. As a result, your business code is shorter and more readable.

* **Reduce bugs**. Generating cache keys with hand-written code is notoriously bug-prone. Metalama Caching eliminates this source of defects by implementing a reliable approach to key generation, combining object-oriented and aspect-oriented techniques.

* **Reduce coupling**. Cache invalidation can be tricky and often requires you to review your complete _write_ methods every time you add caching to a _read_ method. Cache dependencies act like an abstraction layer sitting between _read_ and _write_ methods and help reduce coupling between them.

* **Flexible topologies**. Metalama Caching supports several caching topologies, and you can switch between them without effort:

    * In-memory caching,
    * Redis-based distributed caching (see <xref:caching-redis>),
    * Redis-based distributed caching with a synchronized in-memory L1 (see <xref:caching-redis>), and
    * In-memory caching with multi-node synchronization over Azure Service Bus or Redis Pub/Sub  (see <xref:caching-pubsub>).



## In this chapter

| Section | Description |
|---------|-------------|
| <xref:caching-getting-started> | This article shows how to make method returned values being cached. |
| <xref:caching-invalidation> | This article shows how to invalidate cached returned values of methods declaratively and imperatively. |
| <xref:caching-dependencies> | This article shows how to invalidate cache items automatically using cache dependencies. |
| <xref:caching-keys> | This article shows how to customize the cache keys which identify cached method return values. |
| <xref:caching-redis> | This article shows how to use Redis as a distributed cache. |
| <xref:caching-pubsub> | This article shows how to invalidate all related in-memory caches in a distributed environment. |
| <xref:caching-value-adapters> | This article describes how to cache return values of methods which cannot be cached directly, such as instances of <xref:System.Collections.Generic.IEnumerable`1> or <xref:System.IO.Stream>.  |
| <xref:cache-locking> | This article explains how you can prevent the same method from being executed with the same arguments at the same time - by using locking. |
| <xref:caching-troubleshooting> | This article describes how to add logging to the caching component. |
