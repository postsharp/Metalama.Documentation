---
uid: templates
---
# Writing code templates

The specificity of a tool like Caravela, compared to simple code generation APIs, is that Caravela is able to _modify existing_ code, not only generate new code. Instead of giving you access to the syntax tree, which is extremely complex and error-prone (and you can still do it anyway with <xref:sdk> if you feel brave), Caravela lets you express code transformations in plain C# using a template language named _Caravela Template Language_.

You can compare Caravela Template Language to Razor. Razor allows you to create dynamic web pages by mixing server-side C# code and client-side HTML. With Caravela Template Language, you have _compile-time_ and _run-time_ code and, basically, the compile-time code generates the run-time code. The difference with Razor is that in Caravela both the compile-time and run-time code are the same language: C#. Caravela interprets every expression or statement in a template as having _either_ run-time scope _or_ compile-time scope. Compile-time expressions are generally initiated by calls to the <xref:Caravela.Framework.Aspects.meta> API.

## Initial example

Before moving forward, let's illustrate this concept with an example. The next aspect writes text to the console before and after the execution of a method, but special care is taken for `out` parameters and `void` methods. This is achieved by a conditional compile-time logic which generates simple run-time code. Compile-time code is highlighted <span class="caravelaClassification_CompileTime">differently</span>, so you can see which part of the code executes at compile time and which executes at run time.

