# Writing compile-time code


Compile-time expressions are expressions that either contain a call to a compile-time method, or contain a reference to a compile-time local variable or a compile-time aspect member. Compile-time expressions are executed at compile time, when the aspect is applied to a target.

Compile-time statements are statements, such as `if`, `foreach` or `meta.DebugBreak();`, that are executed at compile time.


## The meta pseudo-keyword

The entry point of the compile-time API is the <xref:Metalama.Framework.Aspects.meta> static class, which you can consider as a pseudo-keyword that opens the door to the _meta_ side of meta-programming. The <xref:Metalama.Framework.Aspects.meta> class is the entry point to the meta-model and the members of this class can be invoked only in the context of a template.

The <xref:Metalama.Framework.Aspects.meta> static class exposes to the following members:

- The <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed> method invokes the target method or accessor being intercepted, which can be either the next aspect applied on the same target or the target source implementation itself.
- The <xref:Metalama.Framework.Aspects.meta.Target?text=meta.Target> property gives access to the declaration to which the template is applied.
- The <xref:Metalama.Framework.Aspects.IMetaTarget.Parameters?text=meta.Target.Parameters> property gives access to the current method or accessor parameters.
- The <xref:Metalama.Framework.Aspects.meta.This?text=meta.This> property represents the `this` instance. Together with <xref:Metalama.Framework.Aspects.meta.Base?text=meta.Base>, <xref:Metalama.Framework.Aspects.meta.ThisType?text=meta.ThisType>, and <xref:Metalama.Framework.Aspects.meta.BaseType?text=meta.BaseType> properties, it allows your template to access members of the target class using dynamic code (see below).
- The <xref:Metalama.Framework.Aspects.meta.Tags?text=meta.Tags> property gives access to an arbitrary dictionary that has been passed to the advice factory method, see <xref:sharing-state-with-advice#sharing-state-with-the-tags-property>.
- The <xref:Metalama.Framework.Aspects.meta.CompileTime*?text=meta.CompileTime> method coerces a neutral expression into a compile-time expression.
- The <xref:Metalama.Framework.Aspects.meta.RunTime*?text=meta.RunTime> method converts the result of a compile-time expression into a run-time value (see below).

## Compile-time language constructs

### Compile-time local variables

Local variables are run-time by default. To declare a compile-time local variable, you must initialize it to a compile-time value. If you need to initialize the compile-time variable to a literal value such as `0` or `"text"`, use the `meta.CompileTime` method to convert the literal into a compile-time value.

Examples:

- In `var i = 0;`, `i` is a run-time variable.
- In `var i = meta.CompileTime(0);`, `i` is a compile-time variable.
- In `var parameters = meta.Target.Parameters;`, `parameters` is a compile-time variable.

> [!NOTE]
> You cannot assign a compile-time variable from a block whose execution depends on a run-time condition, including a run-time `if`, `else`, `for`, `foreach`, `while`, `switch`, `catch` or `finally`.


### Compile-time if

If the condition of an `if` statement is a compile-time expression, the `if` statement will be interpreted at compile-time.

> [!NOTE]
> You may not have a compile-time `if` inside a block whose execution depends on a run-time condition, including a run-time `if`, `else`, `for`, `foreach`, `while`, `switch`, `catch` or `finally`.


#### Example

In the following example, the aspect prints a different string for static methods than for instance ones.

[!include[Compile-Time If](../../../code/Metalama.Documentation.SampleCode.AspectFramework/CompileTimeIf.cs)]

### Compile-time foreach

If the expression of a `foreach` statement is a compile-time expression, the `foreach` statement will be interpreted at compile-time.

> [!NOTE]
> It is not allowed to have a compile-time `foreach` inside a block whose execution depends on a run-time condition, including a run-time `if`, `else`, `for`, `foreach`, `while`, `switch`, `catch` or `finally`.

#### Example

The following aspect uses a `foreach` loop to print the value of each parameter of the method to which it is applied.


[!include[Compile-Time If](../../../code/Metalama.Documentation.SampleCode.AspectFramework/CompileTimeForEach.cs)]

### No compile-time for, while and goto

It is not possible to create compile-time `for` or `while` loops. `goto` statements are forbidden in templates. If you need a compile-time `for`, you can use the following construct:

```cs
foreach ( int i in meta.CompileTime( Enumerable.Range( 0, n ) ) )
```

If the approach above is not possible, you can try to move your logic to a compile-time aspect function (not a template method), have this function return an enumerable, and use the return value in a `foreach` loop in the template method.

### nameof expressions

`nameof` expressions in compile-time code are always pre-compiled into compile-time expressions, which makes it possible for compile-time code to reference run-time types.

### typeof expressions

When `typeof(Foo)` is used with a run-time-only type `Foo`, a mock `System.Type` object is returned. This object can be used in run-time expressions or as an argument of Metalama compile-time methods. However, the members of this fake `System.Type`, for instance `Type.Name`, _cannot_ be evaluated at design time. You may sometimes need to call the <xref:Metalama.Framework.Aspects.meta.RunTime*?text=meta.RunTime> method to tip the T# compiler that you want a run-time expression instead of a compile-time one.



## Accessing aspect members

Aspect members are compile-time and can be accessed from templates. For instance, an aspect custom attribute can define a property that can be set when the custom attribute is applied to a target declaration and then read from the aspect compile-time code.

There are a few exceptions to this rule:

- aspect members whose signature contains a run-time-only type cannot be accessed from a template.
- aspect members annotated with the `[Template]` attribute (or overriding members that are, such as `OverrideMethod`) cannot be invoked from a template.
- aspect members annotated with the `[Introduce]` or `[InterfaceMember]` attribute are considered run-time (see <xref:introducing-members> and <xref:implementing-interfaces>).

#### Example

The following example shows a simple _Retry_ aspect. The maximum number of attempts can be configured by setting a property of the custom attribute. This property is compile-time.

[!include[Retry](../../../code/Metalama.Documentation.SampleCode.AspectFramework/Retry.cs)]


## Custom compile-time types and methods

If you want to share compile-time code between aspects or aspect methods, you can create your own types and methods that execute at compile time.

* Compile-time code must be annotated with the [<xref:Metalama.Framework.Aspects.CompileTimeAttribute?text=CompileTime>] custom attribute. You would typically use this attribute on:
  * a method or field of an aspect;
  * a type (`class`, `struct`, `record`, ...);
  * an assembly, using `[assembly: CompileTime]`.
* Code that can run either at compile time or at run time must be annotated with the [<xref:Metalama.Framework.Aspects.RunTimeOrCompileTimeAttribute?text=RunTimeOrCompileTime>] custom attribute.

## Calling other packages from compile-time code

By default, compile-time code can call only the following APIs:
* .NET Standard 2.0 (all libraries)
* Metalama.Framework

For advanced scenarios, the following packages are also included by default:
* Metalama.Framework.Sdk
* Microsoft.CodeAnalysis.CSharp

To make another package available in compile-time code:
1. Make sure that this package targets .NET Standard 2.0.
2. Make sure that the package is included in the project.
3. Edit your `.csproj` or `Directory.Build.props` file and add the following:

```xml
<ItemGroup>
 <MetalamaCompileTimePackage Include="MyPackage"/>
</ItemGroup>
```

When this configuration is done, `MyPackage` can be used both in run-time and compile-time code.

> [!WARNING]
> You must also specify `MetalamaCompileTimePackage` in each project that _uses_ the aspects.

