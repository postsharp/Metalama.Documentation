---
uid: simple-override-method
level: 200
---

# Getting started: overriding a method 


So far, you have used the prebuilt aspects. If you were considering getting your feet wet, by creating simple aspects that could enhance the behaviour of your methods, then you shall learn how to do that in this chapter. 

## Creating your first method aspect 

In this section, you shall create your first-ever method aspect. Follow the following steps to create the first aspect. 


**Step 1**:To create the simplest possible method aspect, create a class in your project.

**Step 2**: Call it `SimpleLogAttribute` and make it `public`

**Step 3**: Inherit `OverrideMethodAspect` as shown in the code.


[!metalama-sample ~/code/Metalama.Documentation.SimpleAspects/SimpleLogAttribute.cs tabs="target"]



> [!NOTE]
> To use `<xref:Metalama.Framework.Aspects.OverrideMethodAspect>` you need to add the `Metalama.Framework` package to your project. 
  
**Step 4**: Build your project. 

If the build is successful, Congratulations! You have successfully created your first method aspect. I would agree that the aspect doesn't do much yet, but you shall learn how to enhance it to be more useful in the upcoming sections in this chapter.  


## Making the `SimpleLog` aspect more specific 

`OverrideMethodAspect` lets you do exactly what the name suggests. Override the method. So if you put your aspect on a method, then the method will first execute the code from the aspect. 

`Console.WriteLine($"Simply logging a method..." );`

And then it will be invoked. The magic will be done by the call to `meta.Proceed`. meta is a special class. 
It can almost be thought of as a keyword that lets you tap into the meta-model of the code you are dealing with. In this case, you are dealing with a method. So, the target of the aspect is the method. 

You can get to the target by a call to the `Target` property of the `meta` class like `meta.Target`. 
The names of the properties are obvious. So if you want to get to the name of the method you are targetting from the aspect code, you can do so by calling <xref:Metalama.Framework.Aspects.IMetaTarget.Method.Name>

`meta.Target.Method.Name`. This will give you just the name of the method. You can get the fully qualified name of the method by calling the `meta.Target.Method.ToDisplayString()` method. 

Now let's see how this information can be used to enhance the log aspect that is already created.  

The following code shows how this can be used:

[!metalama-sample ~/code/Metalama.Documentation.SimpleAspects/SpecificLogAttribute.cs tabs="target"]

> [!NOTE]
> This is the prelude of how you can create several custom template methods for your aspect using the T# language. The next chapter <xref:tsharp-tempaltes> will show several cases of templates. 

## Meet the Retry aspect. 

In the previous chapter, you have used the built-in aspect `Retry`. In this section, you shall learn to create it from scratch. 

**Step 1** Create a class called `RetryFewTimes`

**Step 2** Implement `OverrideMethod` from `OverrideMethodAspect` as shown below. 

[!metalama-sample ~/code/Metalama.Documentation.SimpleAspects/RetryFewTimes.cs tabs="target"]

Note how the overridden implementation in the aspect retries the method being overridden. In this example, the number of retries is 
hard-coded. 

## Example: Checking how much time a method takes. 

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.EnhanceMethods/TimeItAttribute.cs name="Converting bytes to MB"]


## Example: Block