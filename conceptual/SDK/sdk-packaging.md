---
uid: sdk-packaging
---

# Packaging


Packing a weaver project that was created using the steps above will produce a package that contains all parts of the aspect, including its dependencies (simplifying step 3 above), but with a name ending with `.Weaver`.

To fix this:

1. Specify `<PackageId>` without the `.Weaver` suffix inside a `<PropertyGroup>` in the csproj of the second project.
2. Specify `<PackageId>` with a `.Redist` suffix inside a `<PropertyGroup>` in the csproj of the first project.
3. (Optional) Add `<IsPackable>false</IsPackable>` to a `<PropertyGroup>` in the csproj of the first project, to prevent you from creating a package that would contain just the attribute.

<!--- Illustrating the above with an example would be a good idea -->
## Formatting the output code

Your weaver does not need to format the output code itself. This is done by Metalama at the end of the pipeline, and only when necessary.

However, your weaver is responsible to annotating the syntax nodes with the annotations declared in the  <xref:Metalama.Framework.Engine.Formatting.FormattingAnnotations> class.


## Examples

Available examples of Metalama.Framework.Sdk weavers are:

* [Metalama.Open.Virtuosity](https://github.com/postsharp/Metalama.Open.Virtuosity): makes all possible methods in a project `virtual`
* [Metalama.Open.AutoCancellationToken](https://github.com/postsharp/Metalama.Open.AutoCancellationToken): automatically propagates `CancellationToken` parameter
* [Metalama.Open.DependencyEmbedder](https://github.com/postsharp/Metalama.Open.DependencyEmbedder): bundles .NET Framework applications into a single executable file

The Metalama.Open.Virtuosity repository contains very little logic, so it can be used as a template for your own weavers.
