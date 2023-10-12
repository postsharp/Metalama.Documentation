---
uid: caching-configuration
---

# Configuring the caching service

The following elements of the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute> aspect behavior can be configured: 

* expiration (absolute and sliding),
* priority,
* auto-reload, and
* enabled/disabled.

There are three ways to set these options:

* at compile time using the properties of the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> or <xref:Metalama.Patterns.Caching.Aspects.CachingConfigurationAttribute?text=[CachingConfiguration]> attributes.
* at compile time using the  <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingConfigurationExtensions.ConfigureCaching*> fabric method.
* at run time using the <xref:Metalama.Patterns.Caching.CachingProfile> class.

In this article, we describe these three approaches.

## Configuring caching with custom attributes

You can configure the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute> aspect by setting the properties of the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute> custom attribute. The inconvenience of this approach is that you have to repeat the configuration for each cached method. 

To configure several methods in a single line of code, you can add the <xref:Metalama.Patterns.Caching.Aspects.CachingConfigurationAttribute> custom attribute to the declaring type, the base type of the declaring type, the enclosing type of the declaring type, if it is nested, or the declaring assembly. 

The configuration of Metalama Caching is based on the configuration framework of Metalama Framework. For details, see <xref:fabrics-configuration>.


### Example: configuring expiration with custom attributes

In the following example, the absolute expiration of cache items is set to 60 minutes for methods of the `PricingService` class, but to 20 minutes for the `GetProducts` method. 


[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/AbsoluteExpiration_Attribute.cs]


## Configuring caching with fabrics

Instead of using custom attributes, you can use a fabric to configure caching aspect at any level of granularity, thanks to the <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingConfigurationExtensions.ConfigureCaching*> fabric extension method.

### Example: configuring expiration with a fabric

The following example sets the absolute expiration to 20 minutes for the namespace `MyProduct.MyNamespace`.


[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/AbsoluteExpiration_Fabric.cs]


## Configuring caching at run time with profiles

All compile-time approaches described above have a the same drawback: by definition, they cannot be modified at run time. This can be a problem if you want the caching options to be loaded from a configuration file that you can deploy with your application.

Metalama Caching makes it possible to change caching options at run time thanks to a concept called _caching profile_, which are sets of options that can be modified at run time. Caching profiles are represented by the <xref:Metalama.Patterns.Caching.CachingProfile> class. 

To modify run-time caching options with a caching profile:

1. Set the <xref:Metalama.Patterns.Caching.Aspects.CachingBaseAttribute.ProfileName> property of the <xref:Metalama.Patterns.Caching.Aspects.CacheAttribute?text=[Cache]> aspect, the <xref:Metalama.Patterns.Caching.Aspects.CachingConfigurationAttribute?text=[CachingConfiguration]> attribute, or the <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingOptionsBuilder> parameter of the  <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingConfigurationExtensions.ConfigureCaching*> fabric extension method.

    > [!NOTE]
    > A default profile is assigned to any cached method even if the <xref:Metalama.Patterns.Caching.Aspects.CachingBaseAttribute.ProfileName> property is not assigned. This profile can be customized at run time as any other profile.

2. Go back to the code that initialized the Metalama Caching by calling <xref:Metalama.Patterns.Caching.Building.CachingServiceFactory.AddCaching*?text=serviceCollection.AddCaching>  or <xref:Metalama.Patterns.Caching.CachingService.Create*?text=CachingService.Create>, and supply a delegate that calls <xref:Metalama.Patterns.Caching.Building.ICachingServiceBuilder.AddProfile*>.

    
> [!WARNING]
> Any configuration property specified thanks to a compile-time mechanism have priority over the configuration of the <xref:Metalama.Patterns.Caching.CachingProfile>. 


### Example: configuring expiration with a fabric

In this example, the `ProductCatalogue` class uses two caching profiles: the default one, and the _hot_ one, whose content should be refreshed more frequently. However, we don't want to hard-code these expiration values in source code. The following code in `Program.cs` configures the logging profiles. Here the values are hard-coded, but we could easily read them from a ~~JSON~~ ~~XML~~ ~~INI~~ configuration file.
 
[!metalama-test ~/code/Metalama.Documentation.SampleCode.Caching/Profiles/Profiles.cs]
    