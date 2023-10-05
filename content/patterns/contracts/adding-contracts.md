---
uid: value-contracts
---

# Validating field, property and parameter values

## Validating input values of fields, properties or parameters (preconditions)

Most of the time, you will add contracts directly to their target field, property or parameter using custom attributes.

Follow these simple steps:

1. Add  the `Metalama.Patterns.Contracts` package.
2. Add one of the <xref:contract-types?text=contract attributes> to the fields, properties or parameters you want to validate.

### Example: validating input values

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/Input.cs]

## Using contract inheritance

By default, all contracts are inherited from interfaces and `virtual` or `abstract` members to their implementation. It means that when you add a contract to an interface member, it will be automatically implemented in all classes implementing this interface. The same applies for `virtual` or `abstract` members.

### Example: contract inheritance

In the following example, contracts are applied to members of the `ICustomer` interface. You can observe that they are automatically implemented by the `Customer` class implementing the interface.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/Inheritance.cs]

## Validating output values (postconditions)

The most common use of code contracts is to validate the input data flow. This is what happens by default when you apply a contract to a field, property, or any parameter except `out` ones. When you validate the input data flow, you are basically being distrustful and defensive against the code _calling_ you, which is totally a best practice because it prevents defects of foreign components to cause unexplainable failures in your own component.

It can be also useful to validate the output data flow. This, in turn, is useful when you distrust the _implementation_ of some interface or virtual method. That's why it makes more sense to validate the output data flow when the constraint is applied to an interface or virtual member, and inheritance is used to enforce the constraint on implementations.

If the validation of the output data flow fails, an exception of type <xref:Metalama.Patterns.Contracts.PostconditionViolationException> is thrown.

### Return values

To validate the return value of a method, apply the contract to the return parameter using the `[return: XXX]` syntax.


#### Example: contract on return value

In the following example, a `[NotEmpty]` contract has been added to the return value of the `GetCustomerName` method in the `ICustomerService` interface. The `CustomerService` class implements this interface, and you can observe how the return value of the `GetCustomerName` method implementation is being validated by the `[NotEmpty]` contract.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/ReturnValue.cs]

### Out parameters

To validate the value of an `out` parameter just before the method exits, just apply the custom attribute to the parameter as usual.

#### Example: contract on out parameter

In the following example, a `[NotEmpty]` contract has been added to the `out` parameter of the `TryGetCustomerName` method in the `ICustomerService` interface. The `CustomerService` class implements this interface, and you can observe how the of the value of the `out` parameter of the `TryGetCustomerName` method implementation is being validated by the `[NotEmpty]` contract.


[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/OutParameter.cs]

### Ref parameters

By default, only the input value of `ref` parameters is validated. To change the default behavior, use the <xref:Metalama.Patterns.Contracts.ContractBaseAttribute.Direction> property. To validate only the output value, use the <xref:Metalama.Framework.Aspects.ContractDirection.Output> value. To validate both input and output values, use <xref:Metalama.Framework.Aspects.ContractDirection.Both>.

#### Example: contract on ref parameter

In the following example, a `[Positive]` contract has been added to the `ref` parameter of the `CountWords` method of the `IWordCounter`. The <xref:Metalama.Patterns.Contracts.ContractBaseAttribute.Direction> property is set to <xref:Metalama.Framework.Aspects.ContractDirection.Both> so that both the input and the output value of the parameter are verified. The `WordCounter` class implements the `IWordCounter` interface. You can observe that the `[Positive]` contract was verified both when the method enters and completes.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/RefParameter.cs]

### Fields and properties

This may sound surprising at first sight, but fields and properties also have an input and an output flow if you take the look at them with the right mindset. The input flow is the _assignment_ one, i.e. the value passed to the _setter_, while the output flow is the one of the _getter_.

By default, when a contract is applied to a field or property that has a setter, the contract validates the value passed to the setter. 

Just like with `ref` parameters, you can use the the <xref:Metalama.Patterns.Contracts.ContractBaseAttribute.Direction> and set it to either <xref:Metalama.Framework.Aspects.ContractDirection.Output> or <xref:Metalama.Framework.Aspects.ContractDirection.Both>.

#### Example: output contracts on properties

In the following example, we have added the `[NotEmpty]` contract to two properties of the `IItem` interface. The `Key` property is get-only, so the contract applies to the getter return value by default. The `Value` property has both a getter and a setter, so we have set the <xref:Metalama.Patterns.Contracts.ContractBaseAttribute.Direction> property to <xref:Metalama.Framework.Aspects.ContractDirection.Both> to validate both the input value and the output value.

The `Item` class implements the `IItem` interface. You can observe that the contrats defined on the `IItem` interface are implemented in code. 

In the `Item` class, the `Key` property is implemented as an automatic property. It might seem surprising that the contract is still implemented in the getter instead of in the setter. The reason is to preserve the semantics of the contract: when applied to the getter, the contract promises to throw a <xref:Metalama.Patterns.Contracts.PostconditionViolationException> exception upon violation. Implementing the contract on the getter would change the contract. Specifically, no exception would be thrown if the property is never set.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/Property.cs]

### Changing the default contract direction

You can change the default contract direction by using configuring <xref:Metalama.Patterns.Contracts.ContractOptions> object for the whole project, a specific namespace, or selected types. 

TODO
