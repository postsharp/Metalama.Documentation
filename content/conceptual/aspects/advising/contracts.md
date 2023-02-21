---
uid: contracts
level: 300
---

# Validating parameter, field and property values with contracts

In <xref:simple-contracts>, you have learned to create simple contracts by implementing the  <xref:Metalama.Framework.Aspects.ContractAspect> class.

In this article, we will cover more advanced scenarios.

### Accessing the metadata of the field, property, or parameter being validated


Your template code can access its context using the following meta APIs:

- `meta.Target.Declaration` returns the target parameter, property, or field.
- `meta.Target.FieldOrProperty` returns the target property or field, but will throw an exception if the contract is applied to a parameter.
- `meta.Target.Parameter` returns the parameter (including the parameter representing the return value), but will throw an exception if the contract is applied to a field or property.
* `meta.Target.ContractDirection` returns `Input` or `Output` according to the data flow being validated ([see below](#contract-directions)). Typically, it is `Input` for input parameters and property setters, and `Output` for output parameters and return values.


### Example: NotNull

The following aspect throws an exception if the field, property, or parameter to which it is applied receives a null value, or if a null value is assigned to an `out` parameter or to the return value.

[!metalama-sample  ~/code/Metalama.Documentation.SampleCode.AspectFramework/NotNull.cs name="NotNull"]

### Example: Trim

The following aspect normalizes the received value by calling the `string.Trim` method. The only difficulty is that it needs to choose between `value.Trim` and the null-conditional `value?.Trim` according to the nullability of the target.

[!metalama-sample  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Trim.cs name="Trim"]


### Contract directions

By default, the <xref:Metalama.Framework.Aspects.ContractAspect> aspect applies the contract to the _default data flow_ of the target parameter, field, or property.

The default direction is as follows:

* for input and `ref` parameters: the _input_ value,

* for fields and properties: the _assigned_ value (i.e. the `value` parameter of the setter),

* for `out` parameters and return value parameters, the _output_ value.

To change the filter direction, set the <xref:Metalama.Framework.Aspects.ContractAspect.Direction> property of the <xref:Metalama.Framework.Aspects.ContractAspect> class in the constructor.

To learn about customizing eligibility for different contract directions than the default one, see the remarks on the documentation of the <xref:Metalama.Framework.Aspects.ContractAspect> class. To learn about eligibility, see <xref:eligibility>.

## Adding contract advice programmatically

Just as any advice, you can add a contract to a parameter, field, or property from the `BuildAspect` method of your aspect using the <xref:Metalama.Framework.Advising.IAdviceFactory.AddContract*> method.

> [!NOTE]
> When possible, provide all contracts to the same method from a single aspect. It has better compile-time performance than using several aspects.

### Example: automatic NotNull

The following snippet shows how to automatically add precondition checks for all situations in the public API where a non-nullable parameter could receive a null value from a consumer.

The [fabric](xref:fabrics) adds a method-level aspect to all exposed methods. Then, the aspect adds individual contracts using the <xref:Metalama.Framework.Advising.IAdviceFactory.AddContract*> method.


[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/NotNullFabric.cs name="NotNull Fabric"]

