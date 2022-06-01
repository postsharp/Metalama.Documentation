---
uid: aspect-weavers
---

# Aspect Weavers




## Implementing an aspect

### Step 1. Define the public interface of your aspect (a custom attribute)

1. Create an "interface" project (it must target .NET Standard 2.0), which will contain your custom attributes.
2. Add a reference to the _Metalama.Framework_ package (but not  _Metalama.Framework.Sdk_).
3. Define an aspect custom attribute as usual, e.g.

    ```cs
    public class AutoCancellationAttribute : TypeAspect { }
    ```

### Step 2. Create the weaver for this project

1. Create a project that targets .NET Standard 2.0 and name it with the `.Weaver` suffix (by convention).
2. Add a reference the _Metalama.Framework.Sdk_ package.
3. Add a reference to the _first_ project project. In the `<ProjectReference>` in your csproj file, additionally specify `PrivateAssets="all"`.
4. Add a class that implements the <xref:Metalama.Framework.Engine.AspectWeavers.IAspectWeaver> interface. 
5. Add the <xref:Metalama.Compiler.MetalamaPlugInAttribute> and <xref:Metalama.Framework.Aspects.RequireAspectWeaverAttribute>  custom attributes to this class.


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

<xref:Metalama.Framework.Engine.AspectWeavers.IAspectWeaver.Transform*> has a parameter of type <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext>. The <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.Compilation> property of this object contains the input compilation, and your implementation must set this property to the new compilation.

The type of the <xref:Metalama.Framework.Engine.AspectWeavers.AspectWeaverContext.Compilation> property is <xref:Metalama.Framework.Engine.CodeModel.IPartialCompilation>. Compilations are immutable objects. This interface, as well as the extension class <xref:Metalama.Framework.Engine.CodeModel.PartialCompilationExtensions>, offer different methods to transform the compilation. For instance, the <xref:Metalama.Framework.Engine.CodeModel.PartialCompilationExtensions.RewriteSyntaxTrees*> will apply a rewriter to the input compilation and return the resulting compilation.

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

