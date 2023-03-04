---
uid: validation
---

# Validating architecture

This chapter describes how to validate your source code against the architecture, design patterns, and other team conventions.

## Benefits

Validating code is especially important for projects developed by a large team or maintained for an extended period.

* **Executable rules instead of paper guidelines**. Architectural guidelines can now be enforced in real-time within the code editor instead of being simply written down and put on shelves.
* **Immediate feedback**. Developers don't need to wait for the CI build to complete. Feedback is given within seconds.
* **Smoother code reviews**. Petty rule violations are automatically detected so that code reviews can focus on flows and concepts.
* **Better team alignment**. Automated code validation encourages the team to respect consistent patterns and practices.
* **Lower complexity**. The resulting codebase is simpler for everyone when the whole team follows consistent patterns and practices.
* **Reduced architecture erosion**. The gap between the initial architecture and its implementation in source code stays smaller.

## Aspects or Fabrics

You can validate code using both aspects or fabrics. The approach is very similar.

* Use **aspects** to encapsulate your validation logic as a _custom attribute_ that can be used in the current or other projects. Custom attributes are preferable when the target declarations must be hand-picked.
* Use **fabrics** if you do not need reusable logic or prefer to expose an object-oriented API instead of custom attributes. Fabrics are preferable when the target declarations can be selected as a general rule.

## In this chapter

This chapter contains the following articles:

| Article | Description |
|--|--|
| <xref:validating-declarations> | This article explains how aspects and fabrics validate a declaration. It also shows how to validate the state of the code model after all aspects have been applied.
| <xref:validating-references> | This article describes how to validate the way a declaration is _used_, i.e., how to validate every reference instead of the declaration itself.



