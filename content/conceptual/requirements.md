---
uid: requirements
---

# Requirements

## Build Environment

* [.NET SDK](https://dotnet.microsoft.com/download) 6.0 needs to be installed.
* Metalama has been tested on Windows (x64), Ubuntu (x64) and MacOS (ARM).

## IDEs

Metalama integrates with Roslyn, so it will integrate with any Roslyn-based IDE.

| IDE | Design-Time Correctness | Code Fixes | Additional UI features |
| --- | ----------------------- | ---------- | ---------------------- |
| Visual Studio 2022 _with_ Metalama Tools | Yes | Yes | Transformed code diff, info bar, syntax highlighting.
| Visual Studio 2022 _without_ Metalama Tools | Yes | Yes | |
| Rider | Yes | Yes | |
| Visual Studio Code (OmniSharp) | Yes | No | |
| Visual Studio for Mac | Yes | Yes

> [!NOTE]
> When using Visual Studio, the use of Metalama Tools for Visual Studio is not required but is highly recommended.

## Target Frameworks

Only SDK-style projects are supported.

Your projects can target any framework that supports .NET Standard 2.0.

This includes:

| Framework | Versions |
|-----------|-----------|
| .NET and .NET Core	| 2.0 or later
| .NET Framework | 4.7.2  or later
| Mono |	5.4 or later
| Xamarin.iOS	 | 10.14 or later
| Xamarin.Mac |	3.8 or later
| Xamarin.Android |	8.0 or later
| Universal Windows Platform	| 10.0.16299 or later

## Synchronizing Metalama and Visual Studio versions

Since Metalama includes a fork of Roslyn, and Roslyn comes with Visual Studio,  you may wonder if you need to synchronize the update of Metalama and your IDEs.

> [!NOTE]
> Even if you are not using Visual Studio, your IDE is still bound to a specific version of Roslyn. Roslyn if a part of the Visual Studio product family. Therefore, the support and versioning policies of your IDE are linked to the ones of Visual Studio.

To keep yourself safe from versioning issues, follow the following suggestions:

* You can update your IDE at any time and without impact on Metalama projects as long as you do not start using new C# features in Metalama projects. That is, you should not get into trouble just by updating Visual Studio.
* Before you start using new C# features in a Metalama project, you must update Metalama to a version that supports the new C# version. If you do not update, your code will not compile.
* Always use a version of Visual Studio that is in active [mainstream support](https://docs.microsoft.com/en-us/lifecycle/policies/fixed#mainstream-support) by Microsoft. When a version of Visual Studio falls out of support, update to a supported version within 3 months.  If you use an unsupported version, you will only be able to use the language features of the last supported C# version _below_ the version that you are using. If you do not use a supported version of Visual Studio, you may be stuck to an unsupported version of Metalama.

To learn more about the support policies of Visual Studio, see [Visual Studio Product Lifecycle and Servicing](https://docs.microsoft.com/en-us/visualstudio/productinfo/vs-servicing), and [Visual Studio Channels and Release Rhythm](https://docs.microsoft.com/en-us/visualstudio/productinfo/release-rhythm).

The reasons behind these suggestions are:

* By policy, we are adding support for new Roslyn versions not later than 3 weeks after their stable release, and remove support for obsolete versions no sooner than 3 months after they fall out of mainstream support by Microsoft.
* The `Metalama.Framework` package always replaces the compiler, so your code will build against the version of Roslyn that Metalama was built for regardless of the installed version of your IDE or the .NET SDK.
* Metalama includes design-time support for several versions of Visual Studio. Many Metalama implementation assemblies have to be compiled for each targeted version of Roslyn. To keep our packages small, we need to remove support for unsupported Roslyn versions.


The following table shows the correlation between Visual Studio and Metalama versions and their support status:

| Visual Studio | Roslyn | C# Version | Release Date | End of Support | Min Metalama Version | Max Metalama Version |
|--|--|--|--|--|--|--|
| 2022 version 17.0 | 4.0.1 | 10.0 | November 8, 2021 | October 11, 2023
| 2022 version 17.1 | 4.1.0 | 10.0 | February 15, 2022 | August 9, 2022
| 2022 version 17.2 | 4.2.0 | 11.0 | May 10, 2022 | April 9, 2024 | 0.5.23


