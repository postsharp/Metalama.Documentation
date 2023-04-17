---
uid: sdk
level: 400
---

# Extending Metalama with the Roslyn API

The `Metalama.Framework.Sdk` NuGet package offers direct, low-level access to
Metalama by using [Roslyn-based APIs](https://docs.microsoft.com/dotnet/csharp/roslyn-sdk/compiler-api-model).

Unlike `Metalama.Framework`, our high-level API, aspects built with `Metalama.Framework.Sdk` must be in their own project, separate from the code they transform.

`Metalama.Framework.Sdk` is much more complex to use and less safe than `Metalama.Framework`,
and because of that the new project in which the `Metalama.Framework.Sdk` package is included
must never be entangled with the aspects built with it.

You should use `Metalama.Framework.Sdk` only when creating one-off coding aids.

For all mainstream development needs,
we recommend that you stick to `Metalama.Framework`, `Metalama.Attributes`,
and possibly `Metalama.Aspects`.

This chapter contains the following articles:

* <xref:sdk-scaffolding>
* <xref:aspect-weavers>

