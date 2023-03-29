---
uid: template-dynamic-code
level: 200
---

# Generating run-time code


## Dynamic typing

Templates use the `dynamic` type to represent types unknown to the template developer. For instance, an aspect may not know in advance the return type of the methods to which it is applied. The return type is represented by the `dynamic` type.

```cs
dynamic? OverrideMethod() 
{ 
    return default;
}
```

All `dynamic` compile-time code is transformed into strongly-typed run-time code. When the template is expanded, `dynamic` variables are transformed into `var` variables. Therefore, all `dynamic` variables must be initialized.

In a template, it is not possible to generate code that uses `dynamic` typing at run time.

## Converting compile-time values to run-time values

You can use `meta.RunTime( expression )` to convert the result of a compile-time expression into a run-time value. The compile-time expression will be evaluated at compile time, and its value will be converted into _syntax_ that represents that value. Conversions are possible for the following compile-time types:

- Literals;
- Enum values;
- One-dimensional arrays;
- Tuples;
- Reflection objects: <xref:System.Type>, <xref:System.Reflection.MethodInfo>, <xref:System.Reflection.ConstructorInfo>, <xref:System.Reflection.EventInfo>, <xref:System.Reflection.PropertyInfo>, <xref:System.Reflection.FieldInfo>;
- <xref:System.Guid>;
- Generic collections: <xref:System.Collections.Generic.List`1> and <xref:System.Collections.Generic.Dictionary`2>;
- <xref:System.DateTime> and <xref:System.TimeSpan>.
- Immutable collections: <xref:System.Collections.Immutable.ImmutableArray`1> and <xref:System.Collections.Immutable.ImmutableDictionary`2>.
- Custom objects implementing the <xref:Metalama.Framework.Code.SyntaxBuilders.IExpressionBuilder> interface (see [Converting custom objects from compile-time to run-time values](#custom-conversion) for details).

### Example

The following aspect converts the following build-time values into a run-time expression: a `List<string>`, a `Guid`, and a `System.Type`.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/ConvertToRunTime.cs name="Dynamic"]

## Dynamic code

The `meta` API exposes some properties of `dynamic` type and some methods returning `dynamic` values. These members are compile-time, but they produce a _C# expression_ that can be used in the run-time code of the template. Because these members return a `dynamic` value, they can be used anywhere in your template. The code will not be validated when the template is compiled but when the template is applied.

For instance, `meta.This` returns a `dynamic` object that represents the expression `this`. Because `meta.This` is `dynamic`, you can write `meta.This._logger` in your template, which will translate to `this._logger`. This will work even if your template does not contain a member named `_logger` because `meta.This` returns a `dynamic`, therefore any field or method referenced on the right hand of the `meta.This` expression will not be validated when the template is compiled (or in the IDE), but when the template is _expanded_, in the context of a specific target declaration.

Here are a few examples of APIs that return a `dynamic`:

* Equivalents to the `this` or `base` keywords:
  * <xref:Metalama.Framework.Aspects.meta.This?text=meta.This>, equivalent to the `this` keyword, allows calling arbitrary _instance_ members of the target type.
  * <xref:Metalama.Framework.Aspects.meta.Base?text=meta.Base>, equivalent to the `base` keyword, allows calling arbitrary _instance_ members of the _base_ of the target type.
  * <xref:Metalama.Framework.Aspects.meta.ThisType?text=meta.ThisType> allows calling arbitrary _static_ members of the target type.
  * <xref:Metalama.Framework.Aspects.meta.BaseType?text=meta.BaseType>, allows calling arbitrary _static_ members of the _base_ of the target type.
* <xref:Metalama.Framework.Code.IExpression.Value?text=IExpression.Value> allows to get or set the value of a compile-time expression in run time code. It is implemented, for instance, by:
  * `meta.Target.Field.Value`, `meta.Target.Property.Value` or `meta.Target.FieldOrProperty.Value` allow to get or set the value of the target field or property.
  * `meta.Target.Parameter.Value` allows to get or set the value of the target parameter.
  * `meta.Target.Method.Parameters[*].Value` allows you to get or set the value of a target method's parameter.
  * <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.ToExpression*?text=ExpressionFactory.ToExpression(*).Value> allows to get or set the value of an arbitrary <xref:Metalama.Framework.Code.IField>, <xref:Metalama.Framework.Code.IProperty>, or <xref:Metalama.Framework.Code.IParameter>


### Abilities

You can also write any dynamic code on the left of a dynamic expression. As with any dynamically typed code, the syntax of the code is validated, but not the existence of the invoked members.

```cs
// Translates into: this.OnPropertyChanged( "X" );
meta.This.OnPropertyChanged( "X" );
```

Some more complex expressions also expose `dynamic`. For instance, `meta.Target.Parameters["p"].Value` refers to the `p` parameter of the target method and compiles simply into the syntax `p`. 

```cs
// Translates into: Console.WriteLine( "p = " + p );
Console.WriteLine( "p = " + meta.Target.Parameters["p"].Value );
```

When the expression is writable, the `dynamic` member can be used on the right hand of an assignment:

```cs
// Translates into: this.MyProperty = 5;
meta.Property.Value = 5;
```

You can combine dynamic code and compile-time expressions:

```cs
// Translated into: this.OnPropertyChanged( "MyProperty" );
meta.This.OnPropertyChanged( meta.Property.Name );
```

### Example 
In the following aspect, the logging aspect uses `meta.This`, which returns a `dynamic` object, to access the type being enhanced. The aspect assumes that the target type defines a field named `_logger`he and that the type of this field has a method named `WriteLine`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/DynamicTrivial.cs name="meta.This"]

