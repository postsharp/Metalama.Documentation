---
uid: naming-conventions
---

# Enforcing naming conventions

In any professional team, it is essential that everyone uses the same terminology. This is especially true in large codebases maintained by multiple people over a long period. 

To ensure consistency, Metalama provides a simple way to enforce naming conventions across lines of inheritance. For example, you can easily require that any class inheriting the `IFactory` interface has a name that ends with the `Factory` prefix. This helps to ensure that everyone is using the same language and understanding the same concepts.

## Enforcing naming conventions for derived types

If you want to enforce a naming convention for any type that derives from a given class or interface, follow these steps:

1. Add the `Metalama.Extensions.Architecture` package to your project.

2. Add the <xref:Metalama.Extensions.Architecture.Aspects.DerivedTypesMustRespectNamingConventionAttribute> custom attribute to the base class or interface. The argument of this custom attribute must be the naming pattern, where the asterisk (`*`) will match any substring.

> [!NOTE]
> If you want full control over the regular expression, use  <xref:Metalama.Extensions.Architecture.Aspects.DerivedTypesMustRespectRegexNamingConventionAttribute>.

### Example

In the following example, we require all types implementing `IFactory` to have a name that ends with the `Factory` suffix.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/NamingConvention.cs tabs="target"]
