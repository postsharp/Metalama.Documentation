---
uid: aspect-inheritance
---

# Aspect Inheritance

Many aspects, such as `INotifyPropertyChanged` implementation or thread synchronization aspects, need to be _inherited_ from the base class to which the aspect is applied, to all derived classes. That is, if a base class has a `[NotifyPropertyChanged]` aspect that adds calls to `OnPropertyChanged` to all property setters, it is logical that the aspect also affects the property setters of the _derived_ classes.

This feature is called _aspect inheritance_. It is activated by adding the <xref:Metalama.Framework.Aspects.InheritedAttribute> custom attribute to the aspect class. When an aspect is marked as _inherited_, its  <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method is no longer only called for the direct target declaration of the aspect, but also for all derived declarations.

Aspect can be inherited along the following lines:

* from a base class to derived classes;
* from an interface to all classes implementing that interface;
* from a `virtual` member to ts `override` members;
* from an interface members to its implementations;
* from a parameter of a `virtual` method to the corresponding parameter of all `override` methods;
* from a parameter of an interface member to the corresponding parameter of all its implementations.

## Example

The following type-level aspect is applied to a base class and is implicitly inherited by all derived classes.

[!include[Type-level inherited aspect](../../code/Metalama.Documentation.SampleCode.AspectFramework/InheritedTypeLevel.cs)]


## Cross-project inheritance

Aspect inheritance also works across project boundaries, that is, even when the base class is in a different project than the derived class.

To make this possible, the aspect instance on in the project containing the base class is _serialized_ into a binary buffer, and stored as a managed resource in the assembly. When compiling the project containing the derived class, the aspect is _deserialized_ from the binary buffer, and its <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method can be called.

Serialization is done using a custom formatter whose semantics are close to the legacy <xref:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter> of the now obsolete `[Serializable]`. To mark a field or property as non-serializable, use the <xref:Metalama.Framework.Serialization.LamaNonSerializedAttribute> custom attribute.
