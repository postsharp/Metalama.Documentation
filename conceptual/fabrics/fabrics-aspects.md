---
uid: fabrics-aspects
---

# Adding Aspects in Bulk from a Fabric

You can use a fabric to programmatically add aspects to any declaration that is "under" the target of the fabric. Thanks to fabrics, you do not need to add aspects one by one using custom attributes.

* From a type fabric, you can add aspects to any member of this type or to the type itself.
* From a namespace fabric, you can add aspects to any type of that namespace, or to any member of one of these types.
* From a project fabric, you can add aspects to any type or member of that project.

To add an aspect from a fabric:

1. Create a fabric class and add an <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*>, <xref:Metalama.Framework.Fabrics.NamespaceFabric.AmendNamespace*> or <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*> method.

2. Call one of the following methods from <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*>, or

   *To select the type itself, simply use <xref:Metalama.Framework.Fabrics.IAmender`1.Outbound*?text=amender.Outbound> property.
   * To select type members (methods, fields, nested types, and others), call the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.Select*> or <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SelectMany*> method and, finally, provide a lambda expression that selects the relevant type members.

   The reason behind this design is that the <xref:Metalama.Framework.Fabrics.IAmender`1.Outbound*?text=amender.Outbound> property not only selects members declared in source code but also members introduced by other aspects, and that are unknown at the time <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> method is executed.

3. Call the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspect*> method.

## Example: Adding an aspect to all methods in a project

In the following example, a project fabric adds a logging aspect to all methods in a project.

[!include[Type Fabric Adding Aspects](../../code/Metalama.Documentation.SampleCode.AspectFramework/ProjectFabric.cs)]


## Example: Adding an aspect from a type fabric

In the following example, a type fabric adds a logging aspect to all public methods.

[comment]: # (TODO: make class partial and split into different file)


[!include[Type Fabric Adding Aspects](../../code/Metalama.Documentation.SampleCode.AspectFramework/TypeFabric.cs)]

