---
uid: tsharp-tempaltes
---

# Writing templates with T# 

A template is a way to create a new override for a method. T# is 100% C# compatible templating language.  
Whenever you override a method from `<xref:Metalama.Framework.Aspects.OverrideMethodAspect>`, you are essentially creating a template. 

In a template you can have _compile time_ and _run time_ expressions. For example, `Console.WriteLine` is a call that will execute at runtime and `meta.Target.Method.Name` is a compile-time expression. 

The essence of templates is that you can write compile-time code having runtime expressions. 

In this chapter few examples of templates are provided for your reference by posing some situations. 

## Example 1: Getting the parameters of the target method 
A method is created at compile time. However, if you want to capture the values of the parameters at runtime and do some logging; you are still in luck. Because Metalama templates let you capture the parameter names and values. The following code demonstrates this purpose. 


[!metalama-sample ~/code/Metalama.Documentation.SimpleAspects/ShowParameterValuesAttribute.cs tabs="target"]




## Example 2: Changing logging 
Based on the return type of the target methods; you may decide to log them differently. The following code demonstrates this

[!metalama-sample ~/code/Metalama.Documentation.SimpleAspects/BranchedLoggingAttribute.cs tabs="target"]
