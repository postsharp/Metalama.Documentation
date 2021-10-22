---
uid: applying-aspects
---

# Applying Aspects to Code

## Declaratively, with a custom attribute

If the aspect derives from `System.Attribute`, you can apply it to your code as a normal custom attribute.


## Programmatically, with a fabric

(TODO: document better)

Thanks to fabrics, you can add aspects to a large number of declarations without using custom attributes. You can add a fabric to a project, to a namespace, or to a type:

* For a project-level fabric, add a type implementing the <xref:Caravela.Framework.Fabrics.IProjectFabric> interface anywhere in the project, and implement the <xref:Caravela.Framework.Fabrics.IProjectFabric.AmendProject%2A> method.
* For a namespace-level fabric, add a type implementing the <xref:Caravela.Framework.Fabrics.INamespaceFabric> interface in the desired namespace, and implement the <xref:Caravela.Framework.Fabrics.INamespaceFabric.AmendNamespace%2A> method. 
* For a type-level fabric, add a nested type implementing <xref:Caravela.Framework.Fabrics.ITypeFabric> interface in the target type, and implement the <xref:Caravela.Framework.Fabrics.ITypeFabric.AmendType%2A> method.

The `Amend` methods accept a parameter of type <xref:Caravela.Framework.Fabrics.IAmender%601>, which allows you to add aspects by calling <xref:Caravela.Framework.Aspects.IAspectLayerBuilder%601.WithMembers%2A> and then <xref:Caravela.Framework.Aspects.IDeclarationSelection%601.AddAspect%2A>

## Programmatically, from another aspect

If you're an aspect author and your aspect requires another aspect to work, it is better if your aspect adds it automatically where it needs it. See <xref:child-aspects> for details.
