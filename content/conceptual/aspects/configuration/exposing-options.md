---
uid: exposing-options
level: 300
---


# Exposing a configuration API

<xref:Metalama.Framework.Options> is the go-to namespace whenever you want to build a configuration API for your aspects. It might be a bit difficult to understand at first sight, but it allows you to create a delightful configuration experience for your users with relative ease.

The <xref:Metalama.Framework.Options> namespace supports the following features:

* Options are automatically inherited from the base type, base member, enclosing type, enclosing namespace, or project. In other words, you can easily configure options for the whole project, or a namespace, a specific type, or even a given method.
* Cross-project inheritance of options is fully supported and transparent.
* You can build a developer-friendly programmatic API to configure your options, and the users of your aspects can use it from any fabric.
* You can also build a configuration custom attribute.


## Creating an option class

When you design a configuration API, the first step is to create the class that will represent and store the options. The option class must implement the <xref:Metalama.Framework.Options.IHierarchicalOptions`1>. for as many declarations types as desired. Suppose for instance that you are building a caching aspect. Naturally, you will implement the `IHierarchicalOption<IMethod>` interface because the aspect is applied to methods. However, to allow bulk configuration of methods, you will also implement the `IHierarchicalOption<INamedType>` so that users can configure whole types,  `IHierarchicalOption<INamespace>` for namespaces, and `IHierarchicalOption<ICompilation>` for the whole project.

It is important to understand that option classes represent _changes_.  option classes themselves must be designed as _immutable_. That is, all properties must have an `init` accessor instead of a `set` accessor. Furthermore, most properties including value-typed properties must be _nullable_. In general, a `null` value represents the absence of a modification in this property. This is why we say that option classes represent _incremental_ objects, and indirectly implement the  <xref:Metalama.Framework.Options.IIncrementalObject> interface.

Therefore the most important member of option classes is the <xref:Metalama.Framework.Options.IIncrementalObject.ApplyChanges*> method, which should merge two immutable incremental instances into one new immutable instance representing the combination of changes. This is the method that gets called by Metalama in order to merge the configuration settings provided at different levels by the user.

The <xref:Metalama.Framework.Options.IHierarchicalOptions`1> interface defines a second method, <xref:Metalama.Framework.Options.IHierarchicalOptions.GetDefaultOptions*>, which we should ignore at the moment and should just return `null`. This method is useful when getting default options from MSBuild properties. See <xref:reading-msbuild-properties> for details.

In summary, to create an option class, follow these steps:

1. Create a class (records are currently unsupported) that implements as many generic instances of the <xref:Metalama.Framework.Options.IHierarchicalOptions`1> interface as needed, where `T` is any type of declaration on which you want to allow the user to define options.
2. Do not add any constructor to this class. The class must keep its default constructor.
3. Make sure that all properties are `init`-only and are of a nullable type, even value-typed ones (exceptions apply for complex types like collections, see below).
4. Skip the implementation of `IHierarchicalOptions.GetDefaultOptions(IProject)` for now. Just return `this`.
5. Implement the <xref:Metalama.Framework.Options.IIncrementalObject.ApplyChanges*> method so that it returns a new instance of the option class combining the changes of the current instance and the supplied parameter, where the properties of the parameter win over the ones of the current instance.

### Example: option class

The following class demonstrates a typical implementation of <xref:Metalama.Framework.Options.IHierarchicalOptions`1>. The object has no explicit constructor and has only `init`-only properties. The  <xref:Metalama.Framework.Options.IIncrementalObject.ApplyChanges*> method merges the changes and gives priority to the properties of the supplied parameter, if defined.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration.Options.cs]


## Reading the options 

Reading options that apply to a different context requires some care. There are two APIs. Both are exposed under the expression.

To read the options applying to any declaration, call the <xref:Metalama.Framework.Code.DeclarationExtensions.Enhancements*?text=declaration.Enhancements()> method and then <xref:Metalama.Framework.Code.DeclarationEnhancements`1.GetOptions*> method.

> [!WARNING]
> Options provided by _aspects_ through the <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider> interface (see below) are applied shortly _before_ the aspect's <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method is executed. They will be not available to the <xref:Metalama.Framework.Code.DeclarationEnhancements`1.GetOptions*?text=d.Enhancements().GetOptions()> before that moment.


### Example: reading options

