---
uid: eligibility
---

# Defining the Eligibility of Aspects

Most of the aspects are designed and implemented for specific kinds of target declarations. For instance, you may decide that your caching aspect will not support `void` methods or methods with an `out` or `ref` parameter. It is important, as the author of the aspect, to make sure that the user of your aspect applies it only to the declarations that you expect. Otherwise, at best, the aspect will cause build errors and confuse the user or at worse, the run-time behavior of your aspect will be incorrect.

## Benefits

Defining the eligibility of an aspect has the following benefits:

* **Predictable behavior**. Applying an aspect to a declaration for which it was not designed or tested can be a very confusing experience for your users because of error messages they may not understand. It is your responsibility, as the author of the aspect, to ensure that using your aspect is easy and predictable.
* **Standard error messages**. All eligibility error messages are standard. It is easier for the aspect users.
* **Relevant suggestions in the IDE**.  The IDE will only propose code action in the refactoring menu for eligible declarations.


## Defining eligibility

To define the eligibility of your aspect, implement or override the <xref:Metalama.Framework.Eligibility.IEligible`1.BuildEligibility*> method of the aspect. Use the `builder` parameter, of type <xref:Metalama.Framework.Eligibility.IEligibilityBuilder`1>, to specify the requirements of your aspect. For instance, use <xref:Metalama.Framework.Eligibility.EligibilityExtensions.MustNotBeAbstract*?text=builder.MustNotBeAbstract()> to require a non-abstract method.

A number of predefined eligibility conditions are implemented by the <xref:Metalama.Framework.Eligibility.EligibilityExtensions> static class. You can add a custom eligibility condition by calling <xref:Metalama.Framework.Eligibility.EligibilityExtensions.MustSatisfy*> and by providing your own lambda expression. This method also expects the user-readable string that should be included in the error message when the user attempts to add the aspect to an ineligible declaration.

>[!NOTE] 
> Your implementation of <xref:Metalama.Framework.Eligibility.IEligible`1.BuildEligibility*> must not reference any instance member of the class. Indeed, this method is called on an instance obtained using `FormatterServices.GetUninitializedObject` that is, _without invoking the class constructor_.

### Example

[!include[Eligibility](../../code/Metalama.Documentation.SampleCode.AspectFramework/Eligibility.cs)]

## When to emit custom errors instead?

It may be tempting to add an eligibility condition for every requirement of your aspect instead of emitting a custom error message. However, this may be confusing for the user. 

As a rule of thumb, you should use eligibility to define those declarations for which it makes sense to either apply the aspect or not, and use error messages when the aspect makes sense on the declaration, but there is some contingency that prevents the aspect from being used. 

For details about reporting errors, see <xref:diagnostics>.

### Example 1

Adding a caching aspect to a `void` method does not make sense and should be addressed with eligibility. However, the fact that your aspect does not support methods returning a collection is an implementation detail and should be reported using a custom error.

### Example 2

Adding a dependency injection aspect to an `int` or `string` field does not make sense and this condition should therefore be expressed using the eligibility API. However, the fact that your implementation of the aspect requires the field to be non-read-only is a contingency and should be reported as an error.

### Example

The following example expands the previous one, reporting custom errors when the target class does not define a field `logger` of type `TextWriter`.

[!include[Eligibility and Validation](../../code/Metalama.Documentation.SampleCode.AspectFramework/EligibilityAndValidation.cs)]
