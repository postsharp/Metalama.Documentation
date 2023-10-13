---
uid: caching-invalidation
---
# Invalidating the cache

As Phil Karlton once [famously said](https://www.karlton.org/2017/12/naming-things-hard/), _here are only two hard things in Computer Science: cache invalidation and naming things._

As this quote humoursly captures, cache invalidation seems straigghtforward but is deceptively difficult to get right. 

Metalama, unfortunately, cannot  erase this problem from the map of software engineering annoyances. The reason cache invalidation is and will remain challenging, especially in case of distributed systems where multiple caches and data stores need to be kept in sync. However, Metalama makes it at least superficially simpler to request the removal of the right cache item -- by exposing a rich programmatic and aspect-oriented API, and removing the need to generate concistent caching key between the cached method and the invalidating logic.

Metalama offers two ways to invalidate the cache:

* _Direct_ invalidation is when the method that _updates_ the application state (such as a database entity) directly invalidates the cache for all _read_ methods that depends on this entity. The benefit of direct invalidation is that it does not require a lot of resources on the caching backend. However, this approach has a big disadvantage: it exhibits an imperfect separation of concerns. Update methods need to have a precise knowledge of all cached read methods, therefore update methods need to be modified whenever a read method is added. The current article will only cover direct invalidation.

* _Indirect_ invalidation adds a layer of abstraction, named _cache dependencies_, between the cached method and the invalidating code. Read methods are reponsible for adding the proper dependencies to their context, and update methods are responsible for invalidating the dependencies. Therefore, update methods no longer need to know all read methods. For details about this approach, see <xref:caching-dependencies>. 


## Invalidating cache items declaratively using the [InvalidateCache] aspect

You can add the <xref:Metalama.Patterns.Caching.Aspects.InvalidateCacheAttribute?text=[InvalidateCache]> aspect to a method (called the *invalidating method*) to cause any call to this method to remove from the cache the value of one or more other methods. Parameters of both methods are matched by name and type. 

By default, the <xref:Metalama.Patterns.Caching.Aspects.InvalidateCacheAttribute?text=[InvalidateCache]> aspect looks for the cached method in the current type. You can specify a different type using the alternative constructor of the custom attribute. 

For instance, suppose you have the following read method:

[!metalama-file  ~/code/Metalama.Documentation.SampleCode.Caching/InvalidateAspect/InvalidateAspect.cs marker="Cache"]

Then, the following code would invalidate this method:

[!metalama-file  ~/code/Metalama.Documentation.SampleCode.Caching/InvalidateAspect/InvalidateAspect.cs marker="InvalidateCache"]

### Example: using [InvalidateCache]

The following example demonstrates a service named `PriceCatalogue` with two read methods, `GetProducts` and `GetPrice`, and two write methods, `AddProduct` and `UpdatePrice`. The write methods use the <xref:Metalama.Patterns.Caching.Aspects.InvalidateCacheAttribute?text=[InvalidateCache]> aspect to cause invalidation of their respective read method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/InvalidateAspect/InvalidateAspect.cs]

## Compile-time validation


One of the nicest features of the <xref:Metalama.Patterns.Caching.Aspects.InvalidateCacheAttribute?text=[InvalidateCache]> aspect is that is verifies that the parameters of the invalidated and invalidating methods match:

* When the method cannot be found, a compile-time error is reported.

    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/InvalidateCompileTimeErrors/BadMethod.cs]

* If any parameter of the cached method cannot be matched with a parameter of the invalidating method, a build error will be reported (unless the parameter has the <xref:Metalama.Patterns.Caching.NotCacheKeyAttribute> custom attribute). The order of parameters is not considered. 

    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/InvalidateCompileTimeErrors/BadParameter.cs]

* When you invalidate a non-static method (unless instance has been excluded from the cache key by setting the <xref:Metalama.Patterns.Caching.Aspects.CachingBaseAttribute.IgnoreThisParameter> to `true`), you can do it only from a non-static method of the current type. 

    [!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/InvalidateCompileTimeErrors/NonStatic.cs]

* If there are more invalidated methods of the same name for one invalidating method, a build error is reported. To enable invalidation of all the matching overloads by the one invalidating methods, set the property <xref:Metalama.Patterns.Caching.Aspects.InvalidateCacheAttribute.AllowMultipleOverloads> to `true`. 

## Invalidating cache items imperatively

Instead of annotating invalidating methods with a custom attribute, you can call to one of the overloads of the <xref:Metalama.Patterns.Caching.CachingServiceExtensions.Invalidate*> or <xref:Metalama.Patterns.Caching.CachingServiceExtensions.InvalidateAsync*> extension method of the <xref:Metalama.Patterns.Caching.ICachingService> interface.

To get access to this interface, if you are using dependency injection, you should first make your class `partial`. Then, the service is available as a field named `_cachingService`. If you are not using dependency injection in this class, use  the <xref:Metalama.Patterns.Caching.CachingService.Default?text=CachingService.Default> property.

The first argument of the <xref:Metalama.Patterns.Caching.CachingServiceExtensions.Invalidate*> method should be a delegate to the method to invalidate. This argument must be followed by the list of arguments for which the cache should be invalidated. These arguments will be used construct the key of the item to be removed from cache. All arguments must be supplied. Arguments of parameters that are not part of the cache key will be included.

For instance, suppose you have the following read method:

[!metalama-file  ~/code/Metalama.Documentation.SampleCode.Caching/ImperativeInvalidate/ImperativeInvalidate.cs marker="Cache"]

Then, the following code would invalidate this method:

[!metalama-file  ~/code/Metalama.Documentation.SampleCode.Caching/ImperativeInvalidate/ImperativeInvalidate.cs marker="InvalidateCache"]

### Example: imperative invalidation

The following example is an update of the previous one. The `PriceCatalogue` service has two read methods, `GetProducts` and `GetPrice`, and two write methods, `AddProduct` and `UpdatePrice`. The write methods use the <xref:Metalama.Patterns.Caching.CachingServiceExtensions.Invalidate*> method to cause invalidation of their respective read method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/ImperativeInvalidate/ImperativeInvalidate.cs]

## Forcing the cache to refresh

If you want to call a method while skipping the cache you can, instead of calling  <xref:Metalama.Patterns.Caching.CachingServiceExtensions.Invalidate*?text=_cachingService.Invalidate> and then this method, call just the <xref:Metalama.Patterns.Caching.CachingServiceExtensions.Refresh*?text=_cachingService.Refresh> method. The new result of the method will be stored in the cache.

Contrarily to <xref:Metalama.Patterns.Caching.CachingServiceExtensions.Invalidate*>, <xref:Metalama.Patterns.Caching.CachingServiceExtensions.Refresh*> will cause methods in the calling context to skip the cache.