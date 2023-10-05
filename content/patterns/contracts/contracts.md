---
uid: contract-patterns
---

# Ready-made contracts

Metalama Contracts assist in implementing the principles of Contract-Based Programming, a software engineering practice that significantly enhances software reliability and clarity. In contract-based programming, a "contract" delineates a set of obligations and benefits between two parties: a caller and a callee. Metalama Contracts embody three fundamental concepts of contract-based programming: preconditions, postconditions, and type invariants.

_Preconditions_ ensure that necessary conditions are met before a method executes, while _postconditions_ verify the outcomes post-execution. This enforcement aids in early error detection, preventing a defect in one component from manifesting itself in another component. For instance, when preconditions are properly enforced, one can assume that a <xref:System.NullReferenceException> in a component is attributable to this component itself, whereas an <xref:System.ArgumentException> thrown by a method shifts the blame to its _caller_. In a similar vein, Metalama Contracts introduce a new exception type, <xref:Metalama.Patterns.Contracts.PostconditionViolationException>, indicating that the method throwing it is to blame for the defect.

_Type invariants_ are conditions that remain true throughout the entire lifetime of an instance of a type. Type invariants prove useful in diagnosing defects in types with mutable states and complex logic.

By adhering to these specified contracts, software transitions towards a modular and decoupled design, fostering system extensibility and adaptability. Overall, the enforcement of preconditions and postconditions is a pragmatic practice crucial for the creation of maintainable, reliable, and scalable software systems.

Compared to hand-written precondition checkings, Metalama Contracts offer the following benefits:

* **More readable**. Metalama Contracts are represented as custom attributes there is less code to read and understand.

* **Inherited**. You can add a Metalama Code Contract attribute to an interface method parameter and it will automatically be enforced in all implementations of this method.

* **Customizable**. It's easy to change the code generation pattern that throws the exception. For instance, you can localize your exception messages as an afterthought, without any impact on your business code.


## In this chapter

This chapter is composed of the following articles:

| Article | Description |
|--|--|
| <xref:value-contracts> | This article describes how to add preconditions and postconditions to your fields, properties and parameters. |
|  <xref:invariants> | This article describes how to add type invariants and execute verification logic after completion of all public methods. |
| <xref:configuring-contracts> | This article describes how to configure Metalama Contracts. |

