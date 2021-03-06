---
uid: validation
---

# Validating Code

This chapter describes how to validate your source code against the architecture, design patterns, and other team conventions.

## Benefits

Validating code is especially important in projects that are developed by a large team or maintained over a long period.

* **Executable rules instead of paper guidelines.** Instead of just being written down (simply for reference), architecture guidelines can now be enforced in real time within the code editor.
* **Immediate feedback.** No need to wait for the CI build to complete. Developers get feedback within seconds.
* **Smoother code reviews.** Petty violations of rules are automatically detected, so code reviews can focus on flows and concepts.
* **Better team alignment.** Automated code validation encourages the team to respect a consistent set of patterns and practices.
* **Lower complexity.** When the whole team follows consistent patterns and practices, the resulting code base is simpler for everybody.
* **Lower architecture erosion.** As a result, the gap between the initial architecture and its implementation in source code stays smaller.

## Aspects or Fabrics

You can validate code using both aspects or fabrics. The approach is very similar.

* Use **aspects** if you want to encapsulate your validation logic as a _custom attribute_ that you can then use many times in the current project or in other projects.
* Use **fabrics** if you don't need your logic to be reusable, or if you prefer to expose an object-oriented API instead of custom attributes.

## In this chapter

This chapter contains the following articles:

| Article | Description |
|--|--|
| <xref:validating-declarations> | This article explains how an aspect or a fabric can validate a declaration. It also shows how to validate the state of the code model after all aspects have been applied.
| <xref:validating-references> | This article describes how to validate the way a declaration is being _used_, i.e. to validate every reference instead of the declaration itself.



