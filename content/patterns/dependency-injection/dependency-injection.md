---
uid: dependency-injection-aspect
---

# Injecting dependencies

Dependency injection is one of the most common patterns in .NET. Prior to .NET Core, several frameworks were developed by the community, with different features and coding patterns. Since then, `Microsoft.Extensions.DependencyInjection` has become the default framework.

Unlike previous frameworks, `Microsoft.Extensions.DependencyInjection` does not rely on custom attributes on fields and properties, but require you to add the dependencies to the class constructor and to store them as fields. This can cause some boilerplate code, especially if there are many dependencies in a complex class hierarchy. Additionally, it can be cumbersome to migrate your code from an attribute-based framework to a constructor-based framework.

To reduce these inconveniences (arguably only minor ones), you can use the <xref:Metalama.Extensions.DependencyInjection.DependencyAttribute?text=[Dependency]> aspect of the `Metalama.Extensions.DependencyInjection`.

The benefits of this aspects are:

* Reduced boilerplate, 
* Easier migration from attribute-based frameworks to constructor-based frameworks,
* Supports multiple dependency injection frameworks (see <xref:dependency-injection>).

The <xref:Metalama.Extensions.DependencyInjection.DependencyAttribute?text=[Dependency]> aspect exposes two properties:

* <xref:Metalama.Extensions.DependencyInjection.DependencyAttribute.IsLazy> generates code that resolves the dependency lazily, upon first access.
* <xref:Metalama.Extensions.DependencyInjection.DependencyAttribute.IsRequired> determines whether the code can execute if the property is missing


## Example: injecting dependencies

The following example shows the code generation pattern for three kinds of dependency: required, optional, and lazy.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.DependencyInjection/DependencyInjectionAspect.cs]