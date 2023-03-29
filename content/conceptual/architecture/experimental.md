---
uid: experimental
level: 200
---

# Marking experimental APIs

The [[Obsolete]](xref:System.ObsoleteAttribute) attribute is a familiar custom attribute that generates a warning when the marked declaration is used unless the referencing declaration is also marked as `[Obsolete]`.

Sometimes, it may be necessary to report a warning for an experimental API that may be changed or removed later. The `[Obsolete]` attribute may not be the best choice for this, as the error message it generates could mislead users. As an alternative, Metalama provides the <xref:Metalama.Extensions.Architecture.Aspects.ExperimentalAttribute> attribute and the <xref:Metalama.Extensions.Architecture.Fabrics.VerifierExtensions.Experimental*> compile-time method, which are better suited for this purpose.

## Marking a specific API as experimental

To generate warnings when an experimental API is being used, the best is to use the <xref:Metalama.Extensions.Architecture.Aspects.ExperimentalAttribute> attribute. Follow these steps:

1. Add the `Metalama.Extensions.Architecture` package to your project.

2. Annotate the API with the <xref:Metalama.Extensions.Architecture.Aspects.ExperimentalAttribute>. 

### Example: Using the Experimental attribute

In the following example, the `ExperimentalApi` class is explicitly marked as experimental.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Experimental.cs tabs="target"]

## Programmatically marking APIs as experimental 

If you want to mark several APIs as experimental using a programmatic rule instead of hand-picking declarations, you can use fabrics. Follow these steps.

1. Add the `Metalama.Extensions.Architecture` package to your project.

2. Create or reuse a fabric type as described in <xref:fabrics>.

3. Import the <xref:Metalama.Extensions.Architecture.Fabrics> namespace to benefit from extension methods.

4. Edit the  <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*>,  <xref:Metalama.Framework.Fabrics.NamespaceFabric.AmendNamespace*> or  <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> of this method. Open the dance by calling [amender.Verify()](xref:Metalama.Extensions.Architecture.Fabrics.AmenderExtensions.Verify*).

5. Select the experimental APIs using the <xref:Metalama.Extensions.Architecture.Fabrics.VerifierExtensions.Select*>, <xref:Metalama.Extensions.Architecture.Fabrics.VerifierExtensions.SelectMany*>  and <xref:Metalama.Extensions.Architecture.Fabrics.VerifierExtensions.Where*> methods.

6. Call the <xref:Metalama.Extensions.Architecture.Fabrics.VerifierExtensions.Experimental*> method.


### Example: Using the Experimental compile-time method

In the following example, all public members of `ExperimentalNamespace` are programmatically marked as experimental.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Experimental_Fabric.cs tabs="target"]