---
uid: internal-only-implement
level: 200
---

# Restricting who can implement an interface

When designing an interface, it is sometimes preferable to prevent others from implementing it. Indeed, once someone implements the interface, adding new members is no longer possible without breaking any class that implements it.

Metalama provides a way to protect your interface from being implemented by other assemblies. To do this, follow these steps.

1. Add the `Metalama.Extensions.Architecture` package to your project.

2. Annotate the interface with the <xref:Metalama.Extensions.Architecture.Aspects.InternalOnlyImplementAttribute>. This attribute will prevent any other assemblies from implementing your interface.

## Example

In the parent project, assume we have the following interface protected by the <xref:Metalama.Extensions.Architecture.Aspects.InternalOnlyImplementAttribute> attribute:

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/InternalOnlyImplement.Dependency.cs]

If we try to implement this interface in a child project, a warning is reported:

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/InternalOnlyImplement.cs tabs="target"]


