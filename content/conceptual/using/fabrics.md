---
uid: fabrics
level: 200
---

# Fabrics

In the previous article, you have seen how to add many aspects at once using compile-time imperative code (as opposed to declarative custom attributes). We have shown a single type of fabric: <xref:Metalama.Framework.Fabrics.ProjectFabric>. There are more types of fabric, and more use case for them.

Even if you presently have no plans to construct your own aspects, understanding fabrics will elevate your proficiency with Metalama.

_Fabrics_ are unique classes in your code that execute at compile time within the compiler, and at design time within your IDE. Unlike aspects, fabrics do not require to be _applied_ to any declaration or _called_ from anywhere. Their primary method will be invoked at the appropriate time simply because it exists in your code. Hence, you can perceive fabrics as _compile-time entry points_.

With fabrics, you can:

* Add aspects programmatically using LINQ-like code queries, instead of marking individual declarations with custom attributes. See <xref:fabrics-adding-aspects>
* Configure aspect libraries. See <xref:fabrics-configuration>
* Implement architecture rules to your code. See <xref:validation>.


Additionally to <xref:Metalama.Framework.Fabrics.ProjectFabric>, there are three more types of fabric:


| Fabric Type | Abstract Class | Purpose |
|-------------|----------------|---------|
| Project Fabrics | <xref:Metalama.Framework.Fabrics.ProjectFabric> | Add aspects, architecture rules, or configure aspect libraries in the _current_ project. |
| Transitive Project Fabrics | <xref:Metalama.Framework.Fabrics.TransitiveProjectFabric> | Add aspects, architecture rules, or configure aspect libraries in projects that _reference_ the current project. |
| Namespace Fabric | <xref:Metalama.Framework.Fabrics.NamespaceFabric> | Add aspects or architecture rules to the namespace that contains the fabric type. |
| Type Fabric | <xref:Metalama.Framework.Fabrics.TypeFabric> | Add aspects to different members of the type that contains the nested fabric type. |


Let's now dive into the second use case of fabrics: configuration.