The following example demonstrates a template that uses <xref:Metalama.Framework.Code.DeclarationEnhancements`1.GetOptions*?text=d.Enhancements().GetOptions()> to read the options applying to the current aspect.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration.Aspect.cs]


## Configuring the options from a fabric

If you choose to make the option class public, the users of your aspects can now set the options using the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SetOptions*?text=amender.Outgoing.SetOptions> method in any fabric. Users can also use methods like <xref:Metalama.Framework.Aspects.IAspectReceiver`1.Select*>, <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SelectMany*> or <xref:Metalama.Framework.Aspects.IAspectReceiver`1.Where*>.

> [!NOTE]
> This technique not only works from fabrics, but also from any aspect that is applied _before_ the aspect that will consume the option.

### Example: applying options from a fabric

The following example puts all previous code snippets together. It adds a project fabric that configures the logging aspect for the whole project and then specify different options for a child namespace. You can check in the that the logging code generated for the target code refers to different categories, as configured by the fabrics.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration.cs]


### Exposing options directly on your aspect

Quite often, you will want to offer users of your aspect the possibility to specify options directly when instantiating the aspect, typically as properties as the aspect custom attribute.

To achieve this, your aspect must implement the <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider> interfac. This interface has a single method <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider.GetOptions*> that returns a list of options objects, typically an instance of your option class wrapped into an array. 

Note that custom attributes cannot have properties of nullable value types. Therefore, you cannot just duplicate the properties of the option class into your aspect. Instead, you have to create field-backed properties where every property is backed by a field of nullable field. With this design, the implementation of <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider.GetOptions*> becomes a mapping of the backing fields of the aspects to the properties of the option class.


### Example: aspect providing options

In the following example, the aspect code has been updated to support configuration properties. Note how the `Level` property, of a value type, is backed by nullable value type, so we can distinguish between the default value and the unspecified value. The aspect shows a trivial implementation of the <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider.GetOptions*> aspect.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration_Provider.Aspect.cs]

## Creating a configuration custom attribute

Additionally to the programmatic API represented by the option class, you may want to provide your users with a configuration custom attributes. This would allow users to configure your aspect without resorting to a fabric.

To create a configuration custom attribute, follow these steps:

1. Create a class derived from <xref:System.Attribute?text=System.Attribute>.
2. Add the <xref:System.Attribute?text=[AttributeUsage]> attribute as required. Typically, you will want to allow users to apply this attribute to the assembly, class or struct that contains the member to which the aspect is applied.
3. Implement the <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider> interface where `T` is the option class. 
4. Create the relevant properties with non-nullable value types and back them by nullable fields, as indicated in the section above.
5. Implement the <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider.GetOptions*> method by creating an instance of your option classes where the properties of this class are mapped to the backing fields of the attribute.

#### Example: configuration custom attribute

In the following example, we added a `LogConfigurationAttribute` class that allows to configure the logging aspect using a custom attribute additionally to a fabric.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration_Provider.cs]


## Working with collections

Up to now, we only worked with flat option classes, which only had properties of a simple type like strings, numbers or enums. Things can become more complex when the configuration needs to have collection properties. Remember that each instance of an option class represents a _change_ or _increment_. If you need to work with collection properties, these properties must represent a change in the collection and not the collection itself. 

Since this is not a trivial endeavour, Metalama offers two collection classes that implement the <xref:Metalama.Framework.Options.IIncrementalObject> semantic: 

* <xref:Metalama.Framework.Options.IncrementalHashSet`1> represents an unordered set of values. You can create operations on this collection using the <xref:Metalama.Framework.Options.IncrementalHashSet> factory class.
* <xref:Metalama.Framework.Options.IncrementalKeyedCollection`2> represents collections of items that can be identified by a key. The factory class is <xref:Metalama.Framework.Options.IncrementalKeyedCollection>. Items must implement the <xref:Metalama.Framework.Options.IIncrementalKeyedCollectionItem`1> interface.


## Offering a beautiful configuration API

The problem with classes like <xref:Metalama.Framework.Options.IncrementalKeyedCollection`2> is that they are quite syntax-heavy for the users of your aspect API.

If you want to provide your users with a more convenient APIs, you can make your option classes `internal`, and supply a simple public configuration API exposed as a set of extension methods to the <xref:Metalama.Framework.Aspects.IAspectReceiver`1> interface. These extension methods will like have to use the _builder_ pattern.

1. Create an class, named say `MyOptions`, implementing <xref:Metalama.Framework.Options.IHierarchicalOptions`1> as described above and following. Follow the _immutable_ pattern. Make the class `internal`.
2. Create a public, mutable _builder_ class, named say `MyOptionsBuilder` whith an `internal` method named for instance `Build` returning the immutable `MyOptions`.
3. Create one or many extension methods to the <xref:Metalama.Framework.Aspects.IAspectReceiver`1> interface, named say `ConfigureMyOptions`, with the following signature:

    ```csharp
    public static void ConfigureMyOptions( this IAspectReceiver<INamedType> aspectReceiver, Action<MyOptionsBuilder> action )
    {
        var builder = new MyOptionsBuilder();
        action( builder );
        var options = builder.Build();
        aspectReceiver.SetOptions( options );
    }
    ```

This is approach we took for the configuration of the <xref:Metalama.Extensions.DependencyInjection> namespace.

