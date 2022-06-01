---
uid: sdk
---

# Extending Metalama with the Roslyn API

_Metalama.Framework.Sdk_ offers direct access to Metalama's underlying code-modifying capabilities through [Roslyn-based APIs](https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/compiler-api-model). 

Unlike _Metalama.Framework_, our high-level API, aspects built with _Metalama.Framework.Sdk_ must be in their own project, separate from
the code they transform. _Metalama.Framework.Sdk_ is much more complex and less safe than _Metalama.Framework_, and does not allow for a good design-time experience. You should use _Metalama.Framework.Sdk_ only when necessary.

This chapter contains the following articles:

* <xref:sdk-scaffolding>
* <xref:aspect-weavers>
* <xref:custom-metrics>
* <xref:services>

