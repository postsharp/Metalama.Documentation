---
uid: caching-getting-started
---

# Getting started with Metalama Caching

Suppose you have a time-consuming method that always returns the same return value when called with the same arguments. The easiest thing you can do to increase your application's performance is to cache this method. With Metalama Caching, this can be as simple as adding the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> attribute from the [Metalama.Patterns.Caching.Aspects](https://www.nuget.org/packages/Metalama.Patterns.Caching.Aspects/) package.


Before you can use the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> aspect, your projects need a bit of set up. The approach depends on the architecture of your project: with or without dependency injection.

> [!WARNING]
> By default, the cache key of a parameter is built using `ToString` method. However, the default implementation of the `ToString` method does not return a unique string for custom classes and structs. The default implementation of `ToString` for records is more likely to be correct. After you are done with the first steps of this _getting started_ guide, it is essential to provide a cache key implementation for all parameter types of a cached method. For details, see <xref:caching-keys>.


## With dependency injection

If your project is designed for the .NET Core dependency injection framework (`Microsoft.Extensions.DependencyInjection`), follow these steps.

1. Install the [Metalama.Patterns.Caching.Aspects](https://www.nuget.org/packages/Metalama.Patterns.Caching.Aspects/) package into your project.
2. In your application setup logic, while adding services to the <xref:Microsoft.Extensions.DependencyInjection.IServiceCollection>, add a call to the <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddCaching*> extension method. It will add an instance of the <xref:Metalama.Patterns.Caching.ICachingService> interface, which is consumed by the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> aspect.

    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/GettingStarted/GettingStarted.Program.cs mark="Registration"]    

The <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> aspect is now available to all objects instantiated by the dependency injection container.

> [!NOTE]
> If your project uses a different dependency injection framework, you may need to create an adapter for this framework, then create an instance of the <xref:Metalama.Patterns.Caching.ICachingService> interface using <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create> method.  For details about DI adapters, see <xref:dependency-injection>.

### Example: setting up caching _with_ dependency injection

In this example, we show how to add logging to a self-hosted .NET Core application. This application is composed of two services, the main service called `MainService` and a fantasy `CloudCalculator` which is supposed to perform complex and slow computations. 

`Program.Main` calls the <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddCaching*> extension method. This makes <xref:Metalama.Patterns.Caching.ICachingService> available to `CloudCalculator`, which can use the  <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> aspect. Observe how the <xref:Metalama.Patterns.Caching.ICachingService> interface is automatically pulled into the `CloudCalculator` class.

Finally, `MainService` calls `CloudCalculator` as usual. It calls the `CloudCalculator.Add` three times with the same parameters, and displays, at the end, the actual number of operations performed.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/GettingStarted/GettingStarted.cs ]

## Without dependency injection

If your project does not use dependency injection, the global default instance of the <xref:Metalama.Patterns.Caching.ICachingService>  will be used. It is exposed as the <xref:Metalama.Patterns.Caching.CachingService.Default?text=CachingService.Default> property.

Follow these steps to configure your project:

1. Install the [Metalama.Patterns.Caching.Aspects](https://www.nuget.org/packages/Metalama.Patterns.Caching.Aspects/) package into your project.
2. Create a file named for instance `CachingConfiguration.cs` and add the following code:

    [!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/GettingStarted_NoDI/GettingStarted_NoDI.CachingConfiguration.cs ]

    This code disables dependency injection for the whole project. You can also add this attribute to individual types to disable dependency injection specifically for these types. You can also configure your project using fabrics and use the <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingConfigurationExtensions.ConfigureCaching*?text=amender.Outgoing.ConfigureCaching> method and modify the <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingOptionsBuilder.UseDependencyInjection> property for selected namespaces or types.

3. Initialize the <xref:Metalama.Patterns.Caching.CachingService.Default?text=CachingService.Default> property from your initialization code (typically `Program.Main`). Call the <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create> method to get a new instance of the service.
 
    > [!IMPORTANT]
    > The caching service has to be initialized before any cached method is called for the first time.


### Example: setting up caching _without_ dependency injection

When dependency injection is disabled, we can also cache `static` methods. Observe the  <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> in static `CloudCalculator` implementaiton.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/GettingStarted/GettingStarted.cs ]

## What's next

So far so good, but it looks too good to be true.
If your cached methods to have more complex parameters than intrinsics like `int` or `string` (and a dozen of other well-known types), Metalama Caching will use the `ToString` method to represent the parameter in the caching key. This may not always be appropriate. In the next article, we will see how to customize the caching key.


