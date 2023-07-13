---
uid: quickstart-adding-aspects
level: 100
---

# Adding aspects to your code

Aspects are custom attributes that must be applied to some target declaration. Some aspects can target methods, while others can target properties or classes.

In this section, you will learn how to use custom attributes to add aspects.

## Adding aspects as custom attributes

Let's assume you have a method that occasionally fails.

![](images/flaky_method_no_aspect.png)

At present, CodeLens displays `No aspect`. This indicates that no aspect has been applied to this method yet.

To apply the `Retry` aspect, add it as a regular custom attribute by typing `[Retry]`:

![](images/applying_retry_attribute.png)

CodeLens now displays `1 aspect`. When you hover your cursor over that text, it will present the following tooltip:

![](images/retry_aspect_applied.png)

If you wish to see the details, click on the text `1 aspect`:

![Retry_Aspect_Code_Lense](images/showing_retry_aspect_code_lense.png)

The details displayed in this example are trivial. However, this feature can be beneficial when you have several aspects on the same method, or when aspects are implicitly applied instead of being explicitly applied using a custom attribute.

## Adding more than one attribute

You have the option to add as many aspects as you need to a target method. In this example, if you wish to log each retry attempt, you can use the `Log` aspect.

![Retry_and_Log_Aspect_Together](images/retry_and_log_aspect_together.png)

CodeLens now displays that two aspects have been applied to the method `FlakyMethod`. If you click on the text `2 aspects`, you can view the details that CodeLens provides:

![Retry_Log_Applied_CodeLense](images/retry_log_code_lense_details.png)


## Adding aspects via the refactoring menu

Instead of manually adding attributes, you can do so via the refactoring menu. You can access this menu by clicking on the _lightbulb_ or _screwdriver_ icon, or by pressing `Ctrl + .`.

![Context_menu_offers_aspects](images/add_aspect_via_context_menu.png)

As illustrated, the refactoring menu displays that three different aspects can be applied to this method. If you hover your mouse cursor over a menu item, you will see a preview of your code with the aspect custom attribute.

The refactoring menu is intelligent enough to recognize which aspect has already been applied and adjusts the recommendations accordingly. The following screenshot illustrates that after applying the `Retry` aspect, the refactoring menu only displays the available but unused aspects.

![Successive_application_of_aspects_via_context_menu](images/successive_application_aspects_via_context_menu.png)

> [!NOTE]
> The refactoring menu only displays aspects that are _eligible_ for your code. The eligibility of an aspect is determined by the aspect's author. For instance, it would not make sense to add a caching aspect to a method returning `void`, so the author of this aspect may make it eligible for non-void methods only.

> [!div class="see-also"]
> <xref:video-first-aspect>