## Generating calls to the code model

When you have a <xref:Metalama.Framework.Code> representation of a declaration, you may want to access it from your generated run-time code. You can do this by using the `Invokers` property exposed by the <xref:Metalama.Framework.Code.IMethod>, <xref:Metalama.Framework.Code.IFieldOrProperty> or <xref:Metalama.Framework.Code.IEvent> interfaces.

For details, see the documentation of the <xref:Metalama.Framework.Code.Invokers> namespace.

### Example

The following example is a variation of the previous one. The aspect no longer assumes the logger field is named `_logger`. Instead, it looks for any field of type `TextWriter`. Because it does not know the field's name upfront, the aspect must use `Invokers.Final.GetValue` to get an expression allowing it to access the field. `Invokers.Final.GetValue` returns a `dynamic` object.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/DynamicCodeModel.cs name="Invokers"]


## Generating run-time arrays

The first way to generate a run-time array is to declare a variable of array type and to use a statement to set each element, for instance:

```cs
var args = new object[2];
args[0] = "a";
args[1] = DateTime.Now;
MyRunTimeMethod( args );
```

To generate an array of a variable length, you can use the <xref:Metalama.Framework.Code.SyntaxBuilders.ArrayBuilder> class.

For instance:

```cs
var arrayBuilder = new ArrayBuilder();
arrayBuilder.Add( "a" );
arrayBuilder.Add( DateTime.Now );
MyRunTimeMethod( arrayBuilder.ToValue() );
```

This will generate the following code:

```cs
MyRunTimeMethod( new object[] { "a", DateTime.Now });
```

## Generating interpolated strings

Instead of generating a string as an array separately and using `string.Format`, you can generate an interpolated string using the <xref:Metalama.Framework.Code.SyntaxBuilders.InterpolatedStringBuilder> class.

The following example shows how an <xref:Metalama.Framework.Code.SyntaxBuilders.InterpolatedStringBuilder> can be used to implement the `ToString` method automatically.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/ToString.cs name="ToString"]
 
> [!div id="parsing" class="anchor"]

## Parsing C# code

Sometimes it is easier to generate the run-time code as a simple text instead of using a complex meta API. If you want to use C# code represented as a `string` in your code, use the <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.Parse*?text=ExpressionFactory.Parse> method. This method returns an <xref:Metalama.Framework.Code.IExpression>, which is a compile-time object that you can use anywhere in compile-time code. The <xref:Metalama.Framework.Code.IExpression> interface exposes the run-time expression in the <xref:Metalama.Framework.Code.IExpression.Value> property.


>[!NOTE] 
> The string expression is inserted _as is_ without validation or transformation. Always specify the full namespace of any declaration used in a text expression.

>[!NOTE]
> Instead of the traditional `StringBuilder` you can use <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder> to build an expression. It offers convenient methods like `AppendLiteral`, `AppendTypeName` or `AppendExpression`. To add a statement to the generated code, use <xref:Metalama.Framework.Code.SyntaxBuilders.StatementBuilder> to create the statement and then `meta.InsertStatement` from the template at the place where the statement should be inserted.

### Example

The `_logger` field is accessed through a parsed expression in the following example.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/ParseExpression.cs name="ParseExpression"]

## Capturing run-time expressions into compile-time objects

If you want to manipulate a run-time expression as a compile-time object, use the <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.Capture*?textExpressionFactory.Capture> method. This allows you to have expressions that depend on compile-time conditions and control flows. The <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.Capture*?textExpressionFactory.Capture> method returns an <xref:Metalama.Framework.Code.IExpression>, the same interface returned by <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.Parse*>. The <xref:Metalama.Framework.Code.IExpression> is a compile-time object you can use anywhere in compile-time code. It exposes the run-time expression in the <xref:Metalama.Framework.Code.IExpression.Value> property.

The following example is taken from the clone aspect. It declares a local variable named `clone`, but the expression assigned to the variable depends on whether the `Clone` method is an override.

```cs
IExpression baseCall;

if (meta.Target.Method.IsOverride)
{
    ExpressionFactory.Capture(meta.Base.Clone(), out baseCall);
}
else
{
    ExpressionFactory.Capture(meta.Base.MemberwiseClone(), out baseCall);
}

// Define a local variable of the same type as the target type.
var clone = meta.Cast(meta.Target.Type, baseCall);
```

[comment]: # (TODO: Reference code snippets from the file by marked region)

This template generates either `var clone = (TargetType) base.Clone();` or `var clone = (TargetType) this.MemberwiseClone();`.

> [!NOTE] 
> The weird syntax of Capture, which gives the result as an `out` parameter instead of a return value, is due to the viral nature of `dynamic`. In C#, the return type of `ExpressionFactory.Caputre(someDynamicExpression)` would be `dynamic`, while Metalama would require it to be `IExpression`.

## Converting custom objects from compile-time to run-time values

> [!div id="custom-conversion" class="anchor"]

You can have classes that exist both at compile and run time. To allow Metalama to convert a compile-time value to a run-time value, your class must implement the <xref:Metalama.Framework.Code.SyntaxBuilders.IExpressionBuilder> interface. The <xref:Metalama.Framework.Code.SyntaxBuilders.IExpressionBuilder.ToExpression> method must generate a C# expression that, when evaluated, returns a value that is structurally equivalent to the current value. Note that your implementation of <xref:Metalama.Framework.Code.SyntaxBuilders.IExpressionBuilder> is _not_ a template, so you will have to use the <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder> class to generate your code.

### Example

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/CustomSyntaxSerializer.cs name="Custom Syntax Serializer"]
