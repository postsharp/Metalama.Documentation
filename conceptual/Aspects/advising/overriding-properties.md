---
uid: overriding-fields-or-properties
---
# Overriding Fields or Properties

In <xref:overriding-methods>, you have learned how to wrap an existing method with additional, automatically-generated model. You can do the same with fields and properties thanks to the  <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect> abstract class. 

## The simple way: deriving the OverrideFieldOrPropertyAspect abstract class

1. Add Metalama to your project as described in <xref:installing>.
   
2. Create a new class derived from the <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect> abstract class. This class will be a custom attribute, so it is a good idea to name it with the `Attribute` suffix.

3. Implement the <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect.OverrideProperty> property in plain C#. The accessors of this property will serve as <xref:templates?text=templates> defining the way the aspect overrides the accessors of the hand-written field or property.
   - To insert code or expressions that depend on the target accessors of the aspect (such as the field or property name or type), use the <xref:Metalama.Framework.Aspects.meta> API.
   - Where the original implementation must be invoked, call the <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed> method.

4. The aspect is a custom attribute. To transform a field or property using the aspect, just add the aspect custom attribute to the field or property.

> [!WARNING]
> When you apply an aspect to a field, Metalama will automatically transform the field into a property. If the field is used by reference using `ref`, `out` and `in` keywords, it will result in a compile-time error.

[comment]: # (TODO: #28909)

### Example: An empty OverrideFieldOrPropertyAspect aspect

The next example shows an empty implementation of <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect> applied to a property and to a field.

[!include[Empty OverrideFieldOrProperty](../../../code/Metalama.Documentation.SampleCode.AspectFramework/EmptyOverrideFieldOrProperty.cs)]


## Getting or setting the underlying property

If you have only worked with methods so far, you may be already used to using the `meta.Proceed()` method in your template. This method also works in a property template: when called from the getter, it returns the field or property value; when called from the setter, it sets the field or property to the value of the `value` parameter.

If you need to get the property value from the setter, or if you need to set the property value to something else than the `value` parameter, you can do it by getting or setting the `meta.Target.FieldOrProperty.Value` property.

[comment]: # (TODO: example)

## Accessing the metadata of the overridden field or property

The metadata of the field or property being overridden are available from the template accessors on the <xref:Metalama.Framework.Aspects.IMetaTarget.FieldOrProperty?text=meta.Target.FieldOrProperty> property . This property gives you all information about the name, type, parameters and custom attributes of the field or property. For instance, the member name is available on `meta.Target.FieldOrProperty.Name` and its type on `meta.Target.FieldOrProperty.Type`.

The _value_ of the field or property is available on <xref:Metalama.Framework.Aspects.IMetaTarget.FieldOrProperty?text=meta.Target.FieldOrProperty.Value>. Your aspect can read and write this property, as long as the field or the property is writable. To determine if the field is `readonly` or if the property has a `set` accessor, you can use <xref:Metalama.Framework.Code.IFieldOrPropertyOrIndexer.Writeability?meta.Target.FieldOrProperty.Writeability>.

### Example: Resolving dependencies on the fly

The following example is a simplified implementation of the service locator pattern.

The `Import` aspect overrides the getter of a property to make a call to a global service locator. The type of the service is determined from the type of the field or property, using `meta.Target.FieldOrProperty.Type`.
The dependency is not stored, so the service locator must be called every time the property is evaluated.

[!include[Import Service](../../../code/Metalama.Documentation.SampleCode.AspectFramework/GlobalImport.cs)]

### Example: Resolving dependencies on the fly and storing the result

This example builds over the previous one, but the dependency is stored in the field or property after it has been retrieved from the service provider for the first time.

[!include[Import Service](../../../code/Metalama.Documentation.SampleCode.AspectFramework/GlobalImportWithSetter.cs)]

## Overriding several fields or properties from the same aspect

Just like for methods, to override one or more fields or properties from a single aspect, your aspect needs to implement the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method exposed on `builder.Advice`. Your implementation must then call the <xref: Metalama.Framework.Advising.IAdviceFactory.OverrideFieldOrProperty*> method.

There are two overloads of this method:

* One overload accepts a _property_ template.
* The second overload accepts one or two _accessor_ templates, i.e. one template _method_ for the getter and/or one other method for the setter.

### Using a property template

The _first argument_ of `OverrideFieldOrProperty` is the <xref:Metalama.Framework.Code.IFieldOrProperty> that you want to override. This field or property must be in the type being targeted by the current aspect instance.

The _second argument_ of `OverrideFieldOrProperty` is the name of the template property. This property must exist in the aspect class and, additionally:

* the template property must be annotated with the `[Template]` attribute,
* the template property must be of type `dynamic?` (_dynamically-typed_ template), or a type that is compatible with the type of the overridden property (_strongly-typed_ template).
* the template property can have a setter, a getter, or both. If one accessor is not specified in the template, the corresponding accessor in the target code will not be overridden.


#### Example: registry-backed class

The following aspect overrides properties so that they are written to and read from the Windows registry.

[!include[Registry Storage](../../../code/Metalama.Documentation.SampleCode.AspectFramework/RegistryStorage.cs)]


#### Example: string normalization

This example illustrates a strongly-typed property template with a single accessor that uses the `meta.Target.FieldOrProperty.Value` expression to access the underlying field or property.

The following aspect can be applied to fields of properties of type `string`. It overrides the setter to trim and lower case the assigned value. 

[!include[Normalize](../../../code/Metalama.Documentation.SampleCode.AspectFramework/Normalize.cs)]

### Using an accessor template


Advising fields or properties with the `OverrideFieldOrProperty` has the following limitations over the use of `OverrideAccessors`:

* You cannot choose a template for each accessor separately.
* You cannot have generic templates.  (Not yet implemented in `Overrideccessors` anyway.)

To alleviate these limitations, you can use the method <xref:Metalama.Framework.Advising.IAdviceFactory.Override*> and provide one or two method templates: a getter template and/or a setter template.

The templates must fulfill the following conditions:

* Both templates must be annotated with the `[Template]` attribute.
* The getter template must be of signature `T Getter()`, where `T` is either `dynamic` or a type compatible with the target field or property.
* The setter template must be of signature `void Setter(T value)`, where the name `value` of the first parameter is mandatory.

[comment]: # (TODO: example)
