---
uid: release-notes-2024-0
---

# Metalama 2024.0

The principal objective of this release was to support C# 12. Additionally, it fills a few leftovers of the previous releases when we released the source code of Metalama.

## Plaftorm update


* `Metalama.Compiler`: merge from Roslyn 4.8 RTM.

* Support for C# 12:

   * Default lambda parameters,
   * Inline arrays in run-time code,
   * `nameof`: access to instance members from a static context,
   * Primary constructors,
   * Type aliases,
   * Collection expressions (aka collection literals) like `[1,2,..array]`,
   * `AppendParameter` advice (used in dependency injection scenarios) in primary constructors,
   * Primary constructor parameters in initializer expressions.

## Multi C# version support

Metalama 2024.0 is the first version that supports several versions of C#.

* Metalama will use different C# code generation patterns depending on the C# version of the current project. Supported versions are 10, 11, and 12.
* Metalama detects the version used by each T# template and will report an error if a template is used in a project that targets a lower version of C# than required. The new MSBuild property `MetalamaTemplateLanguageVersion` restricts the version of C# that can be used in templates. Define this property if you don't want to use a higher version of C# than desired by mistake.
* There is currently no way for a template to conditionally generate code patterns according to the C# version of the target project.


## Other Improvements

* **Deterministic build** is now implemented for all Metalama assemblies. This allows users to verify that the binaries we released are actually built from our source code. The only difference between official assemblies and your own builds should normally be strong name keys and AuthentiCode signatures. Note that building Metalama from source code requires a source subscription, sold for an additional fee.
* **Symbol packages** are now published for all Metalama NuGet packages, allowing for source code debugging thanks to SourceLink.
* **Warnings and errors deduplication**. (Not supported in the user API at the moment.)
* **Licensing**: aspect inheritance is now allowed for all license types.


## Breaking changes

In the <xref:Metalama.Framework.Code.RefKind> enum, `In` and `RefReadOnly` are no longer synonyms.

## In Progress

We have been working on the following projects, but they are not stable yet:

* [Metalama.Patterns.Observability](https://github.com/postsharp/Metalama.Patterns/tree/release/2024.0/src/Metalama.Patterns.Observability) is an aspect implementating of the `INotifyPropertyChanged` interface. It supports computer properties and child objects.
* [Metalama.Patterns.Xaml](https://github.com/postsharp/Metalama.Patterns/tree/release/2024.0/src/Metalama.Patterns.Xaml) are aspects implementing XAML commands and dependency properties.