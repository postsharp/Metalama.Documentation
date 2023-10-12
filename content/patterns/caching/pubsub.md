---
uid: caching-pubsub
title: "Synchronizing Local In-Memory Caches for Multiple Servers"
product: "postsharp"
categories: "Metalama;AOP;Metaprogramming"
---
# Synchronizing Local In-Memory Caches for Multiple Servers

Caching in distributed applications can be a tricky problem. When there are several instances of an application running simultaneously (typically web sites or web services deployed into the cloud or web farms), you have to make sure that the cache is properly invalidated for all instances of the application.

A typical answer to this issue is to use a centralized cache server (or a cluster of cache servers) that solves this problem for you. For instance, you can use a Redis server or a Redis cluster. However, running a cache server, even more a cache cluster, comes with a cost, and it does not always pay off for medium applications such as the website of a small business.

An alternative solution to the problem of distributed caching is to have a local in-memory cache in each instance in the application. Instead of using a shared distributed cache, each application instance caches its own data into its own local cache. However, when one instance of the application modifies a piece of data, it needs to make sure that all instances remove the relevant items from their local cache. This is called *distributed cache invalidation*. It can be achieved easily and cheaply with a publish/subscribe (Pub/Sub) message bus such as Azure Service Bus, much less expensive than a cache cluster. 

Metalama allows you to easily add sub/sub cache invalidation to your existing Metalama caching.

The principal inconvenience of pub/sub invalidation is that there is some latency in the invalidation mechanism, i.e. different instances of the application can see different data during a few dozens of milliseconds.


## Using Azure Service Bus pub/sub for distributed invalidation


### To use Azure Service Bus pub/sub for distributed invalidation:

1. Add caching to your application as described in <xref:caching-getting-started>. 


