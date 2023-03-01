---
uid: simple-override-property
level: 200
---

# Getting started: overriding fields and properties

In <xref:simple-override-method>, you have learned how to change the implementation of a method with an aspect. You can do the same with fields and properties thanks to the <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect> abstract class.

## Overriding a field or property

To create an aspect that can override a field or a property, the simplest approach is the following.

1. Add Metalama the `Metalama.Framework` package to your project.

2. Create a new class derived from the <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect> abstract class. This class will be a custom attribute, so it is a good idea to name it with the `Attribute` suffix.

3. Implement the <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect.OverrideProperty> property in plain C#. To call the original implementation, call <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed>. 

4. The aspect is a custom attribute. To transform a field or property using the aspect, just add the aspect custom attribute to the field or property.

> [!WARNING]
> When you apply an aspect to a field, Metalama will automatically transform the field into a property. If the field is used by reference using `ref`, `out` and `in` keywords, it will result in a compile-time error.

### Example: an empty OverrideFieldOrPropertyAspect aspect

The next example shows an empty implementation of <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect> applied to a property and to a field.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/EmptyOverrideFieldOrProperty.cs name="Empty OverrideFieldOrProperty"]

This aspect does not do anything useful, but, as you can see, it transforms the field into a property.


## Getting or setting the underlying property

If you have only worked with methods so far, you may be already used to using the `meta.Proceed()` method in your template. This method also works in a property template: when called from the getter, it returns the field or property value; when called from the setter, it sets the field or property to the value of the `value` parameter.

If you need to get the property value from the setter, or if you need to set the property value to something else than the `value` parameter, you can do it by getting or setting the <xref:Metalama.Framework.Code.IExpression.Value?text=meta.Target.FieldOrProperty.Value> property.


### Example: trimming strings

In this aspect, you shall see how you can trim whitespace before and after string values before they are assigned to the field or property.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.EnhanceProperties/Trimmed.cs name="Trimming string fields and properties"]

The aspect does not need to modify the getter, so it only calls `meta.Proceed()` and Metalama replaces this call by the original implementation of the property. We could have written `get => meta.Target.PropertyOrField.Value` instead, with the same effect.

The setter is modified to call the `Trim` method on the input `value`. The most shortest and simplest code is `set => meta.Target.PropertyOrField.Value = value?.Trim`. Alternatively, we could have written the following code.

```cs
set 
{
    value = value?.Trim();
    meta.Proceed();
}
```


### Example: turning the value to upper case

The following example is very similar to the previous one: instead of trimming a string, we normalize it to upper case.

We apply the aspect to a class that represents a shipment between two airports.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.EnhanceProperties/UpperCase.cs name="Changing case to Upper case"]

Note that, in this example, `From` is a public field and `To` is a public property. They are deliberately kept that way to show that the aspect actually works on both because <xref:Metalama.Framework.Code.IFieldOrProperty> is used in the aspect. If you want to aspect to be applicable only on properties and not on fields you have to use <xref:Metalama.Framework.Code.IProperty> 

