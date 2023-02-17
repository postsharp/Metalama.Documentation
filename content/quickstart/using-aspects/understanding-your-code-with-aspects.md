---
uid: understanding-your-code-with-aspects
---

# Understanding your aspect-oriented code

Now that you have aspects in your code, you may wonder: What will my code do? How will it execute? Worry no more: Metalama offers several tools to let you know what _exactly_ happens with your code when you hit the Run button.

These tools are:

* CodeLens
* Diff Preview
* Debug Transformed Code

## CodeLens details

The first tool that can help you understand your code is one we have already met before:

`CodeLens`:
It shows you, inside the editor, how many aspects have been applied to your code. When you click on the summary, it gives you more details:

![](./images/log_aspect_applied_on_flakymethod.png)

CodeLense shows the following details:

|Detail | Purpose
|-------|---------
|`Aspect Class` | The name of the aspect applied on this target.
|`Aspect Target` |The fully qualified name of the target.
|`Aspect Origin` |How the aspect is applied.
|`Transformation`|This is a default message showing that the aspect changes the behavior of the target method.

At the moment, you may wonder why this could be useful, but it will become clear when you see that many aspects can be added to your code, and when aspects are applied implicitly.

Here is an example of a method with a couple of aspects applied.

[!code-csharp[](~\code\DebugDemo3\Program.cs)]

the example shows a method that is designed to get customer details from the database in the form of an XML string. There can be many problems connecting to a database and therefore a `Retry` aspect makes sense and it is better to log these. So `Log` aspect also makes sense. However, the order in which these aspects will be applied is determined by the aspect author. As a user of these aspects, you need not worry about them.

Another interesting thing that CodeLense shows is a clickable link to show the transformed code and original code side by side.

## Previewing generated code

To preview the change click on the link `Preview Transformed` Code` It will show the result like this

![Metalama_Diff_Side_by_Side](../images/../using-aspects/images/lama_diff_side_by_side.png)

> [!NOTE]
> This preview dialog can also be opened by pressing `Ctrl + K` followed by `0`

The screenshot shows just the source of `FlakyMethod` and the modified code by the `[Log]` aspect. However, you can see that the command shows the entire file in its original and modified version side by side.

To see changes for a particular section of the code, select that part of the code from the dropdown as shown below.

![Diff_change_selector](../images/../using-aspects/images/metalama_diff_change_view_selector.png)

You can also see this from the Context menu that is popped when you right-click on the method. The following screenshots show the highlighted option `Show Metalama Diff`.

![Metalama_Diff_Menu_Option](../images/../using-aspects/images/showing_metalama_diff_option.png)

## Implicit aspect addition

In <xref:quickstart-adding-aspects> you have seen how aspects can be added to a target method one at a time. This operation is _explicit_ because you are adding the attribute.

However, sometimes you shall discover that CodeLense shows some aspects that are applied to some targets even though there is no explicit attribute given. This is like _implicit_ aspect application.

This sort of thing is possible because some of the aspects can be designed as <xref:Metalama.Framework.Aspects.InheritableAttribute?text=[Inheritable]> aspects and these aspects are inherited from the base class to the children classes.

### Implementing `INotifyPropertyChanged`

In the following code example, it is shown how the `PropertyChanged` event is fired for all members of derived classes when the `NotificationPropertyChanged` aspect is applied.

> TODO: Add code.

When run this program prints the following output

```
X has changed
Y has changed
----------
X has changed
Y has changed
Z has changed
```

> [!NOTE]
> Notice that members of `MovingVertex3D` type doesn't have `NotifyPropertyChanged` explicitly. But that aspect is inherited because `NotifyPropertyChanged` is an inhertiable aspect.
Note that there is no **explicit** application of aspects and the inheritable type aspect `[NotifyPropertyChanged]` applied on the base type is inherited by all the members of the derived type.
