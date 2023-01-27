---
uid: template-overview
---

# T# Templates: Overview

T# is the template language used by Metalama. The syntax of T# is 100% compatible with C#. The difference between T# and C# is the T# compiler executes within the compiler or the IDE and generates C# code, while the C# compiler generates IL (binary) files.

## Introduction

T# templates are a mix between _compile-time_ and _run-time_ expressions and statements. Compile-time expressions and statements are evaluated at compile time in the compiler (or at design time in the IDE when you use the [Preview feature](xref:preview)), and result in the generation of other run-time expressions.

Metalama analyzes T# and splits the compile-time part from the run-time part. It does it by applying a set of inference rules. Compile-time expressions and statements very often start with the `meta` pseudo-keyword. <xref:Metalama.Framework.Aspects.meta> is actually a static class, but it is useful to think of it as a kind of magic keyword that means that it starts a compile-time expression or statement.


### Initial example

Before moving forward, let's illustrate this concept with an example. The following aspect writes some text to the console before and after the execution of a method. 

In the below code, compile-time code is highlighted <span class="metalamaClassification_CompileTime">differently</span>, so you can see which part of the code executes at compile time and which part executes at run time. In the different tabs on the example, you can see the aspect code (with the template), the target code (to which the aspect is applied) and the transformed code, i.e. the target code transformed by the aspect.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/SimpleLogging.cs name="Simple Logging"]


> [!NOTE]
> To benefit from syntax highlighting in Visual Studio, install the [Metalama Tools for Visual Studio](https://marketplace.visualstudio.com/items?itemName=PostSharpTechnologies.metalama). Syntax highlighting is not supported in other IDEs.


The expression `meta.Target.Method` (with an implicit trailing `.ToString()`) is a compile-time expression. It is replaced at compile time by the name and signature method to which the aspect is applied.

The call to `meta.Proceed()` means that the original method body should be injected at that point.


### Comparison with Razor

You can compare T# to Razor. Razor allows you to create dynamic web pages by mixing two languages: C# for server-side code (the _meta_ code), and HTML for client-side code. With T#, you also have two kinds of code: _compile-time_ and _run-time_ code. The compile-time code generates the run-time code. The difference with Razor is that in T# both the compile-time and run-time code are the same language: C#. Metalama interprets every expression or statement in a template as having _either_ run-time scope _or_ compile-time scope. Compile-time expressions are generally initiated by calls to the <xref:Metalama.Framework.Aspects.meta> API.


