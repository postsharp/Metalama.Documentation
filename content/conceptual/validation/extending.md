---
uid: validation-extending
---

# Creating your own validation rules



## Why Metalama.Extensions.Architecture

In this chapter, we will show how to use the [Metalama.Extensions.Architecture](https://www.nuget.org/packages/Metalama.Extensions.Architecture) package, a free and [open-source](https://github.com/postsharp/Metalama.Extensions) extension to the Metalama framework. `Metalama.Extensions.Architecture` is built on the top of Metalama Framework and has been specifically designed to validate architecture. It contains ready-made architecture rules and is highly extensible.

Instead of using `Metalama.Extensions.Architecture`, you can use directly Metalama Framework and add validators to your aspects or fabrics. For details, see <xref:aspect-validating>. The advantage of this approach is that you can spare a dependency on `Metalama.Extensions.Architecture`, and it may seem simpler at first sight to create your own aspects than to extend the high-level package. Nevertheless, it is a good idea to use `Metalama.Extensions.Architecture` if your goal is validate architecture.