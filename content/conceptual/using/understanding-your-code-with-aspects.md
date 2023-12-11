---
uid: understanding-your-code-with-aspects
level: 100
summary: "The document explains how to understand aspect-oriented code using Metalama's tools like CodeLens, Diff Preview, and Debug Transformed Code. It also discusses explicit and implicit aspect applications."
---

# Understanding your aspect-oriented code

Now that you have integrated aspects into your code, you may be curious about its functionality and execution process. Metalama provides several tools to help you understand precisely what happens with your code when you hit the Run button.

These tools include:

* CodeLens
* Diff Preview
* Debug Transformed Code

## CodeLens details

> [!NOTE]
> This feature is only available in Visual Studio when Metalama Tools with Visual Studio are installed.

The first tool that can assist you in understanding your code is CodeLens. This tool displays the number of aspects applied to your code directly in the editor. Clicking on the summary provides more details:

![](./images/log_aspect_applied_on_flakymethod.png)

CodeLens reveals the following details:

|Detail | Purpose |
|-------|---------|
|`Aspect Class` | The name of the applied aspect to this target. |
|`Aspect Target` | The fully qualified name of the target. |
|`Aspect Origin` | How the aspect is applied. |
|`Transformation`| A default message indicating that the aspect alters the behavior of the target method. |

The utility of this feature becomes apparent when you notice that many aspects can be added to your code and when aspects are applied implicitly.

Consider the following example of a method with a couple of aspects applied:

[!code-csharp[](~\code\DebugDemo3\Program.cs)]

This example illustrates a method that retrieves customer details from the database as an XML string. Given the potential issues associated with database connectivity, a `Retry` aspect is useful, as is a `Log` aspect for recording these issues. However, the order of applying these aspects is determined by the aspects' author. As a user of these aspects, you don't need to concern yourself with this.

CodeLens also provides a clickable link to display the transformed and original code side by side.

## Previewing generated code

> [!NOTE]
> This feature is only available in Visual Studio when Metalama Tools with Visual Studio are installed.

To preview the change, click on the `Preview Transformed Code` link. It will display the result as follows:

![Metalama_Diff_Side_by_Side](images/lama_diff_side_by_side.png)

> [!NOTE]
> This preview dialog can also be accessed by pressing `Ctrl + K` followed by `0`.

The screenshot above displays the source of `FlakyMethod` and the code modified by the `[Log]` aspect. However, the command shows the entire file in its original and modified versions side by side.

To view changes for a specific section of the code, select that part of the code from the dropdown as shown below.

![Diff_change_selector](images/metalama_diff_change_view_selector.png)

This can also be accessed from the Context menu that appears when you right-click on the method. The following screenshots highlight the `Show Metalama Diff` option.

![Metalama_Diff_Menu_Option](images/showing_metalama_diff_option.png)

## Implicit aspect addition

In <xref:quickstart-adding-aspects>, you learned how aspects can be added to a target method one at a time. This operation is considered _explicit_ because you are adding the attribute.

However, you may sometimes find that CodeLens displays some aspects applied to certain targets even though no explicit attribute is provided. These are _implicit_ aspect applications.

This occurs because aspects marked as <xref:Metalama.Framework.Aspects.InheritableAttribute?text=[Inheritable]> are inherited from the base class to the child classes. Another reason is that fabrics or other aspects can programmatically apply aspects. In these cases, you won't see an aspect custom attribute on the target declaration.

### Example: NotifyPropertyChanged

The following code example demonstrates how the `PropertyChanged` event is triggered for all members of derived classes when the `NotificationPropertyChanged` aspect is applied.

[!code-csharp[](~\code\DebugDemo4\Program.cs)]

This program produces the following output:

```
X has changed
Y has changed
----------
X has changed
Y has changed
Z has changed
```

Observe that members of the `MovingVertex3D` type don't have an explicit `[NotifyPropertyChanged]` attribute. The aspect is inherited from the base class.

