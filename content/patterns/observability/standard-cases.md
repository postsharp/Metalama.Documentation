---
uid: observability-standard-cases
---


The `Metalama.Patterns.Observability` package supports the following cases:

* Automatic properties;
* Explicitly-implemented properties whose getter references:
    - fields,
    - other properties,
    - non-virtual instance methods;
* Child objects, i.e. properties whose getter reference properties of another object, called child object, stored in a field or an automatic property of the current type (if this child object is itself observable);
* Class inheritance.

In this section, we present the code generation patterns of each case supported case.

### Automatic properties

The code pattern for automatic properties is unsurprising. The automatic property is turned into a field-backed property. A new `OnPropertyChanged` method is introduced unless it already exists.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Observability/Simple.cs]

### Explicitly-implemented properties

The <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]> aspect analyzes the dependencies between all properties in the type, and calls the `OnPropertyChanged` method
Computed properties (also called read-only properties)

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Observability/ComputedProperty.cs]

### Field-backed properties

Mutable fields are changed into properties of the same name, and the setter of the new property calls `OnPropertyChanged` for all relevant dependent properties.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Observability/FieldBacked.cs]


### Derived types

When a derived type has a property whose getter references a property of the _base_ type, the <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]> aspect overrides the `OnPropertyChanged` method, filters the property name, and recursively calls the `OnPropertyChanged` method for all dependent properties.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Observability/Derived.cs]

### Child objects

It is quite common in MVVM architectures that a property of the ViewModel depends of a property of Model object, which itself is a field or property of the ViewModel object. 

When a property getter references a property of an object stored in another field or property (called _child object_ in this context), the <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]> aspect generates a `SubscribeTo` method for the property containing the child object, and this method subscribes to the <xref:System.ComponentModel.INotifyPropertyChanged.PropertyChanged> event of the child object.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Observability/ChildObject.cs tabs="target"]

### Child objects and derived types

Things get more complex when a type depends on a property of a property of the _base_ type. To support this case, the <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]> aspect generates two additional methods in the base type: `OnChildPropertyChanged` and `OnObservablePropertyChanged`.

The `OnChildPropertyChanged` method is called when a property of a child object has changed. Its role is to avoid derived classes from having to monitor the <xref:System.ComponentModel.INotifyPropertyChanged.PropertyChanged> event for their own needs. Instead, they can just override the method and add their own logic. This method is only generated if the base type has itself a dependency on a child property.

The `OnObservablePropertyChanged` method is called when a property implementing `INotifyPropertyChanged` has changed. The value of this method, compared to the standard `OnPropertyChanged`, is to supply the _old_ value of the property to allow child classes to subscribe and unsubscribe from the PropertyChanged event. When the `OnChildPropertyChanged` is generated, the `OnObservablePropertyChanged` may be redundant.

Both methods are annotated with the <xref:Metalama.Patterns.Observability.ObservedExpressionsAttribute?text=[ObservedExpressions]>. They form a contract between the base type and the derived types and inform the derived types for which properties the methods will be invoked for.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Observability/ChildObject_Derived.cs  tabs="target"]