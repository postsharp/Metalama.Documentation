---
uid: getting-aspects
---
# Getting aspects

As promised, this chapter will not cover the art of creating aspects. We will assume that you already have pre-built aspects that you can use in your projects. These aspects can be provided by your colleagues, by our team, or by the community.



## Demo aspects

In the examples of this chapter, we shall use the following pre-built aspects:

|Aspect | Purpose |
|-------|----------|
|`Log` | To log calls of a method.
|`Retry` | To retry a method a certain number of times.
|`NotifyingPropertyChanged` | To implement the `INotifyPropertyChanged` interface.

You can download the Nuget package with these aspects at [TO_Be_Filled_URL](here).

These aspects, when applied will change the behavior of your source code without modifying it at the source level. These will transform the code before the generated code is given to the compiler.

> [!NOTE]
> Again, you need not worry about their implementation as of now because in this chapter you shall only consume them.


## Metalama ready-made aspects

Our plan is to port the `PostSharp.Patterns` aspect suite to Metalama and to open source them. It will take us a few months. Once it will be done, you will be able to use professionally-built aspects without having to create them yourself.


## Community aspects

We also plan to build a list of maintained third-party Metalama aspect libraries.

In the meantime, you can (search GitHub)[https://github.com/search?p=1&q=metalama&type=Repositories].

