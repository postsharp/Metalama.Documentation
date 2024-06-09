---
uid: xaml-dependency-property
---

# XAML Dependency Property

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Xaml/DependencyProperties/Simple.cs]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Xaml/DependencyProperties/ReadOnly.cs]

## Default values

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Xaml/DependencyProperties/DefaultValue.cs]

## Contracts

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Xaml/DependencyProperties/Contract.cs]


## PropertyChanging, PropertyChanged hooks

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Xaml/DependencyProperties/OnPropertyChanged.cs]

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Xaml/DependencyProperties/OnPropertyChanging.cs]


### Valid PropertyChanging and PropertyChanged Method Signatures

The following signatures originate from the PostSharp implementation:

* `static void OnFooChanged()`
* `static void OnFooChanged(DependencyProperty property)`
* `static void OnFooChanged(TDeclaringType instance)`
* `static void OnFooChanged(DependencyProperty property, TDeclaringType instance)`
* `void OnFooChanged()`
* `void OnFooChanged(DependencyProperty property)`

where `TDeclaringType` is the declaring type of the target property, or `DependencyObject`, or `object`.

The following signatures are inspired by `ObservableProperty` from the Windows Community Toolkit:

* `void OnFooChanging(value_type value)`
* `void OnFooChanged(value_type value)`
* `void OnFooChanged(value_type oldValue, value_type newValue)`
* `void OnFooChanging<T>(T value)`
* `void OnFooChanged<T>(T value)`
* `void OnFooChanged<T>(T oldValue, T newValue)`

where `value_type` is any type assignable from the actual type of the target property.

Note that `oldValue` does not appear to fit the purely DependencyProperty-backed GetValue/SetValue/callbacks model for OnChanging, so `void OnNameChanging(value_type? oldValue, value_type? newValue)` is not supported.

### Valid ValidateValue Method Signatures

All the supported signatures originate from the PostSharp implementation:

* `static bool ValidatePropertyName(TPropertyType value)`
* `static bool ValidatePropertyName(DependencyProperty property, TPropertyType value)`
* `static bool ValidatePropertyName(TDeclaringType instance, TPropertyType value)`
* `static bool ValidatePropertyName(DependencyProperty property, TDeclaringType instance, TPropertyType value)`
* `bool ValidatePropertyName(TPropertyType value)`
* `bool ValidatePropertyName(DependencyProperty property, TPropertyType value)`

where `TDeclaringType` is the declaring type of the target property, or `DependencyObject`, or `object`, and
where `TPropertyType` is any type assignable from the actual type of the target property.
`TPropertyType` can also be a generic type parameter, in which case the method must have exactly one generic parameter. Note that support for the generic forms is implemented in PostSharp but not documented.



## Contracts

## NotifyPropertyChanged

