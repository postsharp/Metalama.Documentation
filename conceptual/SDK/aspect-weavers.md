---
uid: aspect-weavers
---

# Aspect Weavers

Normal aspects are implemented by the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method, which provide advice thanks to the advice factory exposed on the <xref:Metalama.Framework.Aspects.IAspectBuilder> interface. Therefore, normal aspects are limited to the abilities of the <xref:Metalama.Framework.Advising.IAdviceFactory> interface.

By contrast, aspect weavers allow you to perform _completely arbitrary_ transformations on C# code using the low-level Roslyn API.

When you assign an aspect weaver to an aspect class, Metalama no longer calls the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method to implement the aspect, but instead calls the aspect weaver.

Unlike normal aspects, weaver-based aspects:

* are more complex to implement by one or two orders of magnitude;
* are not executed at design time;
* require their own implementation project;
* may have a larger impact on compilation performance when there are many of them.

## Creating a weaver-based aspect

The following steps guide you to the process of creating a weaver-based aspect and its weaver:

### Step 1. Create the solution scaffolding

This step is described in <xref:sdk-scaffolding>.

### Step 2. Define the public interface of your aspect (typically a custom attribute)

In the _public API project_ created in the previous step, define an aspect class as usual, e.g.

```cs
public class AutoCancellationAttribute : TypeAspect { }
```

### Step 3. Create the weaver for this aspect

In the _weaver project_ created in Step 1:

1. Add a class that implements the <xref:Metalama.Framework.Engine.AspectWeavers.IAspectWeaver> interface.
2. Make sure the class is `public`.
3. Add the <xref:Metalama.Compiler.MetalamaPlugInAttribute> custom attributes to this class.


At this point, the code will look like this:

```cs
using Metalama.Compiler;
using Metalama.Framework.Engine.AspectWeavers;

namespace Metalama.Open.AutoCancellationToken;

[MetalamaPlugIn]
internal partial class AutoCancellationTokenWeaver : IAspectWeaver
{
    public Task TransformAsync( AspectWeaverContext context )
    {
        throw new NotImplementedException();
    }
}
```

### Step 4. Bind the aspect class to its weaver class

Go back to the aspect class and annotate it with a custom attribute of type <xref:Metalama.Framework.Aspects.RequireAspectWeaverAttribute>. The constructor argument must be the namespace-qualified name of the weaver class.


```cs
[RequireAspectWeaver("Metalama.Open.AutoCancellationToken.AutoCancellationTokenWeaver")]
public class AutoCancellationAttribute : TypeAspect { }
```

### Step 5. Implement the TransformAsync method

<xref:Metalama.Framework.Engine.AspectWeavers.IAspectWeaver.TransformAsync*> has a parameter of type <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext>. The <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.Compilation> property of this object contains the input compilation, and your implementation must set this property to the new compilation.

The type of the <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.Compilation> property is <xref:Metalama.Framework.Engine.CodeModel.IPartialCompilation>. Compilations are immutable objects. This interface, as well as the extension class <xref:Metalama.Framework.Engine.CodeModel.PartialCompilationExtensions>, offer different methods to transform the compilation. For instance, the <xref:Metalama.Framework.Engine.CodeModel.PartialCompilationExtensions.RewriteSyntaxTreesAsync*> method will apply a rewriter to the input compilation and return the resulting compilation.

Do not forget to write back the <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.Compilation?text=context.Compilation> property.

Each weaver will be invoked a single time per project, regardless of the number of aspect instances in the project.

The list of aspect instances that need to be handled by your weaver is given by the <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.AspectInstances?text=context.AspectInstances> property.

To map the Metalama code model to an `ISymbol`, use the extension methods in <xref:Metalama.Framework.Engine.CodeModel.SymbolExtensions>.


Your weaver does not need to format the output code itself. This is done by Metalama at the end of the pipeline, and only when necessary.

However, your weaver is responsible for annotating the syntax nodes with the annotations declared in the  <xref:Metalama.Framework.Engine.Formatting.FormattingAnnotations> class.


### Step 6. Write unit tests

If you have created a test project as described in <xref:sdk-scaffolding>, you can test your weaver-based aspect as any other aspect.

## Examples

Available examples of Metalama.Framework.Sdk weavers are:

* [Metalama.Open.Virtuosity](https://github.com/postsharp/Metalama.Open.Virtuosity): makes all possible methods in a project `virtual`.
* [Metalama.Open.AutoCancellationToken](https://github.com/postsharp/Metalama.Open.AutoCancellationToken): automatically propagates `CancellationToken` parameter.
* [Metalama.Open.DependencyEmbedder](https://github.com/postsharp/Metalama.Open.DependencyEmbedder): bundles .NET Framework applications into a single executable file.


The Metalama.Open.Virtuosity repository contains very little logic, so it can be used as a template for your own weavers.

