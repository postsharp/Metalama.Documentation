---
uid: getting-aspects
---
# Getting aspects

As promised, this chapter will not cover the art of creating aspects. We'll assume that you already have pre-built aspects that you can use in your projects. These aspects can be provided by your colleagues, by our team, or by the community.

## Demo aspects

In the examples of this chapter, we'll use the following pre-built aspects:

|Aspect | Purpose |
|-------|----------|
|`Log` | To log calls to a method.
|`Retry` | To retry a method a certain number of times.
|`NotifyingPropertyChanged` | To implement the `INotifyPropertyChanged` interface in an easy way..

You can download the NuGet package with these aspects at [TO_Be_Filled_URL](here).

These aspects, when applied, change the behavior of your source code without modifying it at the source level. They will transform the source code before passing it to the compiler.

> [!NOTE]
> Don't worry about their implementation yet, in this chapter we only focus on how to use them.

## Metalama ready-made aspects

Our plan is to port the `PostSharp.Patterns` aspect suite to Metalama and to open source them. It will take us a few months. Once it will be done, you will be able to use professionally-built aspects without having to create them yourself.

## Community aspects

We also plan to build a list of maintained third-party Metalama aspect libraries.

In the meantime, you can [search GitHub](https://github.com/search?p=1&q=metalama&type=Repositories).

