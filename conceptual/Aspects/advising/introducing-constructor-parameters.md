---
uid: introducing-constructor-parameters
---

# Introducing Constructor Parameters

Most of the times, when an aspects needs to introduce a constructor to a parameter, it is because it needs to pull a dependency from a dependency injection framework. In this situation, it is recommended to use the `Metalama.Extensions.DependencyInjection` framework described in <xref:dependency-injection>.

Implementations of dependency injection frameworks typically introduce parameters using the method described here.

To append a parameter to a constructor, use the <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceParameter*> method. This method requires several arguments: the target <xref:Metalama.Framework.Code.IConstructor>, the name and type of the new parameter, as well as the default value of this parameter. Note that it is currently not allowed to introduce a parameter without specifying a default value.

The `pullAction` parameter of the <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceParameter*> method deserves attention: it allows you to specify the value passed to this parameter in other constructors calling the specified construtor, using the `: this(...)` or `: base(...)` syntax. The `pullAction` parameter must receive a function that returns a <xref:Metalama.Framework.Advising.PullAction> value. You can create a <xref:Metalama.Framework.Advising.PullAction> value using one of the following static methods of this type:

*  <xref:Metalama.Framework.Advising.PullAction.UseExistingParameter*>  to use an existing parameter of the calling constructor,
*  <xref:Metalama.Framework.Advising.PullAction.UseExpression*> to pass an arbitrary expresssion,
*  <xref:Metalama.Framework.Advising.PullAction.IntroduceParameterAndPull*> to introduce a new parameter into the calling constructor. In this case, the `pullAction` function is called again, recursively, for each constructor calling this new constructor.


## Example

The following example shows an aspect that registers the current instance in a registry of type `IInstanceRegistry`. The aspect appends a parameter of type `IInstanceRegistry` to the target constructor, then calls the `IInstanceRegistry.Register(this)`.

[!include[Introducing parameters](../../../code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceParameter.cs)]
