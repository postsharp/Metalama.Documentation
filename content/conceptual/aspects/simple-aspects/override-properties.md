---
uid: simple-override-property
level: 200
---

# Getting started: overriding fields and properties

# Overriding fields or properties

In <xref:simple-override-method>, you have learned how to change the implementation of a method with an aspect. You can do the same with fields and properties thanks to the  <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect> abstract class.

## The simple way: deriving the OverrideFieldOrPropertyAspect abstract class

1. Add Metalama the `Metalama.Framework` package to your project.

2. Create a new class derived from the <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect> abstract class. This class will be a custom attribute, so it is a good idea to name it with the `Attribute` suffix.

3. Implement the <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect.OverrideProperty> property in plain C#. The accessors of this property will serve as <xref:templates?text=templates> defining the way the aspect overrides the accessors of the hand-written field or property.
   - To insert code or expressions that depend on the target accessors of the aspect (such as the field or property name or type), use the <xref:Metalama.Framework.Aspects.meta> API.
   - Where the original implementation must be invoked, call the <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed> method.

4. The aspect is a custom attribute. To transform a field or property using the aspect, just add the aspect custom attribute to the field or property.

> [!WARNING]
> When you apply an aspect to a field, Metalama will automatically transform the field into a property. If the field is used by reference using `ref`, `out` and `in` keywords, it will result in a compile-time error.

### Example: An empty OverrideFieldOrPropertyAspect aspect

The next example shows an empty implementation of <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect> applied to a property and to a field.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/EmptyOverrideFieldOrProperty.cs name="Empty OverrideFieldOrProperty"]


## Getting or setting the underlying property

If you have only worked with methods so far, you may be already used to using the `meta.Proceed()` method in your template. This method also works in a property template: when called from the getter, it returns the field or property value; when called from the setter, it sets the field or property to the value of the `value` parameter.

If you need to get the property value from the setter, or if you need to set the property value to something else than the `value` parameter, you can do it by getting or setting the `meta.Target.FieldOrProperty.Value` property.



### Example: Trimming string fields and properties
In this aspect, you shall see how you can trim string values. There can be situations when there could be strict limits on the length of strings. The aspect demonstrates a case where code is trimmed, and the code is expected to be exactly 7 characters to be valid. 


[!metalama-sample ~/code/Metalama.Documentation.SampleCode.EnhanceProperties/Trimmed.cs name="Trimming string fields and properties"]

### Example: Turning string field and property value to upper case.
Let's say you have a class that is used to represent shipment details between two airports. 

Generally, airport services require the airport code in uppercases, but it is not always guaranteed to get the airport codes in uppercase. The following property aspect shows how you can change the case of the assigned airport code to Upper case.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.EnhanceProperties/UpperCase.cs name="Changing case to Upper case"]

> [!NOTE]
> Notice that `From` is a public field and `To` is a public property. They are deliberately kept that way to show that the aspect actually works on both because <xref:Metalama.Framework.Code.IFieldOrProperty> is used in the aspect. If you want to aspect to be applicable only on properties and not on fields you have to use <xref:Metalama.Framework.Code.IProperty> 

### Example: Formatting USA phone numbers 

Phone numbers are saved in many formats. However, it is required to have a normalization scheme for all phone numbers. The following property aspect shows how you can use a common format for the USA phone numbers,
like (XXX)-XXX-XXXX. 

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.EnhanceProperties/PhoneNumberUSA.cs name="Formatting strings like USA phones"]

