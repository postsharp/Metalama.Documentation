---
uid: experimental
---

# Marking experimental APIs

The [[Obsolete]](xref:System.ObsoleteAttribute) attribute is a familiar custom attribute that generates a warning when the marked declaration is used unless the referencing declaration is also marked as `[Obsolete]`.

Sometimes, it may be necessary to report a warning for an experimental API that may be changed or removed later. The `[Obsolete]` attribute may not be the best choice for this, as the error message it generates could mislead users. As an alternative, Metalama provides the <xref:Metalama.Extensions.Architecture.Aspects.ExperimentalAttribute> attribute, which is better suited for this purpose.

To generate warnings when an experimental API is being used, follow these steps:

1. Add the `Metalama.Extensions.Architecture` package to your project.

2. Annotate the API with the <xref:Metalama.Extensions.Architecture.Aspects.ExperimentalAttribute>. 

## Example

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Experimental.cs tabs="target"]