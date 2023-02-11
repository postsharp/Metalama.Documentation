---
uid: sdk
---

# Extending Metalama with the Roslyn API

`Metalama.Framework.Sdk` offers direct, low-level access to Metalama by using [Roslyn-based APIs](https://docs.microsoft.com/dotnet/csharp/roslyn-sdk/compiler-api-model).

Unlike `Metalama.Framework`, our high-level API, aspects built with `Metalama.Framework.Sdk` must be in their own project, separate from
the code they transform. `Metalama.Framework.Sdk` is much more complex to use and less safe than `Metalama.Framework`, and does not allow for a good design-time experience. You should use `Metalama.Framework.Sdk` only when necessary.

This chapter contains the following articles:

* [Scaffolding a SDK project](chapter-extending-sdk-scaffolding.md)
* [Weaving with aspect weavers](chapter-extending-sdk-aspect-weavers.md)
* [Logic](chapter-extending-sdk-logic.md)
* [Creating custom metrics](chapter-extending-sdk-custom-metrics.md)
* [Creating custom services](chapter-extending-sdk-services.md)

