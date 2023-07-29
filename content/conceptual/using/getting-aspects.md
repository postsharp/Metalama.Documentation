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

The NuGet package containing these aspects is [Metalama.Documentation.QuickStart](https://www.nuget.org/packages/Metalama.Documentation.QuickStart). You can add this package to your projects while following the tutorials in this chapter.

When applied, these aspects alter the behavior of your source code without modifying the source level. They transform the source code before it is passed to the compiler.

> [!NOTE]
> The implementation of these aspects is not the focus of this chapter. Instead, we will concentrate on how to use them.

## Metalama ready-made aspects

We are in the process of porting the `PostSharp.Patterns` aspect suite to Metalama and make it open-source. This process may take a few months. You can follow the work in progress in the [Metalama.Patterns](https://github.com/postsharp/Metalama.Patterns) repository. Once completed, you will have access to professionally-built aspects without the need to create them yourself.

## Community aspects

Additionally, you can find open-source aspect libraries built by the community on the [Metalama community portal](https://www.postsharp.net/community/projects).

You can [search GitHub](https://github.com/search?p=1&q=metalama&type=Repositories) for projects mentioning Metalama.


