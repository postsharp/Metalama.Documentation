---
uid: simple-contracts
level: 200
---

# Getting started: contracts

One of the most popular use cases of aspect-oriented programming is to create a custom attribute to validate the field, property, or parameter to which it is applied. Typical examples are `[NotNull]` or `[NotEmpty]`.

In Metalama, you can achieve this using a _contract_. With a contract, you can:

* throw an exception when the value does not fulfill a condition of your choice, or
* normalize the received value (for instance, trimming the whitespace of a string).

Technically speaking, a contract is a piece of code that you inject after _receiving_ or before _sending_ a value. You can do more than throw an exception or normalize the value.


## The simple way: overriding the ContractAspect class

1. Add the `Metalama.Framework` package to your project.

2. Create a new class derived from the <xref:Metalama.Framework.Aspects.ContractAspect> abstract class. This class will be a custom attribute, so it is a good idea to name it with the `Attribute` suffix.


3. Implement the <xref:Metalama.Framework.Aspects.ContractAspect.Validate*> method in plain C#. This method will serve as a <xref:templates?text=template> defining the way the aspect overrides the hand-written target method.

    In this template, the incoming value is represented by the parameter name `value`, regardless of the real name of the field or parameter.


4. The aspect is a custom attribute. You can add it to any field, property, or parameter. To validate the return value of a method, use this syntax: `[return: MyAspect]`.

