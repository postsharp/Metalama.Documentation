---
uid: fabrics
level: 200
---

# Using Fabrics

_Fabrics_ are unique classes in your code that execute at compile time within the compiler, and at design time within your IDE. Unlike aspects, fabrics do not require to be _applied_ to any declaration or _called_ from anywhere. Their primary method will be invoked at the appropriate time simply because it exists in your code. Hence, you can perceive fabrics as _compile-time entry points_.

Even if you presently have no plans to construct your own aspects, understanding fabrics will elevate your proficiency with Metalama.

With fabrics, you can:

* Add aspects programmatically using LINQ-like code queries, instead of marking individual declarations with custom attributes.
* Configure aspect libraries.
* Implement architecture rules to your code (this topic is discussed in the chapter <xref:validation>).

There are four distinct types of fabrics:

| Fabric Type | Abstract Class | Purpose |
|-------------|----------------|---------|
| Project Fabrics | <xref:Metalama.Framework.Fabrics.ProjectFabric> | Add aspects, architecture rules, or configure aspect libraries in the _current_ project. |
| Transitive Project Fabrics | <xref:Metalama.Framework.Fabrics.ProjectFabric> | Add aspects, architecture rules, or configure aspect libraries in projects that _reference_ the current project. |
| Namespace Fabric | <xref:Metalama.Framework.Fabrics.NamespaceFabric> | Add aspects or architecture rules to the namespace that contains the fabric type. |
| Type Fabric | <xref:Metalama.Framework.Fabrics.TypeFabric> | Add aspects to different members of the type that contains the nested fabric type. |

## In This Chapter

| Article | Description |
|---------|-------------|
| <xref:fabrics-adding-aspects> | This article describes how to add aspects using fabrics. |
| <xref:fabrics-configuration> | This article explains how to configure aspect libraries using fabrics. |
| <xref:fabrics-many-projects> | This article describes how to add aspects to multiple projects simultaneously. |

