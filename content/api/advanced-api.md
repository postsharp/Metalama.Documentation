---
uid: advanced-api
---

# Advanced API documentation

This page outlines the namespaces and assemblies that enable you to augment Metalama features using the Roslyn API.

| Namespace                                             | Description                                                                                                              |
|--------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------|
| <xref:Metalama.Compiler>                              | This namespace facilitates writing source transformers at the most basic level of abstraction, devoid of aspect concepts. |
| <xref:Metalama.Framework.Engine.AspectWeavers>        | This namespace allows you to implement Metalama aspects at the lowest level of abstraction using the Roslyn APIs. Unlike <xref:Metalama.Compiler>, this namespace integrates with the <xref:Metalama.Framework.Engine.CodeModel> namespace.                                                       |
| <xref:Metalama.Framework.Engine.CodeModel>            | This namespace correlates the Metalama code model with the Roslyn API. |
| <xref:Metalama.Framework.Engine.Collections>          | This namespace contains collection interfaces. |
| <xref:Metalama.Framework.Engine.Formatting>           | This namespace contains the annotations utilized by the Metalama formatting feature. |
| <xref:Metalama.Testing.AspectTesting>                 | This namespace contains the Metalama testing framework. Typically, this namespace is not used directly; instead, the high-level features delineated in <xref:aspect-testing> are used. |


