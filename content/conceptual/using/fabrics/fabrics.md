---
uid: fabrics
level: 200
---

# Using fabrics

_Fabrics_ are special classes in your code that execute at compile time, within the compiler, and at design time, within your IDE. Unlike aspects, fabrics do not need to be _applied_ to any declaration or be _called_ from anywhere. Their main method will be called at the right time just because it exists in your code. Therefore, you can think of fabrics as _compile-time entry points_.

Even if you don't plan to build your own aspects at the moment, learning fabrics will get you to the next level with Metalama.

Using fabrics, you can:

* add aspects programmatically using LINQ-like code queries, instead of tagging individual declarations with custom attributes,
* configure aspect libraries,
* add architecture rules to your code (this is covered in the chapter <xref:validation>).

There are four different types of fabrics:

| Fabric type | Abstract class | Purposes |
|------------|---------|--|--|
| Project fabrics | <xref:Metalama.Framework.Fabrics.ProjectFabric> | Add aspects or architecture rules or configure aspect libraries in the _current_ project. |
| Transitive project fabrics| <xref:Metalama.Framework.Fabrics.ProjectFabric> | Add aspects or architecture rules or configure aspect libraries in projects that _reference_ the current project. |
| Namespace fabric| <xref:Metalama.Framework.Fabrics.NamespaceFabric> | Add aspects or architecture rules to the namespace that contains the fabric type. |
| Type Fabric | <xref:Metalama.Framework.Fabrics.TypeFabric> | Add aspects to different members of the type that contains the nested fabric type. |

## In this chapter

| Article | Description |
|----|-----|
| <xref:fabrics-adding-aspects> | This article describes adding aspects using fabrics. |
| <xref:fabrics-configuration> | This article explains how to configure aspect libraries using fabrics. |
| <xref:fabrics-many-projects> | This article describes how to add aspects to many projects at once. |