---
uid: sdk
level: 400
summary: "The document provides guidance on using the `Metalama.Framework.Sdk` NuGet package, recommending it for creating one-off coding aids and advising against its use for conventional development."
---

# Extending Metalama with the Roslyn API

The `Metalama.Framework.Sdk` NuGet package provides direct, low-level access to Metalama by utilizing [Roslyn-based APIs](https://docs.microsoft.com/dotnet/csharp/roslyn-sdk/compiler-api-model).

The usage of `Metalama.Framework.Sdk` is more complex and less secure than `Metalama.Framework`.

We recommend using `Metalama.Framework.Sdk` exclusively when creating one-off coding aids.

For all conventional development requirements, we suggest sticking to `Metalama.Framework`.

This chapter comprises the following articles:

* <xref:aspect-weavers>
* <xref:roslyn-api>
* <xref:custom-metrics>


