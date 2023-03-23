---
uid: validation
---

# Verifying architecture

This chapter describes how to verify your source code against the architecture, design patterns, and other team conventions.

There are two ways to add verification rules to your code. You can do it declaratively by applying architecture custom attributes to your code or programmatically using a compile-time fluent API. Metalama comes with a set of ready-made custom attributes and compile-time methods, and you can easily create your own attributes or methods for rules specific to your project.

## Benefits

Verifying code against architecture is especially important for projects developed by a large team or maintained over an extended period.

* **Executable rules instead of paper guidelines**. Architectural guidelines can now be enforced in real-time within the code editor instead of only being written down and put on shelves.
* **Immediate feedback**. Developers don't need to wait for the CI build to complete; feedback is given within seconds.
* **Smoother code reviews**. Rule violations are automatically detected so that code reviews can focus on flows and concepts.
* **Better team alignment**. Automated code validation encourages the team to respect consistent patterns and practices.
* **Lower complexity**. The resulting codebase is simpler when everyone on the team follows consistent patterns and practices.
* **Reduced architecture erosion**. The gap between the initial architecture and its implementation in source code stays smaller.


## In this chapter


This chapter contains the following articles:


|Article  |Description  |
|---------|---------|
|<xref:validating-usage>     |  This article shows how to validate how a set of namespaces, types or members can be _used_.       |
|<xref:naming-conventions> | This article explains how to enforce naming conventions in your code . |
|<xref:experimental> | This article shows how to mark an API as experimental, so a warning is reported when the API is used. |
|<xref:validation-extending>     |  This article shows how to create custom attributes or fabric extension methods to validate your own architecture rules.   |

