---
uid: aspect-inheritance
---

# Applying aspects to derived types

Many aspects, such as `INotifyPropertyChanged` implementation or thread synchronization aspects, need to be _inherited_ from the base class to which the aspect is applied, to all derived classes. That is, if a base class has a `[NotifyPropertyChanged]` aspect that adds calls to `OnPropertyChanged` to all property setters, it is logical that the aspect also affects the property setters of the _derived_ classes.

This feature is called _aspect inheritance_. It is activated by adding the <xref:Metalama.Framework.Aspects.InheritableAttribute?text=[Inheritable]> custom attribute to the aspect class. When an aspect is marked as _inheritable_, its <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method is no longer called only for the direct target declaration of the aspect, but also for all derived declarations.

An aspect can be inherited along the following lines:

* from a base class to derived classes;
* from a base interface to derived interfaces;
* from an interface to all types implementing that interface;
* from a `virtual` or `abstract` member to its `override` members;
* from an interface member to its implementations;
* from a parameter of a `virtual` or `abstract` method to the corresponding parameter of all `override` methods;
* from a parameter of an interface member to the corresponding parameter of all its implementations.

## Example

The following type-level aspect is applied to a base class and is implicitly inherited by all derived classes.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/InheritedTypeLevel.cs name="Type-level inherited aspect"]


## Conditional inheritance

The <xref:Metalama.Framework.Aspects.InheritableAttribute?text=[Inheritable]> custom attribute causes _all_ instances of the aspect class to be inheritable, regardless of their fields or properties. If you want to make the inheritance decision dependent on fields or properties of the aspect, then your aspect must implement the <xref:Metalama.Framework.Aspects.IConditionallyInheritableAspect>.

Note that when the <xref:Metalama.Framework.Aspects.IConditionallyInheritableAspect> interface is implemented, the refactoring menu will always suggest adding the aspect to a declaration, even if the aspect is eligible for inheritance only on the target declaration.

## Cross-project inheritance

Aspect inheritance also works across project boundaries, that is, even when the base class is in a different project than the derived class.

To make this possible, the aspect instance in the project containing the base class is _serialized_ into a binary buffer and stored as a managed resource in the assembly. When compiling the project containing the derived class, the aspect is _deserialized_ from the binary buffer, and its <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method can be called.

Serialization is done using a custom formatter whose semantics are close to the legacy <xref:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter> of the now obsolete `[Serializable]`. To mark a field or property as non-serializable, use the <xref:Metalama.Framework.Serialization.NonCompileTimeSerializedAttribute> custom attribute.

## Eligibility of inherited aspects

The _eligibility_ of an aspect is a set of rules that define on which target declarations an aspect can be legitimately applied. For details, see <xref:eligibility>.

When an aspect is inherited, it has two sets of eligibility rules:
* the _normal_ eligibility rules define on which declarations the aspect can be expanded; typically this would _not_ include any abstract members;
* the _inheritance_ eligibility rules define on which declarations the aspect can be added _for inheritance_; typically this would include abstract members.

When an inherited aspect is added to a target that matches the inheritance eligibility rules but not the normal eligibility rules, an _abstract_ aspect instance is added to that target. That is, <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method is _not_ called for that target, but only for derived targets.

To define the eligibility rules that do not apply to the inheritance scenario, use the <xref:Metalama.Framework.Eligibility.IEligible`1.BuildEligibility*> method, use the <xref:Metalama.Framework.Eligibility.EligibilityExtensions.ExceptForInheritance*> method.

### Example

The following implementation of <xref:Metalama.Framework.Eligibility.IEligible`1.BuildEligibility*> specifies that the aspect will be applied abstractly when applied to an abstract method, that its <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method will not be invoked for the abstract method, but only for methods implementing the abstract method.

```cs
public override void BuildEligibility( IEligibilityBuilder<IMethod> builder )
{
    builder.ExceptForInheritance().MustNotBeAbstract();
}
```

