---
uid: applying-aspects
---

# Applying Aspects to Code

## Declaratively, with a custom attribute

If the aspect derives from `System.Attribute`, you can apply it to your code as a normal custom attribute.


## Programmatically, with a fabric

(TODO: document better)

Thanks to fabrics, you can add aspects to a large number of declarations without using custom attributes. You can add a fabric to a project, to a namespace, or to a type:

* For a project-level fabric, add a type implementing the <xref:Caravela.Framework.Fabrics.ProjectFabric> class anywhere in the project, and implement the <xref:Caravela.Framework.Fabrics.ProjectFabric.AmendProject%2A> method.
* For a namespace-level fabric, add a type implementing the <xref:Caravela.Framework.Fabrics.NamespaceFabric> class in the desired namespace, and implement the <xref:Caravela.Framework.Fabrics.NamespaceFabric.AmendNamespace%2A> method. 
* For a type-level fabric, add a nested type implementing the <xref:Caravela.Framework.Fabrics.TypeFabric> class in the target type, and implement the <xref:Caravela.Framework.Fabrics.TypeFabric.AmendType%2A> method.

The `Amend` methods accept a parameter of type <xref:Caravela.Framework.Fabrics.IAmender%601>, which allows you to add aspects by calling <xref:Caravela.Framework.Aspects.IAspectLayerBuilder%601.WithMembers%2A> and then <xref:Caravela.Framework.Aspects.IDeclarationSelection%601.AddAspect%2A>

### Example: project fabric

The following examples shows how to add a `Log` aspect to all methods in the current project.

[!include[Project Fabric](../../code/Caravela.Documentation.SampleCode.AspectFramework/ProjectFabric.cs)]

### Example: type fabric fabric

[!include[Type Fabric](../../code/Caravela.Documentation.SampleCode.AspectFramework/TypeFabric.cs)]

## Programmatically, from another aspect

If you're an aspect author and your aspect requires another aspect to work, it is better if your aspect adds it automatically where it needs it. See <xref:child-aspects> for details.
