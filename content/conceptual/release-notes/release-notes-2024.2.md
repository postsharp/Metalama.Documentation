---
uid: release-notes-2024.2
summary: ""
---

# Metalama 2024.2

> [!WARNING]
>  This release is under development.

## Introduction of classes and constructors

* It is now possible to introduce whole classes by using the `IAdviceFactory.IntroduceClass` method. This method returns an `IAdviser<INamedType>`, which you can then use to add members to the new type.
* New advice method `IAdviceFactory.IntroduceConstructor` to introduce a constructor into an existing or new type.



## Improvements in fabrics and IAspectReceiver

*  The`IAmender.Outbound` property is now redundant and has been marked as `[Obsolete]`. The `IAmender` interface now exposes `IAspectReceiver`, which was previously exposed by `Outbound`. The `Outbound` property is still required for `IAspectBuilder`.
* New method`IAspectReceiver.Tag`:  adds an arbitrary tag that is carried on and available for all lambdas on the right side of the `Tag` method for new overloads of all  (or most)`IAspectReceiver` methods.
* New method`IAspectReceiver.SelectTypes`: gets all types in the current context (typically namespace, compilation, or current type)
* New method `IAspectReceiver.SelectTypesDerivedFrom`: gets all types in the current context derived from a given type.
* New extension method `IAspectReceiver<ICompilation>.SelectReferencedAssembly`: gets a referenced assembly.
* New extension method `IAspectReceiver<ICompilation>.SelectReflectionType`: gets a type given by `System.Type`.
* The right side of query operators like `IAspectReceiver.SelectMany()`, `IAspectReceiver.SelectTypes` or `IAspectReceiver.SelectTypesDerivedFrom` now executes concurrently.
* When a part of a query is used several times (typically by storing the query in a local variable), its result is cached.

## Improvements in Metalama.Extensions.Architecture

### Reference validator granularity

The reference validator feature now has a concept of _validator granularity_, which can be set to `Compilation`, `Type`, `Member`, or `Declaration`. The idea is that when a validator is invariant within some level of granularity, then its predicate should only be evaluated once within the declaration at this level of granularity. For instance, if a validator granularity is set to `Namespace`, then _all_ references within that namespace will be either valid or invalid at the same time.

The _validator granularity_ concept is essential to improve the performance of validators, as references can be validated collectively instead of one by one.

Code written against the previous API will get obsolescence warnings. We suggest porting your validators to the new API and choosing the coarsest possible granularity.

### Other improvements

* The new class `Metalama.Extensions.Architecture.Predicates.ReferenceEndPredicate` serves as a base for predicates that depend on a single end of the reference to validate. This is a preparation for some next feature, allowing the validation of inbound references instead of just outbound references.

## Improvements in diagnostic suppressions

 Diagnostic suppressions can now be filtered by argument thanks to the new `SuppressionDefinition.WithFilter` method. 

## Improvements in the test framework

* The default diff interactive tool will now be opened when an aspect test fails (i.e. the expected snapshot is different than the actual one). The feature works with [DiffEngine](https://github.com/VerifyTests/DiffEngine) and integrates with [DiffEngineTray](https://github.com/VerifyTests/DiffEngine/blob/main/docs/tray.md).


## Improvements in the code model

* Adding `IExpression.WithType` and `IExpression.WithNullability` to override the inferred type or nullability of a captured expression.
* New method `StatementFactory.FromTemplate` to create an `IStatement` from a template.
* New concept `IStatementList` to represent an unresolved list of statements. New extension methods `IStatement.AsList()`, `IEnumerable<IStatement>.AsList()`, and `IStatement.UnwrapBlock` to create lists. New class `StatementListBuilder` to create an `IStatementList` by adding elements.
* New class `SwitchStatementBuilder` to dynamically creating a `switch` statement (cases can be added programmatically -- only literal case labels are currently supported.)
* New method `IMethodInvoker.CreateInvokeExpression` generating an `IExpression` that represents a method invocation. Can be called outside of a template context.
* New interface `IConstructorInvoker` with methods `Invoke` and `CreateInvocationExpression`.

## Improvements in code formatting

* The performance of whole-project output code formatting has been improved. Note that code formatting is disabled by default so it should not affect your standard builds, but `LamaDebug` builds should be faster.
* Redundancies in member access expressions are eliminated where application  (e.g. `this.X` or `MyType.Y` becomes `X` or `Y`).
* Non-extension calls to extension methods in templates are transformed into extension calls. This is useful because extension methods cannot be called on dynamic types.
* The discard parameter `_`, when used in templates, was renamed to `__1`, `__2` and so on.

## Improvements in advising and code templates

* Added support for lambda statements and anonymous methods of known scope, i.e. either run-time or compile-time (the scope can be coerced using `meta.RunTime` or `meta.CompileTime` when it is not obvious from the context). Lambda expressions returning `dynamic` are not supported and won't be. Single-statement lambdas (e.g. `() => { return 0; }` are transparently simplified into expression lambdas (e.g. `() => 0`).
* New class `Promise<T>` and interfaces `IPromise<T>` and `IPromise` to represent results that are not available yet. This mechanism allows to resolve chicken-or-egg issues when introducing members, when a template must receive a reference to a declaration that has not been introduced yet. A `Promise<T>` can be passed as an argument to a template, which receives it on a parameter of type `T`. 

### Changes in interface implementation
* The `ImplementInterface` advice no longer cares if all interface members are present. Errors will appear during compilation.
* Interface members can be introduced using `[Introduce]` or programmatically using `IAdviceFactory.IntroduceMethod`.
* `[InterfaceMember]` is marked as `[Obsolete]` and replaced by `[Introduce]` or `[ExplicitInterfaceMember]`.
* `InterfaceMemberOverrideStrategy` becomes obsolete. The side case `MakeExplicit` is replaced by a programmatic strategy.
* `IInterfaceImplementationResult` is `IAdviser<INamedType>` and allows to introduce members. Using `IInterfaceImplementationResult.WithExplicitImplementation() : IAdviser<INamedType>` it is possible to introduce named type.

## Breaking Changes

* The `Metalama.Extensions.Architecture.Predicates.ReferencePredicate` class has a new abstract member `Granularity`. Its constructor now requires a `ReferencePredicateBuilder`.
* `ReferenceValidationContext` no longer reports several `ReferenceKinds`, but only the deepest one. For instance, in `class A : List<C>;`,  the reference to `C` is of kind `GenericArgument` and no longer `BaseType | GenericArgument`. Combined flags added complexity, and we did not see a use case for them.
* Projects that were using transitive reference validators (or architecture constraints), if they were built with a previous version of Metalama, must be rebuilt. 
* Relationships specified with `AspectOrderAttribute` are now applied to derived aspect classes by default. To revert to the previous behavior, set the `AspectOrderAttribute.ApplyToDerivedTypes` property to `false`.