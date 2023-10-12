---
uid: caching-exclude-parameters
---

# Excluding parameters from the cache key

Some methods may have parameters that do not need to a part of the cache. A typical example is the <xref:System.Threading.CancellationToken> type, which is automatically skipped. Another example may be a request correlation ID. Often, the current instance (`this`) represents a service instance and should also be skipped. 

This article presents mechanisms to exclude any parameter from the cache key.

## Ignoring the `this` parameter

To exclude the `this` parameter, use one of the following approaches:

* Set the <xref:Metalama.Patterns.Caching.Aspects.CachingBaseAttribute.IgnoreThisParameter> parameter of the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> aspect to `true`. 
* Add a  <xref:Metalama.Patterns.Caching.Aspects.CachingConfigurationAttribute?text=[CacheConfiguration]> attribute to the type or assembly and set its <xref:Metalama.Patterns.Caching.Aspects.CachingBaseAttribute.IgnoreThisParameter> to `true`.
* Using a fabric, call the <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingConfigurationExtensions.ConfigureCaching*> method and set the <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingOptionsBuilder.IgnoreThisParameter> to `true`.

For details regarding configuration, see TODO.

### Example: ignoring the `this` parameter

In the following example, the class `PricingService` exposes two instance methods. Both methods are cached. The `PricingService` class has a unique `id` field, and its `ToString` implementation includes this field because it is useful for troubleshooting. However, we want several instances of the `PricingService` to use reuse the cached results. Therefore, we exclude the `this` instance from the cache key. Since this decision must apply to all cached methods of this type, we apply the  <xref:Metalama.Patterns.Caching.Aspects.CachingConfigurationAttribute?text=[CacheConfiguration]> to the type.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/ExcludeThisParameter.cs]

## Excluding parameters using [NotCacheKey]

When you want to exclude another parameter than the current instance (`this`), simply add the <xref:Metalama.Patterns.Caching.NotCacheKeyAttribute> custom attribute on this parameter. 

### Example: using [NotCacheKey]

In the following example, both methods of the `PricingService` class have a `correlationId` field. This field used for troubleshooting; it has a unique value for each web API request and therefore must be excluded from the cache. 

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/NotCacheKey.cs]


## Excluding parameters by rule using filters


The inconvenience of using the <xref:Metalama.Patterns.Caching.NotCacheKeyAttribute> custom attribute is that it must added to every single parameter. It can be cumbersome and subject to human errors when many parameters must be excluded according to the same rules.

In this case, it is preferable to implement and register a programmatic parameter classifier.  Follow these steps:

1. Create a class that implements the <xref:Metalama.Patterns.Caching.Aspects.Configuration.ICacheParameterClassifier> interface. It has a single method, <xref:Metalama.Patterns.Caching.Aspects.Configuration.ICacheParameterClassifier.GetClassification*>, which receives a parameter and returns a value indicating whether the parameter should be excluded from the cache key.
2. Using a fabric for the desired scope (typically the current project, a namespace, or all referencing projects), call the <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingConfigurationExtensions.ConfigureCaching*?amender.Outgoing.ConfigureCaching> method and supply a delegate that calls the <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingOptionsBuilder.AddParameterClassifier*> method.

> [!WARNING]
> It may be tempting to classify parameters based on naming conventions, for instance to exclude all parameters named `correlationId`, but this is dangerous because naming conventions are easily broken. Instead, it is preferable to use a fabric to report a warning when a method is cached and one of its parameter matches a naming pattern but is not annotated with the  <xref:Metalama.Patterns.Caching.NotCacheKeyAttribute> attribute.

### Example: parameter filter

The following example demonstrates a parameter classifier than prevents any parameter of type `ILogger` from being included in a cache key. The classifier itself is implemented by the `LoggerParameterClassifier` class. It is registered using a project fabric.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/ParameterFilter/ParameterFilter.cs]


