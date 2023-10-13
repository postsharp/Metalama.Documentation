---
uid: fabrics-configuration
level: 200
---

# Configuring aspects with fabrics

Some aspect libraries may expose a compile-time, imperative configuration API that influences the behavior of aspects. 

For instance, the `Metalama.Patterns.Caching` has a <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingConfigurationExtensions.ConfigureCaching*> method that allows to change different settings such as the expiration of cache items, and `Metalama.Extensions.DependencyInjection` has its <xref:Metalama.Extensions.DependencyInjection.DependencyInjectionExtensions.ConfigureDependencyInjection*> allowing to register new frameworks.

Alternatively, an aspect library may expose its configuration API as a class implementing the <xref:Metalama.Framework.Options.IHierarchicalOptions> interface. 

Accessing these configuration APIs is similar to adding aspects in bulk using fabrics:

1. Create a fabric class and derive it from <xref:Metalama.Framework.Fabrics.ProjectFabric>, <xref:Metalama.Framework.Fabrics.NamespaceFabric>, <xref:Metalama.Framework.Fabrics.TransitiveProjectFabric> or, less often, <xref:Metalama.Framework.Fabrics.TypeFabric>.

2. Override the <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*>, <xref:Metalama.Framework.Fabrics.NamespaceFabric.AmendNamespace*> or <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> method.

3. Call one of the following methods:

   * To select the current project, namespace or type itself, simply use the <xref:Metalama.Framework.Fabrics.IAmender`1.Outbound*?text=amender.Outbound> property.
   * To select child members (methods, fields, types, sub-namespaces, etc.), call the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.Select*>, <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SelectMany*> or <xref:Metalama.Framework.Aspects.IAspectReceiver`1.Where*> method and provide a lambda expression that selects the relevant type members.

4. The next step depends on the kind of configuration API exposed by the aspect library:

    * If the aspect library exposes a `Configure` method such as <xref:Metalama.Patterns.Caching.Aspects.Configuration.CachingConfigurationExtensions.ConfigureCaching*> or <xref:Metalama.Extensions.DependencyInjection.DependencyInjectionExtensions.ConfigureDependencyInjection*>, call this method.

    * If the aspect library exposes a configuration object that implements the <xref:Metalama.Framework.Options.IHierarchicalOptions> interface, cal the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SetOptions*> and pass an instance of the desired configuration.


## Example: configuring caching

The following example shows how to configure caching. It sets the absolute expiration for inside ther `MyProduct.MyNamespace` namespace.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.Caching/AbsoluteExpiration_Fabric.cs]


## About the incremental nature of compile-time configuration


It is important to understand that all compile-time configuration APIs are _incremental_, i.e. any call to a `Configure` or <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SetOptions*> method represent a _change_ in the configuration. Changes are merged on a per-property basis.

Suppose for instance that you configure caching. At the project level, you set an absolute expiration of 30 minutes and a sliding expiration of 10 minutes. However, for a sub-namespace named `ColdNs`, you increase the absolute expiration to 60 minutes and don't modify the sliding expiration. As you can expect, the sliding expiration in `ColdNs` is still 10 minutes. 

It may be surprising at first glance, but the order of configuration operations does _not_ matter when they affect different declarations. That is, if you first configure `ColdNs`  and then the project, you will get the exact same result as if the operations were performed in the inverse order. 

Therefore, the two following snippet are equivalent:

```cs
amender.Outbound
  .ConfigureCaching( caching => { 
        caching.SlidingExpiration = TimeSpan.FromMinutes( 10 );
        caching.AbsoluteExpiration = TimeSpan.FromMinutes( 30 );
    });

amender.Outbound
  .Select( x => x.GlobalNamespace.GetDescendant( "ColdNs" )! )
  .ConfigureCaching( caching => caching.AbsoluteExpiration = TimeSpan.FromMinutes( 60 ) );

```

And in reverse order:

```cs
amender.Outbound
  .Select( x => x.GlobalNamespace.GetDescendant( "ColdNs" )! )
  .ConfigureCaching( caching => caching.AbsoluteExpiration = TimeSpan.FromMinutes( 60 ) );

amender.Outbound
  .ConfigureCaching( caching => { 
        caching.SlidingExpiration = TimeSpan.FromMinutes( 10 );
        caching.AbsoluteExpiration = TimeSpan.FromMinutes( 30 );
    });


```

The order of operations only matters when they are applied to the same declaration. In this case, the last operations wins, always on a per-property basis.

## Configuration with custom attributes

Some libraries can provide configuration custom attributes in addition to an imperative one.

It is important for you to know that both mechanisms -- fabrics and custom attributes -- are equivalent. You can think of configuration custom attributes, when they exist are simpler ways to call the configuration API. The configuration properties they provide as merged with the ones provided with the fabrics.


## Inheritance of configuration options

Unless specified otherwise by the aspect library, all configuration options are _inherited_ from the base type or from the overridden member. Implemented interfaces, however, are not taken into account.

So, when you want to set some options for a whole class family, it is sufficient to set these options for the base class, and it will automatically apply to child classes.

Options inherited from the base class have priority over the options that come from the enclosing type (including that of nested types), the enclosing namespace, or the project.