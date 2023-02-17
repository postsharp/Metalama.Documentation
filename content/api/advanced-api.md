---
uid: advanced-api
---

# Advanced API documentation

The namespaces and assemblies described in this page allow you to add features to Metalama using the Roslyn API.


| Namespace                                             | Description                                                                                                              |
|--------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------|
| <xref:Metalama.Compiler>                              | This namespace allows you to write source transformers at the lowest level of abstraction, without a concept of aspect. There is no use case where you should use this API, except the <xref:Metalama.Compiler.MetalamaPlugInAttribute> class.                                                                          |
| <xref:Metalama.Framework.Engine.AspectWeavers>        | This namespace enables you to implement Metalama aspects at a low level of abstraction with the Roslyn APIs. Unlike <xref:Metalama.Compiler>, this namespace integrates with the <xref:Metalama.Framework.Engine.CodeModel> namespace.                                                       |
| <xref:Metalama.Framework.Engine.CodeModel>            | This namespace maps the Metalama code model to the Roslyn API. |
| <xref:Metalama.Framework.Engine.Collections>          | This namespace exposes collection interfaces. |
| <xref:Metalama.Framework.Engine.Formatting>           | This namespace exposes the annotations used by the Metalama formatting feature. |
| <xref:Metalama.Testing.AspectTesting>                 | This namespace implements the Metalama test framework. You should not normally use this namespace directly, but the high-level features described in <xref:aspect-testing>. |

