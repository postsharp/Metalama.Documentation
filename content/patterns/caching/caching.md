---
uid: caching
title: "Caching"
product: "postsharp"
categories: "Metalama;AOP;Metaprogramming"
---
# Caching

Caching is a great way to improve the latency an application. Traditionally, when you implement caching, you need to play with the API of the caching framework (such as <xref:System.Runtime.Caching.MemoryCache>) or caching server (such as Redis) and to include moderately complex logic to your source code to generate the cache key, check the existence of the item in the cache, and add the item into the cache. Another source of complexity stems from removing items from the cache when the source data is updated. Implementing caching manually is not only time-consuming, but also is error-prone: it is easy to generate inconsistent cache keys between read and update methods. 

Metalama allows you to dramatically reduce the complexity of caching. It allows you to cache the return value of a method as a function of its arguments with just a custom attribute, namely the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute> aspect. The <xref:Metalama.Patterns.Caching.Aspects.InvalidateCacheAttribute> aspect and the <xref:Metalama.Patterns.Caching.CachingService> API offer a strongly-typed way to invalidate cached methods. Additionally, Metalama is independent from the caching framework or server (called caching *backend*), so you can choose from several backends or implement an adapter for your own backend. 


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
