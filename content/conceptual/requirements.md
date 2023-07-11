---
uid: requirements
---

# Requirements

## Build environment

* The [.NET SDK](https://dotnet.microsoft.com/download) 6.0 must be installed.
* Metalama has been tested on Windows (x64), Ubuntu (x64), and MacOS (ARM).

## IDEs

Metalama integrates with Roslyn, so it is theoretically compatible with any Roslyn-based IDE.

| IDE | Design-Time Correctness | Code Fixes | Additional UI Features |
| --- | ----------------------- | ---------- | ---------------------- |
| Visual Studio 2022 _with_ Metalama Tools | Yes | Yes | Transformed code diff, info bar, syntax highlighting |
| Visual Studio 2022 _without_ Metalama Tools | Yes | Yes | |
| Rider | Yes | Yes | |
| Visual Studio Code (OmniSharp) | Yes | No | |
| Visual Studio for Mac | Yes | Yes |

> [!NOTE]
> While using Visual Studio, the utilization of Metalama Tools for Visual Studio is not mandatory but is highly recommended.

## Target frameworks

Only SDK-style projects are supported.

Your projects can target any framework that supports .NET Standard 2.0, including:

| Framework | Versions |
|-----------|-----------|
| .NET and .NET Core	| 2.0 or later |
| .NET Framework | 4.7.2  or later |
| Mono |	5.4 or later |
| Xamarin.iOS	 | 10.14 or later |
| Xamarin.Mac |	3.8 or later |
| Xamarin.Android |	8.0 or later |
| Universal Windows Platform	| 10.0.16299 or later |

## Synchronizing Metalama and Visual Studio versions

Since Metalama includes a fork of Roslyn, which comes with Visual Studio, you might question whether you need to synchronize the updates of Metalama and your IDEs.

> [!NOTE]
> Even if you're not using Visual Studio, your IDE is still bound to a specific version of Roslyn. Roslyn is a part of the Visual Studio product family. Therefore, the support and versioning policies of your IDE are linked to those of Visual Studio.

To avoid versioning issues, consider the following suggestions:

* You can update your IDE at any time without impacting Metalama projects, provided you do not start using new C# features in Metalama projects. In other words, merely updating Visual Studio should not cause any issues.
* Before you begin using new C# features in a Metalama project, ensure you update Metalama to a version that supports the new C# version. If you do not update, your code will not compile.
* Always use a version of Visual Studio that is under active [mainstream support](https://docs.microsoft.com/en-us/lifecycle/policies/fixed#mainstream-support) by Microsoft. When a version of Visual Studio falls out of support, update to a supported version within three months. If you use an unsupported version, you will only be able to use the language features of the last supported C# version _below_ the version that you are using. If you do not use a supported version of Visual Studio, you may be stuck with an unsupported version of Metalama.

For more information on the support policies of Visual Studio, see [Visual Studio Product Lifecycle and Servicing](https://docs.microsoft.com/en-us/visualstudio/productinfo/vs-servicing) and [Visual Studio Channels and Release Rhythm](https://docs.microsoft.com/en-us/visualstudio/productinfo/release-rhythm).

The rationale behind these suggestions is as follows:

* As per our policy, we add support for new Roslyn versions no later than three weeks after their stable release and remove support for obsolete versions no sooner than three months after they fall out of mainstream support by Microsoft.
* The `Metalama.Framework` package always replaces the compiler. Your code will build against the version of Roslyn that Metalama was built for, regardless of the installed version of your IDE or the .NET SDK.
* Metalama includes design-time support for several versions of Visual Studio. Many Metalama implementation assemblies must be compiled for each targeted version of Roslyn. To keep our packages small, we need to remove support for unsupported Roslyn versions.


