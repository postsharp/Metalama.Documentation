---
uid: release-notes-2024.2
summary: ""
---

# Metalama 2024.2

> [!WARNING]
>  This release is under development.


## Improvements of fabrics and IAspectReceiver

*  The`IAmender.Outbound` property is now redundant and has been marked as `[Obsolete]`. The `IAmender` interface now exposes `IAspectReceiver`, which was previously exposed by `Outbound`. The `Outbound` property is still required for `IAspectBuilder`.
* New method`IAspectReceiver.Tag`:  adds an arbitrary tag that is carried on and available for all lambdas on the right side of the `Tag` method for new overloads of all  (or most)`IAspectReceiver` methods.
* New method`IAspectReceiver.SelectTypes`: gets all types in the current context (typically namespace, compilation, or current type)
* New method `IAspectReceiver.SelectTypesDerivedFrom`: gets all types in the current context derived from a given type.
* New extension method `IAspectReceiver<ICompilation>.SelectReferencedAssembly`: gets a referenced assembly.
* New extension method `IAspectReceiver<ICompilation>.SelectReflectionType`: gets a type given by `System.Type`.
* The right side of query operators like `IAspectReceiver.SelectMany()`, `IAspectReceiver.SelectTypes` or `IAspectReceiver.SelectTypesDerivedFrom` now executes concurrently.
* When a part of a query is used several times (typically by storing the query in a local variable), its result is cached.

## Improvements of Metalama.Extensions.Architecture

### Reference validator granularity

The reference validator feature now has a concept of _validator granularity_, which can be set to `Compilation`, `Type`, `Member`, or `Declaration`. The idea is that when a validator is invariant within some level of granularity, then its predicate should only be evaluated once within the declaration at this level of granularity. For instance, if a validator granularity is set to `Namespace`, then _all_ references within that namespace will be either valid or invalid at the same time.

The _validator granularity_ concept is essential to improve the performance validators, as references can be validated collectively instead of one by one.

Code written against the previous API will get obsolescence warnings. We suggest porting your validators to the new API and choosing the coarsest possible granularity.

### Other improvements

* The new class `Metalama.Extensions.Architecture.Predicates.ReferenceEndPredicate` serves as a base for predicates that depend on a single end of the reference to validate. This is a preparation for some next feature, allowing the validation of inbound references instead of just outbound references.

## Improvements in diagnostic suppressions

 Diagnostic suppressions can now be filtered by argument thanks to the new `SuppressionDefinition.WithFilter` method. 

## Improvements in the test framework

* The default diff interactive tool will now be opened when an aspect test fails (i.e. the expected snapshot is different than the actual one). The feature works with [DiffEngine](https://github.com/VerifyTests/DiffEngine) and integrates with [DiffEngineTray](https://github.com/VerifyTests/DiffEngine/blob/main/docs/tray.md).

## Breaking Changes

* The `Metalama.Extensions.Architecture.Predicates.ReferencePredicate` class has a new abstract member `Granularity`. Its constructor now requires a `ReferencePredicateBuilder`.
* `ReferenceValidationContext` no longer reports several `ReferenceKinds`, but only the deepest one. For instance, in `class A : List<C>;`,  the reference to `C` is of kind `GenericArgument` and no longer `BaseType | GenericArgument`. Combined flags added complexity, and we did not see a use case for them.
* Projects that were using transitive reference validators (or architecture constraints), if they were built with a previous version of Metalama, must be rebuilt. 