---
uid: simple-override-method
level: 200
---

# Getting started: overriding a method

Overriding a method is the most straightforward aspect you can imagine. The implementation of your aspect will simply replace the original implementation of the aspect. Let's see how it works.

## Creating your first method aspect

To create an aspect that overrides methods:

1. Add the `Metalama.Framework` package to your project.

2. Create a class and make it inherit the <xref:Metalama.Framework.Aspects.OverrideMethodAspect> class.  This class will be a custom attribute, so naming it with the `Attribute` suffix is a good idea.

3. Override the <xref:Metalama.Framework.Aspects.OverrideMethodAspect.OverrideMethod*> method.

4. In your <xref:Metalama.Framework.Aspects.OverrideMethodAspect.OverrideMethod*> implementation, call <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed> where the original method should be invoked.

    > [!NOTE]
    > <xref:Metalama.Framework.Aspects.meta> is a special class.  It can almost be considered a keyword that lets you tap into the meta-model of the code you are dealing with. In this case, calling <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed> is equivalent to calling the method that your aspect is overriding.

5. Apply your new aspect to any relevant method as a custom attribute.


### Example: trivial logging

The following aspect overrides the target method and adds a call to `Console.WriteLine` to write a message before the method is executed.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.EnhanceMethods\SimpleLogging.cs]

To see the effect of the aspect on the code in this documentation, switch to the _Transformed Code_ tab above. In Visual Studio, you can preview the transformed code thanks to the _Diff Preview_ feature. See <xref:understanding-your-code-with-aspects> for details.

As you can see, <xref:Metalama.Framework.Aspects.OverrideMethodAspect> does exactly what the name suggests: to override the method. So if you put your aspect on a method, the aspect code will be executed _instead_ of the code of the target method. Therefore, the following line of code gets executed first:

```csharp
Console.WriteLine($"Simply logging a method..." );
```

Then, thanks to the call to <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed>, the original method code is executed.

Arguably, this aspect does not do much yet, so let's make it more useful.

### Example: retrying upon exception

In the previous chapter, you used the built-in aspect `Retry`. Here is how it is implemented.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.EnhanceMethods/Retry.cs]

Note how the overridden implementation in the aspect retries the method being overridden. In this example, the number of retries is hard-coded.

### Example: authorizing the current user

The following example shows how to verify the current user's identity before executing a method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.EnhanceMethods/Authorize.cs]


## Adding context from the target method

None of the examples above contained anything specific to the method to which the aspect was applied. Even the logging aspect wrote a generic message.

Instead of writing a generic message to the console, we will now write a text that includes the name of the target method.

You can get to the target of the aspect by calling the <xref:Metalama.Framework.Aspects.IMetaTarget.Method?text=meta.Target.Method> property, which exposes all relevant information about the current method: its name, its list of parameters and their types, and so on.

So if you want to get to the name of the method you are targeting from the aspect code, you can do so by calling <xref:Metalama.Framework.Code.INamedDeclaration.Name?text=meta.Target.Method.Name>. You can get the qualified name of the method by calling the `meta.Target.Method.ToDisplayString()` method.

Now let's see how this information can be used to enhance the logging aspect that is already created.

The following code shows how this can be used:

### Example: including the method name in the log

[!metalama-test ~/code/Metalama.Documentation.SampleCode.EnhanceMethods/Log.cs]


### Example: profiling a method

When you need to find out which method call is taking time, the first thing you generally do is to decorate the method with print statements to find out how much time each call takes. The following aspect lets you wrap that in an aspect. And whenever you need to track the calls to a method, you just have to place this aspect (in the form of the attribute) on the method as shown in the Target code.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.EnhanceMethods/Profile.cs]

## Going deeper

If you want to go deeper with method overrides, consider jumping to the following articles:

* In this article, you have seen how to use `meta.Proceed` and `meta.Target.Method.Name` in your templates. You can write much more complex and powerful templates, even doing compile-time `if` and `foreach` blocks. To see how you can jump directly to <xref:templates>.

* To learn how to have different templates for `async` or iterator methods, or to learn how to override several methods from a single type-level aspect, jump to <xref:overriding-methods>.



