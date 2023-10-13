---
uid: caching-pubsub
---
# Synchronizing local in-memory caches for multiple servers

Caching in distributed applications can be a tricky problem. When there are several instances of an application running simultaneously (typically web sites or web services deployed into the cloud or web farms), you have to make sure that the cache is properly invalidated for all instances of the application.

A typical answer to this issue is to use a centralized cache server (or a cluster of cache servers) that solves this problem for you. For instance, you can use a Redis server or a Redis cluster. However, running a cache server, even more a cache cluster, comes with a cost, and it does not always pay off for medium applications such as the website of a small business.

An alternative solution to the problem of distributed caching is to have a local in-memory cache in each instance in the application. Instead of using a shared distributed cache, each application instance caches its own data into its own local cache. However, when one instance of the application modifies a piece of data, it needs to make sure that all instances remove the relevant items from their local cache. This is called *distributed cache invalidation*. It can be achieved easily and cheaply with a publish/subscribe (Pub/Sub) message bus much less expensive than a cache cluster. 

Metalama allows you to easily add sub/sub cache invalidation to your existing Metalama caching using either Azure Service Bus or Redis Pub/Sub.

> [!WARNING]
> With pub/sub invalidation, there is some latency in the invalidation mechanism, i.e. different instances of the application, running on different servers, can see different data during a few dozens of milliseconds. It is generally harmless when the application clients are affinitized to one server (for instance with geo-based request routing), but it can have annoying impact when the same client can randomly connect to different servers.


## Using Azure Service Bus


### Configuring a topic

The first step is to create a topic. To achieve this using Microsoft Azure portal, follow these steps.

1. Go to a Microsoft Azure portal, open the **Service Bus** panel and create a new **Topic**. Choose a small value for the time-to-live setting, for instance 30 seconds. See [Microsoft Azure website](https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-quickstart-topics-subscriptions-portal) for details. 

2. In the Microsoft Azure portal, create a **Shared access policy** and include the **Send**, **Listen** and **Manage** right. This policy will be used by your application. 

3. Copy the primary or secondary connection string to the clipboard.


### Configuring your application

1. Add caching to your application as described in <xref:caching-getting-started>. 

2. Add a reference to the [Metalama.Patterns.Caching.Backends.Azure](https://www.nuget.org/packages/Metalama.Patterns.Caching.Backends.Azure/) NuGet package. 

3. Go back to the code that initialized the Metalama Caching by calling <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddCaching*?text=serviceCollection.AddCaching>  or <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create>. Call the <xref:Metalama.Patterns.Caching.Building.ICachingServiceBuilder.WithBackend*> method, then and supply a delegate that calls the <xref:Metalama.Patterns.Caching.Building.CachingBackendFactory.Memory*> method. Then, call  <xref:Metalama.Patterns.Caching.Backends.Azure.AzureCachingFactory.WithAzureSynchronization*> and pass the topic connection string as an argument.

    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/AzureSynchronized/AzureSynchronized.Program.cs marker="AddCaching"]

4. We recommend initializing the caching service during the initialization sequence of your application, otherwise the service will be initialized lazily upon first use. Get the <xref:Metalama.Patterns.Caching.ICachingService>   interface from the <xref:System.IServiceProvider> and call the <xref"Metalama.Patterns.Caching.ICachingService.InitializeAsync> method.

    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/AzureSynchronized/AzureSynchronized.Program.cs marker="Initialize"]


> [!WARNING]
> Make sure that the <xref:Metalama.Patterns.Caching.ICachingService> is properly disposed of before the application exits, otherwise some background cache write operations may be left unprocessed, and the cache will be inconsistent.


### Example: A distributed application synchronized by Azure Service Bus

The following example simulates a multi-instance application. Here, for the ease of testing, both instances live in the same process. Both instances read and write to a shared database simulated by a concurrent dictionary, which sits behinds an in-memory cache. These two cache instances are synchronized using <xref:Metalama.Patterns.Caching.Backends.Azure.AzureCachingFactory.WithAzureSynchronization*>. 

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/AzureSynchronized/AzureSynchronized.cs]


## Using Redis Pub/Sub

If you are already using Redis as a storage for Metalama Caching, it is useless to add another layer of invalidation because this is already taken care of by the Redis caching back-end. However, if you already have a Redis cluster but you don't want to use it for caching, you can still use it for cache invalidation. An example situation is when the latency of your Redis server is too high for caching but sufficient for cache invalidation. 

No configuration on your Redis server is necessary to use it for cache synchronization.


1. Add caching to your application as described in <xref:caching-getting-started>. 
 
2. Add a reference to the [Metalama.Patterns.Caching.Backends.Redis](https://www.nuget.org/packages/Metalama.Patterns.Caching.Backends.Redis/) NuGet package. 

3. Go back to the code that initialized the Metalama Caching by calling <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddCaching*?text=serviceCollection.AddCaching>  or <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create>. Call the <xref:Metalama.Patterns.Caching.Building.ICachingServiceBuilder.WithBackend*> method, then and supply a delegate that calls the <xref:Metalama.Patterns.Caching.Building.CachingBackendFactory.Memory*> method. Then, call  <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingFactory.WithRedisSynchronization*> and pass an instance of <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCacheSynchronizerConfiguration>.

4. We recommend initializing the caching service during the initialization sequence of your application, otherwise the service will be initialized lazily upon first use. Get the <xref:Metalama.Patterns.Caching.ICachingService>  interface from the <xref:System.IServiceProvider> and call the <xref"Metalama.Patterns.Caching.ICachingService.InitializeAsync> method.


> [!WARNING]
> Make sure that the <xref:Metalama.Patterns.Caching.ICachingService> is properly disposed of before the application exits, otherwise some background cache write operations may be left unprocessed, and the cache will be inconsistent.