2. Add a reference to the [Metalama.Patterns.Caching.Azure](https://www.nuget.org/packages/Metalama.Patterns.Caching.Azure/) NuGet package. 


3. Go to a Microsoft Azure portal, open the **Service Bus** panel and create a new **Topic**. Choose a small value for the time-to-live setting, for instance 30 seconds. See [Microsoft Azure website](https://azure.microsoft.com/en-us/services/service-bus/) for details. 


4. In the Microsoft Azure portal, create a **Shared access policy** and include the **Manage** right. This policy will be used by your application. 


5. Go to the properties of the newly created policy and copy the primary or secondary connection string to the clipboard.


6. Go back to your source code and find the place where the <xref:Metalama.Patterns.Caching.Backends.MemoryCachingBackend> is initialized. 


The next steps differ depending on whether you're using .NET Framework or not.


### If you're using .NET Framework

1. Create an instance of <xref:Metalama.Patterns.Caching.Backends.Azure.AzureCacheInvalidatorOptions> class and specify the connection string to the shared access policy you just created. 


2. Create an instance of the <xref:Metalama.Patterns.Caching.Backends.Azure.AzureCacheInvalidator> class using the <xref:Metalama.Patterns.Caching.Backends.Azure.AzureCacheInvalidator.CreateAsync*> factory method passing the existing instance of the <xref:Metalama.Patterns.Caching.Backends.MemoryCachingBackend> and <xref:Metalama.Patterns.Caching.Backends.Azure.AzureCacheInvalidatorOptions>. Assign the new <xref:Metalama.Patterns.Caching.Backends.Azure.AzureCacheInvalidator> to the <xref:Metalama.Patterns.Caching.CachingService.DefaultBackend> property. 



### Example

This example shows how to initialize an in-memory caching backend to let it invalidate and be invalidated using Azure Service Bus pub/sub.

```csharp
var localCache = new MemoryCachingBackend();

string connectionString = "Endpoint=sb://yourServiceNamespace.servicebus.windows.net/;EntityPath=yourTopic;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=yourKey";

var azureCacheInvalidatorOptions = new AzureCacheInvalidatorOptions
                                    {
                                        ConnectionString = connectionString
                                    };

CachingServices.DefaultBackend = AzureCacheInvalidator.Create( localCache, azureCacheInvalidatorOptions );
```


### If you're using .NET Standard or .NET Core

1. Create an instance of `AzureCacheInvalidatorOptions2` class using the connection string to the shared access policy you just created. You also need to either specify the Service Bus topic subscription name or additional credentials that would allow the code to create new topic subscriptions. See below. 

    If you choose automatic topic management, you will need to pass a client ID, client secret and tenant ID to each client. These can be the same for all clients. [You can create these credentials in the Microsoft Azure portal in the section Azure Active Directory.](https://docs.microsoft.com/en-gb/azure/active-directory/develop/howto-create-service-principal-portal#get-tenant-id) 


2. Create an instance of the `AzureCacheInvalidator2` class using the `AzureCacheInvalidator2.Create` factory method passing the existing instance of the <xref:Metalama.Patterns.Caching.Backends.MemoryCachingBackend> and `AzureCacheInvalidatorOptions2`. Assign the new `AzureCacheInvalidator2` to the <xref:Metalama.Patterns.Caching.CachingService.DefaultBackend> property. 



### Topic creation strategies

A topic subscription can connect only one client to the Azure server at a time. For example, if you start your application on two different computers but use the same Service Bus topic subscription for both, cache invalidation will not work. It is necessary to have two topic subscriptions and assign one to each application instance.

There are two ways to create topic subscriptions: automatic, and manual.

* **Automatic topic management** means that Metalama will automatically create a topic subscription for each instance of the cache invalidator. This topic subscription will have an auto-generated name and will be set to auto-expire after a few minutes after the connection ends. 
    In .NET Framework, this strategy is the only one that can be used and it does not require extra permissions. In .NET Core and .NET Standard, this strategy requires your application to have management permissions to Azure Service Bus.

* **Manual topic management** means that you have to create the topic subscriptions yourself using the Azure portal or another management API. In this case, you need to pass the topic subscription name to `AzureCacheInvalidatorOptions2`, but your Azure credentials do not need the management permission. 

In .NET Core, you select the strategy by invoking the corresponding constructor of the `AzureCacheInvalidatorOptions2` class. (In .NET Framework, automatic topic management is always used.) 


## Using Redis pub/sub for distributed invalidation

If you are already using Redis for Metalama caching, it is useless to add another layer of invalidation because this is already taken care of by the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCachingBackend> class. However, if you already have a Redis cluster but you don't want to use it for caching, you can still use it for cache invalidation. An example situation is when the latency of your Redis server is too high for caching but sufficient for cache invalidation. 


### To use Redis Pub/Sub for distributed invalidation:

1. Add caching to your application as described in <xref:caching-getting-started>. 


2. Create an instance of [StackExchange.Redis.ConnectionMultiplexer](https://stackexchange.github.io/StackExchange.Redis/Configuration). See [Redis Pub/Sub documentation](https://redis.io/topics/pubsub/) for details. 


3. Create and configure an instance of <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCacheInvalidatorOptions> class. 


4. Create an instance of the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCacheInvalidator> class using the <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCacheInvalidator.Create*> factory method passing your instance of <xref:Metalama.Patterns.Caching.Backends.MemoryCachingBackend> and <xref:Metalama.Patterns.Caching.Backends.Redis.RedisCacheInvalidatorOptions>. Assign the instance to the <xref:Metalama.Patterns.Caching.CachingService.DefaultBackend> property. 



### Example

This example shows how to initialize an in-memory caching backend to let it invalidate and be invalidated using Redis Pub/Sub.

```csharp
var localCache = new MemoryCachingBackend();

string connectionConfiguration = "localhost";
string channelName = "myCahnnel";

ConnectionMultiplexer connection = ConnectionMultiplexer.Connect( connectionConfiguration );

var redisCacheInvalidatorOptions = new RedisCacheInvalidatorOptions
                                    {
                                        ChannelName = channelName
                                    };

CachingServices.DefaultBackend = RedisCacheInvalidator.Create( localCache, connection, redisCacheInvalidatorOptions );
```

