---
uid: sdk
---

# Extending Metalama with the Roslyn API

`Metalama.Framework.Sdk` offers direct, low-level access to Metalama by using [Roslyn-based APIs](https://docs.microsoft.com/dotnet/csharp/roslyn-sdk/compiler-api-model).

Unlike `Metalama.Framework`, our high-level API, aspects built with `Metalama.Framework.Sdk` must be in their own project, separate from
the code they transform. `Metalama.Framework.Sdk` is much more complex to use and less safe than `Metalama.Framework`, and does not allow for a good design-time experience. You should use `Metalama.Framework.Sdk` only when necessary.

This chapter contains the following articles:

* <xref:sdk-scaffolding>
* <xref:aspect-weavers>
* <xref:custom-metrics>
* <xref:services>

