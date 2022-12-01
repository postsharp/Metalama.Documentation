---
uid: advanced-api
---

# Advanced API Documentation

These namespaces and assemblies allow you to write aspects (and other code transformations that do not integrate with the aspect framework) at a low level of abstraction, by manipulating directly the Roslyn syntax trees.


| Namespace                             | Description                                                                                                                |
|---------------------------------------|----------------------------------------------------------------------------------------------------------------------------|
| <xref:Metalama.Compiler>              | This namespace allows to write source transformers at the lowest level of abstraction, without a concept of aspect. There is no use case where you should use this API, except the <xref:Metalama.Compiler.MetalamaPlugInAttribute> class.                                                                     |
| <xref:Metalama.Framework.Engine.AspectWeavers>        | This namespace allows you to write aspects at a low level of abstraction with the Roslyn APIs. Unlike <xref:Metalama.Compiler>, this namespace integrates with the | <xref:Metalama.Framework.Engine.CodeModel> | This namespace maps the high-level code model to the Roslyn API. |
| <xref:Metalama.Framework.Engine.Collections>     | This namespace exposes collection interfaces. |
| <xref:Metalama.Framework.Engine.Formatting>  | This namespace exposes the annotations used by the Metalama formatting feature. |
| <xref:Metalama.Testing.AspectTesting>  | This namespace exposes the Metalama test framework. You should normally not use this namespace directly, but use the high-level features described in <xref:compile-time-testing>. |
