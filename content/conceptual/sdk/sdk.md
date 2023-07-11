---
uid: sdk
level: 400
---

# Extending Metalama with the Roslyn API

The `Metalama.Framework.Sdk` NuGet package provides direct, low-level access to Metalama by utilizing [Roslyn-based APIs](https://docs.microsoft.com/dotnet/csharp/roslyn-sdk/compiler-api-model).

Contrary to `Metalama.Framework`, our high-level API, aspects constructed with `Metalama.Framework.Sdk` need to be in their own distinct project, separate from the code they are intended to transform.

The usage of `Metalama.Framework.Sdk` is considerably more complex and less secure than `Metalama.Framework`. As such, the new project where the `Metalama.Framework.Sdk` package is integrated should never be intertwined with the aspects developed with it.

We recommend using `Metalama.Framework.Sdk` exclusively when creating one-off coding aids.

For all conventional development requirements, we suggest sticking to `Metalama.Framework`, `Metalama.Attributes`, and potentially `Metalama.Aspects`.

This chapter comprises the following articles:

* <xref:sdk-scaffolding>
* <xref:aspect-weavers>


