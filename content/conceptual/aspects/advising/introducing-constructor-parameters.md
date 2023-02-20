---
uid: introducing-constructor-parameters
---

# Introducing Constructor Parameters

Most of the time when an aspect needs to introduce a parameter to a constructor, it is because it needs to pull a dependency from a dependency injection framework. In this situation, it is recommended to use the <xref:Metalama.Extensions.DependencyInjection> framework described in <xref:dependency-injection>.

Implementations of dependency injection frameworks typically introduce parameters using the method described here.

To append a parameter to a constructor, use the <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceParameter*> method. This method requires several arguments: the target <xref:Metalama.Framework.Code.IConstructor>, the name, the type of the new parameter, and the default value of this parameter. Note that you cannot introduce a parameter without specifying a default value.

The `pullAction` parameter of the <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceParameter*> method allows you to specify the value that is passed to this parameter in other constructors calling the specified constructor, using the `: this(...)` or `: base(...)` syntax. The `pullAction` parameter must receive a function that returns a <xref:Metalama.Framework.Advising.PullAction> value. To create a <xref:Metalama.Framework.Advising.PullAction> value, use one of three available static members of this type, such as <xref:Metalama.Framework.Advising.PullAction.UseExistingParameter*>, <xref:Metalama.Framework.Advising.PullAction.UseExpression*>, or <xref:Metalama.Framework.Advising.PullAction.IntroduceParameterAndPull*>.

## Example

The following example shows an aspect that registers the current instance in a registry of type `IInstanceRegistry`. The aspect appends a parameter of type `IInstanceRegistry` to the target constructor and calls the `IInstanceRegistry.Register(this)` method.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceParameter.cs name="Introducing parameters"]
