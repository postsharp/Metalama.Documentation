---
uid: template-dynamic-code
---

# Dynamically generating run-time code


## Dynamic typing

Templates use the `dynamic` type to represent types that are unknown by the developer of the template. For instance, an aspect may not know in advance the return type of the methods to which it is applied. The return type is represented by the `dynamic` type.

```cs
dynamic? OverrideMethod() 
{ 
    return default;
}
```

All `dynamic` compile-time code is transformed into strongly-typed run-time code. When the template is expanded, `dynamic` variables are transformed into `var` variables. Therefore, all `dynamic` variables must be initialized.

In a template, it is not possible to generate code that uses `dynamic` typing at run time.

## Converting compile-time values to run-time values

You can use `meta.RunTime( expression )` to convert the result of a compile-time expression into a run-time value. The compile-time expression will be evaluated at compile time, and its result will be converted into _syntax_ that represents that value. Conversions are possible for the following compile-time types:

- Literals;
- Enum values;
- One-dimensional arrays;
- Tuples;
- Reflection objects: <xref:System.Type>, <xref:System.Reflection.MethodInfo>, <xref:System.Reflection.ConstructorInfo>, <xref:System.Reflection.EventInfo>, <xref:System.Reflection.PropertyInfo>, <xref:System.Reflection.FieldInfo>;
- <xref:System.Guid>;
- Generic collections: <xref:System.Collections.Generic.List`1> and <xref:System.Collections.Generic.Dictionary`2>;
- <xref:System.DateTime> and <xref:System.TimeSpan>.

### Example

[!include[Dynamic](../../../code/Metalama.Documentation.SampleCode.AspectFramework/ConvertToRunTime.cs)]

## Dynamic code

The `meta` API exposes some properties of `dynamic` type and some methods returning `dynamic` values. These members are compile-time, but their value represents a _declaration_ that you can dynamically read at run time.
In the case of writable properties, it is also possible to set the value.

Dynamic values are a bit _magic_ because their compile-time value translates into _syntax_ that is injected in the transformed code.

For instance, `meta.Target.Parameters["p"].Value` refers to `p` parameter of the target method and compiles simply into the syntax `p`. It is possible to read this parameter and, if this is an `out` or `ref` parameter, it is also possible to write it.

```cs
// Translates into: Console.WriteLine( "p = " + p );
Console.WriteLine( "p = " + meta.Target.Parameters["p"].Value );


// Translates into: this.MyProperty = 5;
meta.Property.Value = 5;
```

You can also write dynamic code on the left of a dynamic expression:

```cs
// Translates into: this.OnPropertyChanged( "X" );
meta.This.OnPropertyChanged( "X" );
```

You can combine dynamic code and compile-time expressions:

```cs
// Translated into: this.OnPropertyChanged( "MyProperty" );
meta.This.OnPropertyChanged( meta.Property.Name );
```

### Example 
In the following aspect, the logging aspect uses `meta.This`, which returns a `dynamic` object, to access the type being enhanced. The aspect assumes that the target type defines a field named `_logger`, and that the type of this field has a method named `WriteLine`.

[!include[meta.This](../../../code/Metalama.Documentation.SampleCode.AspectFramework/DynamicTrivial.cs)]

## Generating calls to the code model

When you have a <xref:Metalama.Framework.Code> representation of a declaration, you may want to access it from your generated run-time code. You can do this by using the `Invokers` property exposed by the <xref:Metalama.Framework.Code.IMethod>, <xref:Metalama.Framework.Code.IFieldOrProperty> or <xref:Metalama.Framework.Code.IEvent> interfaces.

For details, see the documentation of the <xref:Metalama.Framework.Code.Invokers> namespace.

### Example

The following example is a variation of the previous one. The aspect no longer assumes that the logger field is named `_logger`. Instead, it looks for any field of type `TextWriter`. Because it does not know the name of the field upfront, the aspect must use `Invokers.Final.GetValue` to get an expression that allows it to access the field. `Invokers.Final.GetValue` returns a `dynamic` object.

[!include[Invokers](../../../code/Metalama.Documentation.SampleCode.AspectFramework/DynamicCodeModel.cs)]


## Generating run-time arrays