> [!NOTE]
> To benefit from syntax highlighting in Visual Studio, install the [PostSharp "Caravela" Tools for Visual Studio](https://marketplace.visualstudio.com/items?itemName=PostSharpTechnologies.caravela).

[!include[Simple Logging](../../code/Caravela.Documentation.SampleCode.AspectFramework/LogParameters.cs)]

***

## Writing compile-time code

Compile-time expressions are expressions that either contain a call to a compile-time method, or contain a reference to a compile-time local variable or a compile-time aspect member. Compile-time expressions are executed at compile time, when the aspect is applied to a target.

Compile-time statements are statements, such as `if`, `foreach` or `meta.DebugBreak();`, that are executed at compile time.


### meta API

The entry point of the compile-time API is the <xref:Caravela.Framework.Aspects.meta> static class. The name of this class is intentionally lower case to convey the sentiment that it is something unusual and gives access to some kind of magic. The <xref:Caravela.Framework.Aspects.meta> class is the entry point to the meta model and the members of this class can be invoked only in the context of a template.

The <xref:Caravela.Framework.Aspects.meta> static class exposes to the following members:

- The <xref:Caravela.Framework.Aspects.meta.Proceed?text=meta.Proceed> method invokes the target method or accessor being intercepted, which can be either the next aspect applied on the same target or the target source implementation itself.
- The <xref:Caravela.Framework.Aspects.meta.Target?text=meta.Target> property gives access to the declaration to which the template is applied.
- The <xref:Caravela.Framework.Aspects.IMetaTarget.Parameters?text=meta.Target.Parameters> property gives access to the current method or accessor parameters.
- The <xref:Caravela.Framework.Aspects.meta.This?text=meta.This> property represents the `this` instance. Together with <xref:Caravela.Framework.Aspects.meta.Base?text=meta.Base>, <xref:Caravela.Framework.Aspects.meta.ThisStatic?text=meta.ThisStatic>, and <xref:Caravela.Framework.Aspects.meta.BaseStatic?text=meta.BaseStatic> properties, it allows your template to access members of the target class using dynamic code (see below).
- The <xref:Caravela.Framework.Aspects.meta.Tags?text=meta.Tags> property gives access to an arbitrary dictionary that has been passed to the advice factory method.
-  The <xref:Caravela.Framework.Aspects.meta.CompileTime*?text=meta.CompileTime> method coerces a neutral expression into a compile-time expression.
- <xref:Caravela.Framework.Aspects.meta.RunTime*?text=meta.RunTime> method converts the result of a compile-time expression into a run-time value (see below).

### Compile-time local variables

Local variables are run-time by default. To declare a compile-time local variable, you must initialize it to a compile-time value. If you need to initialize the compile-time variable to a literal value such as `0` or `"text"`, use the `meta.CompileTime` method to convert the literal into a compile-time value.

Examples:

- In `var i = 0`, `i` is a run-time variable.
- In `var i = meta.CompileTime(0)`, `i` is a compile-time variable.
- In `var parameters = meta.Target.Parameters`, `parameters` is compile-time variable.

> [!NOTE]
> It is not allowed to assign a compile-time variable from a block whose execution depends on a run-time condition, including a run-time `if`, `else`, `for`, `foreach`, `while`, - a `catch` or `finally`.

### Aspect members

Aspect members are compile-time and can be accessed from templates. For instance, an aspect custom attribute can define a property that can be set when when the custom attribute is applied to a target declaration and then read from the aspect compile-time code.

There are a few exceptions to this rule:

- aspect members whose signature contain a run-time-only type cannot be accessed from a template.
- aspect members annotated with the `[Template]` attribute (or overriding members that are, such as `OverrideMethod`) cannot be invoked from a template.
- aspect members annotated with the `[Introduce]` or `[InterfaceMember]` attribute are considered run-time (see <xref:introducing-members> and <xref:implementing-interfaces>).

#### Example

The following example shows a simple _Retry_ aspect. The maximum number of attempts can be configured by setting a property of the custom attribute. This property is compile-time.

[!include[Retry](../../code/Caravela.Documentation.SampleCode.AspectFramework/Retry.cs)]

### Compile-time if

If the condition of an `if` statement is a compile-time expression, the `if` statement will be interpreted at compile-time.

> [!NOTE]
> It is not allowed to have a compile-time `if` inside a block whose execution depends on a run-time condition, including a run-time `if`, `else`, `for`, `foreach`, `while`, `switch`, `catch` or `finally`.


#### Example

In the following example, the aspect prints a different string for static methods than for instance ones.

[!include[Compile-Time If](../../code/Caravela.Documentation.SampleCode.AspectFramework/CompileTimeIf.cs)]

### Compile-time foreach

If the expression of a `foreach` statement is a compile-time expression, the `foreach` statement will be interpreted at compile-time.

> [!NOTE]
> It is not allowed to have a compile-time `foreach` inside a block whose execution depends on a run-time condition, including a run-time `if`, `else`, `for`, `foreach`, `while`, `switch`, `catch` or `finally`.

#### Example

The following aspect uses a `foreach` loop to print the value of each parameter of the method to which it is applied.

[!include[Compile-Time If](../../code/Caravela.Documentation.SampleCode.AspectFramework/CompileTimeForEach.cs)]

### No compile-time for, while and goto

It is not possible to create compile-time `for` or `while` loops. `goto` statements are forbidden in templates. If you need a compile-time `for`, you can use the following construct:

```cs
foreach (int i in meta.CompileTime( Enumerable.Range( 0, n ) ))
```

If the approach above is not possible, you can try to move your logic to a compile-time aspect function (not a template method), have this function return an enumerable, and use the return value in a `foreach` loop in the template method.

### typeof, nameof expressions

`typeof` and `nameof` expressions in compile-time code are always pre-compiled into compile-time expression, which makes it possible for compile-time code to reference run-time types.

### Custom compile-time types and methods

If you need to move some compile-time logic from the template to a method, you can create a method in the aspect. It will automatically be considered as compile-time.

If you want to share compile-time code between aspects, you can create a compile-time class by marking it with the `[CompileTimeOnly]` custom attribute.

## Writing dynamic run-time code

### Dynamic typing

Templates use the `dynamic` type to represent types that are unknown by the developer of the template. For instance, an aspect may not know in advance the return type of the methods to which it is applied. The return type is represented by the `dynamic` type.

```cs
dynamic? OverrideMethod() 
{ 
    return default;
}
```

All `dynamic` compile-time code is transformed into strongly-typed run-time code. When the template is expanded, `dynamic` variables are transformed into `var` variables. Therefore, all `dynamic` variables must be initialized.

In a template, it is not possible to generate code that uses `dynamic` typing at run time.

### Converting compile-time values to run-time values

You can use `meta.RunTime( expression )` to convert the result of a compile-time expression into a run-time value. The compile-time expression will be evaluated at compile time, and its result will be converted into _syntax_ that represents that value. Conversions are possible for the following compile-time types:

- Literals;
- Enum values;
- One-dimensional arrays;
- Tuples;
- Reflection objects: <xref:System.Type>, <xref:System.Reflection.MethodInfo>, <xref:System.Reflection.ConstructorInfo>, <xref:System.Reflection.EventInfo>, <xref:System.Reflection.PropertyInfo>, <xref:System.Reflection.FieldInfo>;
- <xref:System.Guid>;
- Generic collections: <xref:System.Collections.Generic.List`1> and <xref:System.Collections.Generic.Dictionary`2>;
- <xref:System.DateTime> and <xref:System.TimeSpan>.

It is not possible to build custom converters at the moment. However, you can generate an expression as a `string`, parse it, and use it in a run-time expression. See [Parsing C# code](#parsing) for details.

#### Example

[!include[Dynamic](../../code/Caravela.Documentation.SampleCode.AspectFramework/ConvertToRunTime.cs)]

### Dynamic code

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

### Generating calls to the code model

When you have a <xref:Caravela.Framework.Code> representation of a declaration, you may want to access it from your generated run-time code. You can do this by using the `Invokers` property exposed by the <xref:Caravela.Framework.Code.IMethod>, <xref:Caravela.Framework.Code.IFieldOrProperty> or <xref:Caravela.Framework.Code.IEvent> interfaces.

For details, see the documentation of the <xref:Caravela.Framework.Code.Invokers> namespace.

### Generating run-time arrays

The first way to generate run-time array is to declare a variable of array type and to use a statement to set each element, for instance:

```cs
var args = new object[2];
args[0] = "a";
args[1] = DateTime.Now;
MyRunTimeMethod( args );
```

If you want to generate an array as a single-line expression, you can use the <xref:Caravela.Framework.Code.SyntaxBuilders.ArrayBuilder> class.

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

### Generating interpolated strings

Instead of generating a string as an array separately and using `string.Format`, you can generate an interpolated string using the <xref:Caravela.Framework.Code.SyntaxBuilders.InterpolatedStringBuilder> class.

The following example shows how an <xref:Caravela.Framework.Code.SyntaxBuilders.InterpolatedStringBuilder> can be used to automatically implement the `ToString` method.

[!include[ToString](../../code/Caravela.Documentation.SampleCode.AspectFramework/ToString.cs)]
 
> [!div id="parsing" class="anchor"]

### Parsing C# code

Sometimes it is easier to generate the run-time code as a simple text instead of using a complex meta API. If you want to use C# code represented as a `string` in your code, you can do it using the <xref:Caravela.Framework.Aspects.meta.ParseExpression*?text=meta.ParseExpression> method. This method returns an <xref:Caravela.Framework.Code.IExpression>, which is a compile-time object that you can use anywhere in compile-time code. The <xref:Caravela.Framework.Code.IExpression> interface exposes the run-time expression in the <xref:Caravela.Framework.Code.IExpression.Value> property.

For instance, consider the following template code:

```cs
var expression = meta.ParseExpression("(a + b)/c");
MyRunTimeMethod( expression.Value );
```

This will generate the following run-time code:

```cs
MyRunTimeMethod((a + b)/c)
```

>[!NOTE] 
> The string expression is inserted _as is_ without any validation or transformation. Always specify the full namespace of any declaration used in a text expression.

>[!NOTE]
> (TODO: document better) You can now use `ExpressionBuilder` (instead of the traditional `StringBuilder`) to build an expression. It offers convenient methods like `AppendLiteral`, `AppendTypeName` or `AppendExpression`. To add a statement to the generated code, use `StatementBuilder` to create the statement and then `meta.InsertStatement` from the template at the place where the statement should be inserted.


### Capturing run-time expressions into compile-time objects

If you want to manipulate a run-time expression as a compile-time object, you can do it using the <xref:Caravela.Framework.Aspects.meta.DefineExpression*?text=meta.DefineExpression> method. This allows you to have expressions that depend on compile-time conditions and control flows. The <xref:Caravela.Framework.Aspects.meta.DefineExpression*> method returns an <xref:Caravela.Framework.Code.IExpression>, the same interface returned by <xref:Caravela.Framework.Aspects.meta.ParseExpression*>. The <xref:Caravela.Framework.Code.IExpression> is a compile-time object that you can use anywhere in compile-time code. It exposes the run-time expression in the <xref:Caravela.Framework.Code.IExpression.Value> property.

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


## Debugging templates

See <xref:debugging>.