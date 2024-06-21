---
uid: introducing-types
---

# Introducing types

Many patterns require you to create new types. This is the case for instance of the Memento, Enum View-Model, or Builder patterns. You can do it by calling the <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceClass*> advice method from your `BuildAspect` implementation. 

> [!NOTE]
> The current version of Metalama allows you to introduce classes. Support for other kinds of types will be added in a future release.


## Introducing a nested class

To introduce a nested class, call the <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceClass*> from an `IAdviser<INamedType>`. For instance, if you have a <xref:Metalama.Framework.Aspects.TypeAspect>, just call `aspectBuilder.IntroduceClass`.

### Example: nested class

In the following example, the aspect introduces a nested class named `Factory`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceNestedClass.cs name="Introducing a nested class"]

## Introducing a top-level class

To introduce a non-nested class, you must first get a hold of an `IAdviser<INamespace>`. Here are a few strategies to get a namespace adviser from any <xref:Metalama.Framework.Advising.IAdviser`1> or <xref:Metalama.Framework.Aspects.IAspectBuilder`1>:

* If you have an `IAdviser<ICompilation>` or `IAspectBuilder<ICompilation>` and want to add a type to `My.Namespace`, call the `WithNamespace( "My.Namespace" )` extension method.
* If you don't have an `IAdviser<ICompilation>`, call `aspectBuilder.With( aspectBuilder.Target.Compilation )`, then call `WithNamespace`.
* To get an adviser for the _current_ namespace, call `aspectBuilder.With( aspectBuilder.Target.GetNamespace() )`.
* To get an adviser for a _child_ of the current namespace, call `aspectBuilder.With( aspectBuilder.Target.GetNamespace() ).WithChildNamespace( "ChildNs" )`.

Once you have an `IAdviser<INamespace>`, call the <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceClass*>  advice method.

### Example: top-level class

In the following example, the introduces the a class in the `Builders` child namespace of the namespace of the target class.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceTopLevelClass.cs name="Introducing a top-level class"]

## Adding class modifiers, attributes, base class, and type parameters

By default, the <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceClass*> method introduces a non-generic class with no modifiers or custom attributes, derived from `object`. To add modifiers, custom attributes, a base type, or type parameters, you must supply a delegate of type `Action<INamedTypeBuilder>` to the `buildType` parameter of the <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceClass*> method. This delegate receives an <xref:Metalama.Framework.Code.DeclarationBuilders.INamedTypeBuilder>, which exposes the required APIs.

### Example: setting up the type

In the following aspect, we continue the nested type example, add the `sealed` modifier the introduced type, and set its base type to `BaseFactory`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceNestedClass.cs name="Introducing a nested class"]

## Adding class members

Once you introduced the type, the next step is to introduce members: constructors, methods, fields, properties, ... 

Introduced types work exactly like source-defined ones.

When you call <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceClass*>, it returns an <xref:Metalama.Framework.Advising.IClassIntroductionAdviceResult>. This interface derives from `IAdviser<INamedType>`, which has familiar extension methods like <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceMethod*>, <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceField*>, <xref:Metalama.Framework.Advising.AdviserExtensions.IntroduceProperty*> and so on.

> [!INFO]
> All programmatic techniques described in <xref:introducing-members>, plus of course those of <xref:introducing-constructors>, also work with introduced types through the `IAdviser<INamedType>` interface.

### Example: adding properties

The following aspect copies properties of the source object to the introduced `Builder` type.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceNestedClass.cs name="Introducing a nested class"]