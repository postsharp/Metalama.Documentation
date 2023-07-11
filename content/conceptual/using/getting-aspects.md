---
uid: getting-aspects
level: 100
---
# Getting aspects

As previously mentioned, this chapter will not delve into the creation of aspects. It will be assumed that you already possess pre-built aspects for use in your projects. These aspects may be provided by your colleagues, our team, or the community.

## Demo aspects

In the examples provided in this chapter, the following pre-built aspects will be utilized:

|Aspect | Purpose |
|-------|----------|
|`Log` | For logging calls to a method. |
|`Retry` | For retrying a method multiple times. |
|`NotifyingPropertyChanged` | For easy implementation of the `INotifyPropertyChanged` interface. |

The NuGet package containing these aspects can be downloaded from [TO_Be_Filled_URL](here).

When applied, these aspects alter the behavior of your source code without modifying the source level. They transform the source code before it is passed to the compiler.

> [!NOTE]
> The implementation of these aspects is not the focus of this chapter. Instead, we will concentrate on how to use them.

## Metalama ready-made aspects

We aim to port the `PostSharp.Patterns` aspect suite to Metalama and make it open-source. This process may take a few months. Once completed, you will have access to professionally-built aspects without the need to create them yourself.

## Community aspects

Additionally, we plan to compile a list of maintained third-party Metalama aspect libraries.

In the interim, you can [search GitHub](https://github.com/search?p=1&q=metalama&type=Repositories) for these libraries.


