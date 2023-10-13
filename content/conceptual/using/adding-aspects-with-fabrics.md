---
uid: fabrics-adding-aspects
level: 200
---

# Adding many aspects at once

In <xref:quickstart-adding-aspects>, you learned to apply aspects one at a time using custom attributes. While this approach is suitable for aspects like caching or auto-retry, it can be overwhelming for other aspects like logging or profiling.

In this article, you will learn how to use _fabrics_ to add aspects to your targets _programmatically_.

## When to use fabrics

Fabrics enable you to add all aspects from a central place. You should consider using fabrics instead of custom attributes when the decision to add an aspect to a declaration can be easily expressed as a _rule_, and when this rule only depends on the metadata of the declaration, such as its name, signature, parent type, implemented interfaces, custom attributes, or any other detail exposed by the [code model](xref:Metalama.Framework.Code).

For instance, if you want to add logging to all public methods of all public types of a namespace, it is more efficient to do it using a fabric.

Conversely, it may not be advisable to use a fabric to add caching to all methods that start with the word _Get_ because you may end up creating more problems than you solve. Caching is typically an aspect you would hand-pick, and custom attributes are a better approach.

## Adding aspects using fabrics

To add aspects using fabrics:

1. Create a fabric class and derive it from <xref:Metalama.Framework.Fabrics.ProjectFabric>.

2. Override the <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*> abstract method.

3. Call one of the following methods from <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*>:

   * To select the type itself, simply use the <xref:Metalama.Framework.Fabrics.IAmender`1.Outbound*?text=amender.Outbound> property.
   * To select type members (methods, fields, nested types, etc.), call the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.Select*>, <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SelectMany*> or <xref:Metalama.Framework.Aspects.IAspectReceiver`1.Where*> method and provide a lambda expression that selects the relevant type members.

4. Call the <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspect*> or  <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspectIfEligible*> method.

> [!NOTE]
> The <xref:Metalama.Framework.Fabrics.IAmender`1.Outbound*?text=amender.Outbound> method will not only select members declared in source code, but also members introduced by other aspects and therefore unknown when the  <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> method is executed. This is why the _Amend_ method does not directly expose the code model.


### Example 1: Adding aspect to all methods in a project

In the following example, we use a fabric to apply a logging aspect to all methods in the current project.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/ProjectFabric.cs]

There are a few things to note in this example. The first point to consider is the `AmendProject` method. We aim to add aspects to different members of a project. Essentially, we are trying to _amend_ the project, hence the name.

Inside the `AmendProject` method, we get all the public methods and add _logging_ and _retrying_ aspects to these methods.

> [!WARNING]
> Sometimes CodeLense misses the aspects to show. For that time, it is required to rebuild the project.

### AddAspect or AddAspectIfEligible?

The difference between <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspect*> and  <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspectIfEligible*> is that <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspect*>  will throw an exception if you try adding an aspect to an ineligible target (for instance, a caching aspect to a `void` method), while <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspectIfEligible*> will silently ignore such targets.

* If you choose <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspect*>, you may be annoyed by exceptions and may have to add a lot of conditions to your `AmendProject` method. The benefit of this approach is that you will be _aware_ of these conditions.
* If you choose <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspectIfEligible*>, you may be surprised that some target declarations were silently ignored.

As is often the case, life does not give you a choice to be completely happy, but you can often choose which pain you want to suffer. In most cases, we recommend using <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspectIfEligible*>.

### Example 2: Adding more aspects using the same Fabric

In the following example, we add two aspects: logging and profiling. We add profiling only to public methods of public classes.

For each project, it is recommended to have only one project fabric. Having several project fabrics makes it difficult to understand the aspect application order.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/ProjectFabric_TwoAspects.cs]

### Example 3: Adding aspects to all methods in a given namespace

To add the Logging aspect (`LogAttribute`) to all the methods that appear in types within namespaces that start with the prefix `Outer.Inner` and all the child types located in any descendant namespace, use the following fabric.

[!metalama-test  ~/code/DebugDemo2/Fabric2.cs tabs="target"]

In this fabric, we use the `GlobalNamespace.GetDescendant` method to get all the children's namespace of the given namespace (in this case, `Outer.Inner`). The first `SelectMany` calls get all the types in these namespaces, and the inner `SelectMany` call gets all the methods in these types. This results in an `IAspectReceiver<IMethod>`. So the final call <xref:Metalama.Framework.Aspects.IAspectReceiver`1.AddAspectIfEligible*> adds the `Log` aspect to all eligible methods.

### Example 4: Adding the `Log` aspect only to derived classes of a given class

Sometimes you may not need or want to add aspects to all the types but only to a class and its derived types. The following fabric shows how you can add those. In this example fabric, you see how to get the derived types of a given type and how to add aspects to them.

[!metalama-test ~/code/Metalama.Documentation.QuickStart.Fabrics.2/AddLoggingToChildrenFabric.cs tabs="target"]


> [!div class="see-also"]
> <xref:video-fabrics-and-inheritance>
> <xref:fabrics-adding-aspects>

