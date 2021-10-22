---
uid: eligibility
---

# Defining the Eligibility of Aspects

## Why to define eligibility?

Most of aspects are designed and implemented for specific kinds of target declarations. For instance, you may decide that your caching aspect will not support `void` methods or methods with an `out` or `ref` parameter. It is important, as the author of the aspect, to make sure that the user of your aspect applies it only to the declaration that you expect. Otherwise, at best, the aspect will cause build errors and confuse the user. At worse, the run-time behavior of your aspect will be incorrect.

By defining the eligibility, you ensure that:

* the IDE or the compiler will report a nice error message when the user tries to add the aspect to an unsupported declaration, and that
* the IDE will only propose code action in the refactoring menu for eligible declarations.

## Defining eligibility

To define the eligibility of your aspect, implement or override the <xref:Caravela.Framework.Eligibility.IEligible%601.BuildEligibility%2A> method of the aspect. Use the `builder` parameter, of type <xref:Caravela.Framework.Eligibility.IEligibilityBuilder%601>, to specify the requirements of your aspect. For instance, use <xref:Caravela.Framework.Eligibility.EligibilityExtensions.MustBeNonAbstract%2A?text=builder.MustBeNonAbstract()> to require a non-abstract method.

A number of predefined eligibility conditions are implemented by the <xref:Caravela.Framework.Eligibility.EligibilityExtensions> static class. You add a custom eligibility condition by calling <xref:Caravela.Framework.Eligibility.EligibilityExtensions.MustSatisfy%2A> and providing your own lambda expression. This method also expects the user-readable string that should be included in the error message when the user attempts to add the aspect to an ineligible declaration.

>[!NOTE] 
> Your implementation of <xref:Caravela.Framework.Eligibility.IEligible%601.BuildEligibility%2A> must not reference any instance member of the class. Indeed, this method is called on an instance obtained using `FormatterServices.GetUninitializedObject` that is, _without invoking the class constructor_.

### Example

> TODO

## When to emit custom errors instead?

It may be tempting to add an eligibility condition for every requirement of your aspect instead of emitting a custom error message. However, this may be confusing for the user. 

As a rule of thumb, you should use eligibility to define for which declarations the aspect makes sense or not, and use error messages when the aspect makes sense on the declaration, but there is some contingency that prevents the aspect from being used. 

For details about reporting errors, see <xref:diagnostics>.

### Example 1

Adding a caching to a `void` method does not make sense and should be addressed with eligibility. However, the fact that your aspect does not support methods returning a collection is an implementation detail and should be reported using a custom error.

### Example 2

Adding a dependency injection aspect to an `int` or `string` field does not make sense and this condition should therefore be expressed using the eligibility API. However, the fact that your implementation of the aspect requires the field to be non-read-only is a contingency and should be reported as an error.

