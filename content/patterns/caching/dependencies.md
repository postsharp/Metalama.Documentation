---
uid: caching-dependencies
---
# Working with cache dependencies

Cache dependencies have two major use cases. First, dependencies can act as a middle layer between the cached methods (typically the read methods) and the invalidating methods (typically the update methods) and therefore reduce the coupling between the read and update methods. Second, cache dependencies can be used to represent external dependencies, such as file system dependencies or SQL dependencies.

Compared to direct invalidation, using dependencies exhibits lower performance and higher resource consumption in the caching backend because of the need to store and synchronize the graph of dependencies. For details about direct invalidation, see <xref:caching-invalidation>. 


## Adding string dependencies

Eventually, all dependencies are represented as strings. Although we recommend using one of the strongly-typed approaches described below, it is good to understand how string dependencies work.

To add or invalidate dependencies, as usual, you will get acess to the <xref:Metalama.Patterns.Caching.ICachingService> interface. If you are using dependency injection, you should first make your class `partial`, and the interface is available under a field named `_cachingService`. Otherwise, use the <xref:Metalama.Patterns.Caching.CachingService.Default?CachingService.Default> property.

Within a _read_ methods, use the <xref:Metalama.Patterns.Caching.CachingServiceExtensions.AddDependency*?text=ICachingService.AddDependency*> at any time to add a dependency to the method being executed, for the arguments with which it is executed. You can pass an arbritrary `string` to this method, potentially including the method arguments.

For instance, here is how to add a `string` dependency:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/StringDependencies/StringDependencies.cs marker="AddDependency"]

Then, in the _update_ methods, use <xref:Metalama.Patterns.Caching.CachingServiceExtensions.AddDependency*?text=ICachingService.Invalidate*> method and pass the dependency `string` to remove any cache item that has a dependency on this `string`.

For instance, the following line invalidates two `string` dependencies:

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/StringDependencies/StringDependencies.cs marker="Invalidate"]


> [!NOTE]
> Dependencies properly work with recursive method calls. If a cached method `A` calls another cached method `B`, all dependencies of `B` are automatically dependencies of `A`, even if `A` was cached when `A` was being evaluated. 


### Example: string dependencies

The following code is a variation of our `ProductCatalogue` example. It has three read methods: 

* `GetPrice`  returns the price of a given product,
* `GetProducts` returns the list of products without their price, and
* `GetPriceList` returns both the name and the price of all products.

It has two write methods:

* `AddProduct` adds a product, therefore should affect both `GetProducts` and  `GetPriceList`, and
* `UpdatePrice` changes the price of a given product, and should affect `GetPrice` for this product and `GetPriceList`.

We model the dependencies using three string templates:

* `ProductList` is the product list without price,
* `ProductPrice:{productId}` is the price of a given product, and
* `PriceList` is the complete price list.


[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/StringDependencies/StringDependencies.cs]


## Adding object-oriented dependencies through the ICacheDependency interface

As mentioned above, working with string dependencies can be error-prone because the code generating the string is duplicated in the both _read_ and the _write_ method. A better approach is to encapsulate the cache key generation logic, i.e. to represent the cache dependency as an object, and add some key-generation logic to this object.

For this reason, Metalama Caching allows you to work with strongly-typed, object-oriented dependencies thanks to the <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency> interface. 

This interface has two members:

* <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency.GetCacheKey*> should return the `string` representation of the caching key,
* <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency.CascadeDepependencies>, an optional property, can return a list of dependencies that should be recursively invalidated when the current dependency is invalidated.

How and where you implement <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency> is entirely up to you. You have the following options:

1. The most practical option is often to implement the <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency> in your domain objects.
2. Alternatively, you can create a parallel object model implementing <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency> -- just to represent dependencies.
3. If you have types that can already be used in cache keys, e.g. thanks to the <xref:Metalama.Patterns.Caching.Aspects.CacheKeyAttribute?text=[CacheKey]> aspect or another mechanism (see <xref:caching-keys>), you can turn these objects into dependencies by wrapping them into an <xref:Metalama.Patterns.Caching.Dependencies.ObjectDependency>. You can also use the <xref:Metalama.Patterns.Caching.CachingServiceExtensions.AddObjectDependency*> and <xref:Metalama.Patterns.Caching.CachingServiceExtensions.InvalidateObject*> methods to avoid creating a wrapper.
4. To represent singleton dependencies, it can be convenient to assign them a constant string and wrap this string into a <xref:Metalama.Patterns.Caching.Dependencies.StringDependency> object.


### Example: object-oriented dependencies

Let's revamp our previous example with object-oriented dependencies.

Instead of just working with primitive types like `string` and `decimal`, we create a new type `record Product( string Name, decimal Price)` and make this type implement the  <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency> interface.

To represent dependencies of the global collections `ProductList` and `PriceList`, instead of creating new classes for each, we use instances of the <xref:Metalama.Patterns.Caching.Dependencies.StringDependency> class and expose them as static properties of the `GlobalDependencies` static class.

To make sure we invalidate the whole `PriceList` whenever we update some `Product`, we return the global `PriceList` dependency instance from the <xref:Metalama.Patterns.Caching.Dependencies.ICacheDependency.CascadeDepependencies> property of the `Product` class.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/ObjectDependencies/ObjectDependencies.cs]

## Suspending the collection of cache dependencies

A new caching context is created for each cached method. The caching context is propagated along all invoked methods. It is implemented using <xref:System.Threading.AsyncLocal`1>. 

When a parent cached method calls a child cached method, the dependencies of the child methods are automatically added to the parent method, even if the child method was actually not executed because its result was found in cache. Therefore, invalidating a child method automatically invalidates the parent method, which is most of the times an intuitive and desirable behavior.

There are cases where propagating the caching context from the parent to the child methods (and therefore the collection of child dependencies into the parent context) is not desirable. For instance, if the parent method runs an asynchronous child task using `Task.Run` and does not wait for its completion, then it is likely that the dependencies of methods called in the child task should not be propagated to the parent (the child task could be considered a side effect of the parent method, and should not affect caching). Undesired dependencies would not break the program correctness, but it would make it less efficient. 

To suspend the collection of dependencies in the current context and in all children contexts, you can use the <xref:Metalama.Patterns.Caching.CachingServiceExtensions.SuspendDependencyPropagation*?text=_cachingService.SuspendDependencyPropagation> method with a `using` construct. 

