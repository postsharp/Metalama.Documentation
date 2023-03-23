---
uid: naming-conventions
level: 200
---

# Enforcing naming conventions

In any professional team, it is essential that everyone uses the same terminology. This is especially true in large codebases maintained by multiple people over a long period. 

To ensure consistency, Metalama provides a simple way to enforce naming conventions across lines of inheritance. For example, you can easily require that any class inheriting the `IFactory` interface has a name that ends with the `Factory` prefix. This helps to ensure that everyone is using the same language and understanding the same concepts.

## Enforcing naming conventions using custom attributes

If you want to enforce a naming convention for any type that derives from a given class or interface, when you own the source code of this class or interface, the easiest approach is to use a custom attribute. Follow these steps.

1. Add the `Metalama.Extensions.Architecture` package to your project.

2. Add the <xref:Metalama.Extensions.Architecture.Aspects.DerivedTypesMustRespectNamingConventionAttribute> custom attribute to the base class or interface. The argument of this custom attribute must be the naming pattern, where the asterisk (`*`) will match any substring.

> [!NOTE]
> If you want full control over the regular expression, use  <xref:Metalama.Extensions.Architecture.Aspects.DerivedTypesMustRespectRegexNamingConventionAttribute>.

### Example

In the following example, we require all types implementing `IFactory` to have a name that ends with the `Factory` suffix.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/NamingConvention.cs tabs="target"]


## Enforcing naming conventions using fabrics

When you want to enforce naming conventions for a different scenario than the one above, you cannot use custom attributes. Instead, you need to use fabrics and write compile-time code. Follow these steps:

1. Add the `Metalama.Extensions.Architecture` package to your project.

2. Create or reuse a fabric type as described in <xref:fabrics>.

3. Import the <xref:Metalama.Extensions.Architecture.Fabrics> namespace to benefit from extension methods.

4. Edit the  <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*>,  <xref:Metalama.Framework.Fabrics.NamespaceFabric.AmendNamespace*> or  <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> of this method. Open the dance by calling [amender.Verify()](xref:Metalama.Extensions.Architecture.Fabrics.AmenderExtensions.Verify*).

5. Select the APIs using the <xref:Metalama.Extensions.Architecture.Fabrics.VerifierExtensions.Select*>, <xref:Metalama.Extensions.Architecture.Fabrics.VerifierExtensions.SelectMany*> and <xref:Metalama.Extensions.Architecture.Fabrics.VerifierExtensions.Where*> methods. You may also find the <xref:Metalama.Extensions.Architecture.Fabrics.ITypeSetVerifier`1.SelectTypesDerivedFrom*> method useful.

6. Call the <xref:Metalama.Extensions.Architecture.Fabrics.VerifierExtensions.MustRespectNamingConvention*> method.


> [!NOTE]
> Unlike <xref:Metalama.Extensions.Architecture.Aspects.DerivedTypesMustRespectNamingConventionAttribute>, the naming convention added by the <xref:Metalama.Extensions.Architecture.Fabrics.VerifierExtensions.MustRespectNamingConvention*> is _neither_ inherited _nor_ cross-project. If you want the naming convention to apply to several projects, use the techniques described in <xref:fabrics-many-projects>.

### Example: Enforcing a naming convention on all types derived from a given system type

Many teams require UI pages to be suffixed `Page`, controls `Control`, and so on. This cannot be achieved using a custom attribute because you don't own the source code of the base class. In the following example, we show how to implement this requirement: we require all classes derived from `TextReader` to be suffixed `Reader`. We use the <xref:Metalama.Extensions.Architecture.Fabrics.ITypeSetVerifier`1.SelectTypesDerivedFrom*> method to select the relevant types.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/NamingConvention_Fabric.cs tabs="target"]