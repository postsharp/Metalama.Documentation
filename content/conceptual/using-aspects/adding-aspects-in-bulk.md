---
uid: adding-aspects-in-bulk
---

# Adding many aspects at once

In <xref:quickstart-adding-aspects>, you learned how to apply aspects one at a time using custom attributes. While this approach is appropriate for aspects like caching or auto-retry, it can be overwhelming for other aspects like logging or profiling.

In this article, you will learn how to use _fabrics_ to add aspects to your targets _programmatically_.

## Fabrics

_Fabrics_ are special classes in your code that execute at compile time, within the compiler, and at design time, within your IDE. Unlike aspects, fabrics do not need to be _applied_ to any declaration, nor do they need to be _called_ from anywhere. Their main method will be called at the right time just because it exists in your code.

Fabrics are really helpful when you need to add aspects to different targets programmatically.

There are three different types of fabrics:

| Fabric type | Abstract class | Main method | Purpose
|------------|---------|--|--|
| Project Fabric| <xref:Metalama.Framework.Fabrics.ProjectFabric>  | <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*> | To add aspects to different declarations in the current project.
| Namespace Fabric| <xref:Metalama.Framework.Fabrics.NamespaceFabric>  | <xref:Metalama.Framework.Fabrics.NamespaceFabric.AmendNamespace*> | To add aspects to different declarations in the namespace that contains the fabric type.
| Type Fabric | <xref:Metalama.Framework.Fabrics.TypeFabric> | <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> | To add aspects to different members of the type that contains the nested fabric type.

## Adding aspects using fabrics

To add aspects using fabrics:

1. Create a fabric class and derive it from <xref:Metalama.Framework.Fabrics.ProjectFabric>,  <xref:Metalama.Framework.Fabrics.NamespaceFabric> or <xref:Metalama.Framework.Fabrics.TypeFabric>.

    > [!WARNING]
    > Type fabrics must be nested classes and apply to their nesting type.
    > Namespace fabrics apply to their namespace.

2. Add an <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*>, <xref:Metalama.Framework.Fabrics.NamespaceFabric.AmendNamespace*> or <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*> method.

3. Call one of the following methods from <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*>, or

   * To select the type itself, simply use <xref:Metalama.Framework.Fabrics.IAmender`1.Outbound*?text=amender.Outbound> property.
   * To select type members (methods, fields, nested types, ...), call the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.Select*> or <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SelectMany*> method and provide a lambda expression that selects the relevant type members.

4. Call to the  <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspect*> method.

> [!NOTE]
> The <xref:Metalama.Framework.Fabrics.IAmender`1.Outbound*?text=amender.Outbound> method will not only select members declared in source code, but also members introduced by other aspects and that are unknown when your  <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> method is executed. This is why the _Amend_ method does not directly expose the code model.

## Example 1: Adding aspect to all public methods of a type

In this section, you shall learn how to add `[Log]` attribute to all public methods of a given project. To add an aspect to all public methods add the following Fabric to your project.

[!code-csharp[](~\code\Metalama.Documentation.QuickStart.Fabrics\Fabric.cs)]

> [!NOTE]
> Fabrics need not be applied. They are triggered because of their presence in the project.

When this fabric is added to a project with the following types,

[!metalama-sample ~/code/DebugDemo/Entities.cs tabs="target"]

It will show that `[Log]` aspect is applied to all of the public methods as shown below

[!metalama-sample ~/code/DebugDemo/Entities.cs tabs="transformed"]

## Example 2: Adding more aspects using the same Fabric

For each project, it is recommended to have only one project fabric.

> [!WARNING]
> Having many project fabric makes it difficult to decide the aspect application order and it is complicated.

To add another aspect we have to alter the project Fabric. This time we will add the capability to add the aspect `Retry` to all public methods that start with the word `Try`.

To do this alter the Fabric like this.

[!metalama-sample  ~/code/DebugDemo2/Fabric.cs tabs="target"]

There are a few things to note in this example. The first point to consider is the `AmendPoject` method. We are trying to add aspects to different members of a project. So essentially we are trying to _amend_ the project. Thus the name.

Inside the `AmendProject` method, we get all the public methods and add _logging_ and _retrying_ aspect to these methods.

> [!WARNING]
> Sometimes CodeLense misses the aspects to show. For that time it is required to rebuild the project.

## Example 3: Adding aspects to all methods in a given namespace

To add the Logging aspect (`LogAttribute`) to all the methods that appear in types within namespaces that starts with the prefix `Outer.Inner` and all the child types located in any descendent namespace use the following fabric

[!metalama-sample  ~/code/DebugDemo3/Fabric.cs tabs="target"]

In this fabric, we use `GlobalNamespace.GetDecendant` method to get all the children's namespace of the given namespace (In this case `Outer.Inner`). The first `SelectMany` calls get all the types in these namespaces and the inner `SelectMany` call gets all the methods in these types. This results in an `IAspectReceiver<IMethod>`. So the final call <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspectIfEligible*> adds the `Log` aspect to all eligible methods.

## Example 4: Adding `Log` aspect only to derived classes of a given class

Sometimes you may not need or want to add aspects to all the types but only to a class and its derived types. The following fabric shows how you can add those. In this example fabric you see how to get the derived types of a given type and how to add aspects to them.

[!metalama-sample ~/code/Metalama.Documentation.QuickStart.Fabrics.2/AddLoggingToChildrenFabric.cs tabs="target"]



## The common pattern

So you may have noticed a common pattern among these examples. Whenever you need to add an aspect to a batch of methods, types etc; you need to create an <xref:Metalama.Framework.Aspects.IAspectReceiver`1> for that type. So if you want to add aspects to many methods, you need to create an instance of `IAspectReceiver` of <xref:Metalama.Framework.Code.IMethod> If you want to add aspects to many types you need to create an `IAspectReceiver` instance of <xref:Metalama.Framework.Code.IType>

