---
uid: fabrics
---

# Fabrics

Fabrics are classes that are executed at compile time. Unlike aspects, fabrics do not need to be explicitly _added_ to a declaration. Fabrics are executed just because they exist in the code.

Fabrics are implemented in the <xref:Metalama.Framework.Fabrics> namespace. See the namespace documentation for a class diagram.

## Benefits

You can use fabrics in the following situations:

* **Add aspects in bulk**. Instead of adding aspects to each individual declaration with a custom attribute, you can add them programmatically in bulk. See <xref:fabrics-aspects> for details.
* **Validate code.** You can add validation rules to your project or namespace using fabrics. See <xref:fabrics-validation> for details.
* **Configure an aspect library**. When an aspect library exposes a configuration API, you can set the options from a project fabric.

## In this chapter

This chapter contains the following articles:

| Article | Description
|-------------|---------------------------------
| <xref:fabric-kinds> | This article describes the different kinds of fabrics and their abilities |
| <xref:fabrics-aspects> | This article describes how to use fabrics to programmatically add aspects to several declarations - without custom attributes |
| <xref:fabrics-validation> | This article explains how to validate code using fabrics. |
| <xref:fabrics-advising> | This article shows how to modify the current type using a type fabric without having to use a separate aspect class. |
| <xref:fabrics-multiple-projects> | This article explains how to target multiple projects with fabrics, either by using a shared fabric in the root directory of your repo, or by using transitive project fabrics.
| <xref:fabrics-execution-order> | This article explains in which order fabrics and aspects execute in a project.
