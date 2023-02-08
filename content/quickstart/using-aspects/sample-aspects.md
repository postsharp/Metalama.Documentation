---
uid: sample-aspects
---
# Sample aspects for demonstration

As promised, this chapter will not cover the act of writing a custom aspect. We will assume that you already have aspects that you can use in your projects.

In the examples of this chapter, we shall use the following prebuilt aspects:

|Aspect | Purpose |
|-------|----------|
|`Log` | To log calls of a method.
|`Retry` | To retry a method a certain number of times.
|`NotifyingPropertyChanged` | To implement the `INotifyPropertyChanged` interface.

You can download the Nuget package with these aspects at [TO_Be_Filled_URL](here).

These aspects, when applied will change the behavior of your source code without modifying it at the source level. These will transform the code before the generated code is given to the compiler. 

> [!NOTE]
> Again, you need not worry about their implementation as of now because in this chapter you shall only consume them. 