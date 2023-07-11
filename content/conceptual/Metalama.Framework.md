# Metalama.Framework

> Try Metalama in your browser, without installing anything, at <https://try.metalama.net/>.

## Introduction

Metalama is an [AOP](https://en.wikipedia.org/wiki/Aspect-oriented_programming) framework wherein aspects are written as pure C# templates.

These templates facilitate the writing of code that seamlessly combines compile-time information (such as the names and types of a method's parameters) and run-time information (such as parameter values). This integration is achieved in a natural way, without the need to learn another language or combine C# with a special templating language.

## Example

Consider this simple aspect, which logs a method's name and its parameters' information to the console before executing the method.

```c#
class Log : OverrideMethodAspect
{
    public override dynamic Template()
    {
        Console.WriteLine(target.Method.Name);
        foreach (var parameter in target.Parameters)
        {
            Console.WriteLine(parameter.Type + " " + parameter.Name + " = " + parameter.Value);
        }

        return proceed();
    }
}
```

This aspect can be applied to a method as an attribute:

```c#
[Log]
void CountDown(string format, int n)
{
    for (int i = 0; i < n; i++)
    {
        Console.WriteLine(format, i);
    }
}
```

Applying this aspect modifies the method so that it behaves as if it was written like this:

```c#
void CountDown(string format, int n)
{
    Console.WriteLine("CountDown");
    Console.WriteLine("string format = " + format);
    Console.WriteLine("int n = " + n);
    for (int i = 0; i < n; i++)
    {
        Console.WriteLine(format, i);
    }
}
```

Observe that the compile-time `foreach` loop was unrolled, so that each parameter has its own statement. The compile-time expressions `parameter.Type` and `parameter.Name` have been evaluated and even folded with the nearby constants. Conversely, the run-time calls to `Console.WriteLine` have been preserved. The expression `parameter.Value` is special and has been translated to access the values of the parameters.

## Aspects, Advice, and Initialize

Abstract aspects like `OverrideMethodAspect` suffice for simple needs, but more complex cases require further customization. Consider the scenario where you want to apply an aspect attribute to a type and have it affect all its methods. In Metalama, this can be achieved by directly implementing the `IAspect<T>` interface and incorporating this logic into the `Initialize` method, as shown below:

```c#
public class CountMethodsAspect : Attribute, IAspect<INamedType>
{
    public void Initialize(IAspectBuilder<INamedType> aspectBuilder)
    {
        var methods = aspectBuilder.TargetDeclaration.Methods.GetValue();

        this.methodCount = methods.Count();

        foreach (var method in methods)
        {
            aspectBuilder.Advice.Override(method, nameof(Template));
        }
    }

    int i;
    int methodCount;

    [OverrideMethodTemplate]
    public dynamic Template()
    {
        Console.WriteLine($"This is {++this.i} of {this.methodCount} methods.");
        return proceed();
    }
}
```

This aspect adds the `Override` *advice* to each method in a marked type. Here, "advice" refers to a modification applied to a single element in your code.

As illustrated, the `Initialize` method can also serve other purposes, such as initializing values shared by the advice methods of the aspect.

## Template Context

Inside a template method, additional operations are available through the `TemplateContext` class members. These members are intended for direct use, which requires adding `using static Metalama.Framework.Aspects.TemplateContext;` at the beginning of your files. To make these members appear as special operations, they use the camelCase naming convention, in violation of .NET naming conventions, which require PascalCase.

These members include:

- `dynamic Proceed()`: Transfers control to the original code of the method to which the template is being applied. When multiple advice methods per method are supported, this will instead transfer control to the next template in line, if any remain.
- `ITemplateContext Target { get; }`: Provides access to information about the code element to which the template is being applied.
- `T CompileTime<T>( T expression )`: Informs the templating engine that this expression should be considered to be compile-time, even when it would not be normally. Besides this, the input value is returned unchanged.

## Packaging an Aspect

Creating a NuGet package for a project that contains Metalama.Framework aspects is straightforward; it works the same way as creating a NuGet package for a regular .NET library.
