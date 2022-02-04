---
uid: sdk
---

# Metalama.Framework.Sdk

## Introduction

_Metalama.Framework.Sdk_ offers direct access to Metalama's underlying code-modifying capabilities through [Roslyn-based APIs](https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/compiler-api-model). 

Unlike _Metalama.Framework_, our high-level API, aspects built with _Metalama.Framework.Sdk_ must be in their own project, separate from
the code they transform. _Metalama.Framework.Sdk_ is much more complex and unsafe than _Metalama.Framework_, and does not allow for a good design-time experience. You should use _Metalama.Framework.Sdk_ only when necessary.

## Implementing an aspect

### Step 1. Define the public interface of your aspect (a custom attribute)

1. Create an "interface" project (it must target .NET Standard 2.0), which will contain your custom attributes.
2. Add a reference to the _Metalama.Framework_ package (but not  _Metalama.Framework.Sdk_).
3. Define an aspect custom attribute as usually, e.g.

    ```cs
    public class AutoCancellationAttribute : TypeAspect { }
    ```

### Step 2. Create the weaver for this project

1. Create a project that targets .NET Standard 2.0 and name it with the `.Weaver` suffix (by convention).
2. Add a reference the _Metalama.Framework.Sdk_ package.
3. Add a reference to the _first_ project project. In the `<ProjectReference>` in your csproj file, additionally specify `PrivateAssets="all"`.
4. Add a class that implements the <xref:Metalama.Framework.Engine.AspectWeavers.IAspectWeaver> interface. 
5. Add the <xref:Metalama.Compiler.MetalamaPlugInAttribute> and <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverAttribute>  custom attributes to this class.


At this point, the code will look like this:

```cs
using Metalama.Compiler;
using Metalama.Framework.Engine.AspectWeavers;

[MetalamaPlugIn]
[AspectWeaver( typeof(AutoCancellationTokenAttribute) )]
internal partial class AutoCancellationTokenWeaver : IAspectWeaver
{
    public void Transform( AspectWeaverContext context )
    {
        throw new NotImplementedException();
    }
}
```

### Step 3. Implement the Transform method

<xref:Metalama.Framework.Engine.AspectWeavers.IAspectWeaver.Transform%2A> has a parameter of type <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext>. The <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.Compilation> property of this object contains the input compilation, and your implementation must set this property to the new compilation.

The type of the <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.Compilation> property is <xref:Metalama.Framework.Engine.CodeModel.IPartialCompilation>. Compilations are immutable objects. This interface, as well as the extension class <xref:Metalama.Framework.Engine.CodeModel.PartialCompilationExtensions>, offer different methods to transform the compilation. For instance, the <xref:Metalama.Framework.Engine.CodeModel.PartialCompilationExtensions.RewriteSyntaxTrees%2A> will apply a rewriter to the input compilation and return the resulting compilation.

Do not forget to write back the <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.Compilation?context.Compilation> property.

Each weaver will be invoked a single time per project, regardless the number of aspect instances in the project.

The list of aspect instances that need to be handled by your weaver is given by the <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.Compilation?context.AspectInstances> property.

To map the Metalama code model to an `ISymbol`, use the extension methods in <xref:Metalama.Framework.Engine.CodeModel.SymbolExtensions>.


### Step 4. Use your aspect

In a _third_ project:

1. Reference the _first_ project (the one defining the custom attribute). Add `OutputItemType="Analyzer" ReferenceOutputAssembly="false"` to its `<ProjectReference>` in the csproj file.
2. Reference the _second_ project (the one defining the weaver). Add `OutputItemType="Analyzer"` to its `<ProjectReference>`.
3. Reference the _Metalama.Framework_ package.
4. Use the aspect by applying the attribute or by using a fabric, as usually:

    ```c#
    [AutoCancellationToken]
    class MyClass 
    {

    }
    ```

## Step 5. Packaging your aspect with its weaver

Packing a weaver project that was created using the steps above will produce a package that contains all parts of the aspect, including its dependencies (simplifying step 3 above), but with a name ending with `.Weaver`.

To fix this:

1. Specify `<PackageId>` without the `.Weaver` suffix inside a `<PropertyGroup>` in the csproj of the second project.
2. Specify `<PackageId>` with a `.Redist` suffix inside a `<PropertyGroup>` in the csproj of the first project.
3. (Optional) Add `<IsPackable>false</IsPackable>` to a `<PropertyGroup>` in the csproj of the first project, to prevent you from creating a package that would contain just the attribute.

## Formatting the output code

Your weaver does not need to format the output code itself. This is done by Metalama at the end of the pipeline, and only when necessary.

However, your weaver is responsible to annotate the syntax nodes with the annotations declared in the  <xref:Metalama.Framework.Engine.Formatting.FormattingAnnotations> class.


## Examples

Available examples of Metalama.Framework.Sdk weavers are:

* [Metalama.Open.Virtuosity](https://github.com/postsharp/Metalama.Open.Virtuosity): makes all possible methods in a project `virtual`
* [Metalama.Open.AutoCancellationToken](https://github.com/postsharp/Metalama.Open.AutoCancellationToken): automatically propagates `CancellationToken` parameter
* [Metalama.Open.DependencyEmbedder](https://github.com/postsharp/Metalama.Open.DependencyEmbedder): bundles .NET Framework applications into a single executable file

The Metalama.Open.Virtuosity repository contains very little logic, so it can be used as a template for your own weavers.