The first way to generate run-time array is to declare a variable of array type and to use a statement to set each element, for instance:

```cs
var args = new object[2];
args[0] = "a";
args[1] = DateTime.Now;
MyRunTimeMethod( args );
```

If you want to generate an array as a single-line expression, you can use the <xref:Metalama.Framework.Code.SyntaxBuilders.ArrayBuilder> class.

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

The following example shows how an <xref:Metalama.Framework.Code.SyntaxBuilders.InterpolatedStringBuilder> can be used to automatically implement the `ToString` method.

[!include[ToString](../../../code/Metalama.Documentation.SampleCode.AspectFramework/ToString.cs)]
 
> [!div id="parsing" class="anchor"]

## Parsing C# code

Sometimes it is easier to generate the run-time code as a simple text instead of using a complex meta API. If you want to use C# code represented as a `string` in your code, you can do it using the <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.Parse*?text=ExpressionFactory.Parse> method. This method returns an <xref:Metalama.Framework.Code.IExpression>, which is a compile-time object that you can use anywhere in compile-time code. The <xref:Metalama.Framework.Code.IExpression> interface exposes the run-time expression in the <xref:Metalama.Framework.Code.IExpression.Value> property.


>[!NOTE] 
> The string expression is inserted _as is_ without any validation or transformation. Always specify the full namespace of any declaration used in a text expression.

>[!NOTE]
> Instead of the traditional `StringBuilder`, you can use <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder> to build an expression. It offers convenient methods like `AppendLiteral`, `AppendTypeName` or `AppendExpression`. To add a statement to the generated code, use `StatementBuilder` to create the statement and then `meta.InsertStatement` from the template at the place where the statement should be inserted.

### Example

In the following example, the `_logger` field is accessed through a parsed expression.

[!include[ParseExpression](../../../code/Metalama.Documentation.SampleCode.AspectFramework/ParseExpression.cs)]

## Capturing run-time expressions into compile-time objects

If you want to manipulate a run-time expression as a compile-time object, you can do it using the <xref:Metalama.Framework.Aspects.meta.DefineExpression*?text=meta.DefineExpression> method. This allows you to have expressions that depend on compile-time conditions and control flows. The <xref:Metalama.Framework.Aspects.meta.DefineExpression*> method returns an <xref:Metalama.Framework.Code.IExpression>, the same interface returned by <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionFactory.Parse*>. The <xref:Metalama.Framework.Code.IExpression> is a compile-time object that you can use anywhere in compile-time code. It exposes the run-time expression in the <xref:Metalama.Framework.Code.IExpression.Value> property.

The following example is taken from the clone aspect. It declares a local variable named `clone`, but the expression assigned to the variable depends on whether the `Clone` method is an override.

```cs
IExpression baseCall;

if (meta.Target.Method.IsOverride)
{
    meta.DefineExpression(meta.Base.Clone(), out baseCall);
}
else
{
    meta.DefineExpression(meta.Base.MemberwiseClone(), out baseCall);
}

// Define a local variable of the same type as the target type.
var clone = meta.Cast(meta.Target.Type, baseCall);
```

[comment]: # (TODO: Reference code snippets from the file by marked region)

This template generates either `var clone = (TargetType) base.Clone();` or `var clone = (TargetType) this.MemberwiseClone();`.

## Converting custom objects from compile-time to run-time values

You can have classes that exist both at compile and run time. To allow Metalama to convert a compile-time value to a run-time value, your class must implement the <xref:Metalama.Framework.Code.SyntaxBuilders.IExpressionBuilder> interface. The <xref:Metalama.Framework.Code.SyntaxBuilders.IExpressionBuilder.ToExpression> method must generate a C# expression that, when evaluated, returns a value that is structurally equivalent to the current value. Note that your implementation of <xref:Metalama.Framework.Code.SyntaxBuilders.IExpressionBuilder> is _not_ a template, so you will have to use the <xref:Metalama.Framework.Code.SyntaxBuilders.ExpressionBuilder> class to generate your code.

### Example

[!include[Custom Syntax Serializer](../../../code/Metalama.Documentation.SampleCode.AspectFramework/CustomSyntaxSerializer.cs)]
