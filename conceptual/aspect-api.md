---
uid: aspect-api
---

# Aspect API documentation

You will use these namespaces while writing your own aspects or fabrics.


| Namespace                             | Description                                                                                                                |
|---------------------------------------|----------------------------------------------------------------------------------------------------------------------------|
| <xref:Metalama.Framework.Aspects>     | This is the main namespace to create your own aspects.                                                                     |
| <xref:Metalama.Framework.Code>        | This namespace contains the code model.                                                                                    |
| <xref:Metalama.Framework.CodeFixes>   | This namespace allows your aspects to suggest code fixes, accessible at design time from the IDE.   |
| <xref:Metalama.Framework.Diagnostics> | This namespace allows your aspects to report or suppress errors, warnings or info message.                                 |
| <xref:Metalama.Framework.Eligibility> | This namespace allows your aspects to declare to which declarations they can be validly applied. |
| <xref:Metalama.Framework.Fabrics>    | This namespace allows to add aspects or validators to whole projects and namespaces, configure aspects. |
| <xref:Metalama.Framework.Metrics>        | This namespace allows you to read predefined code metric, and to implement your own metrics. Metrics are useful in validators and LinqPad queries. |
| <xref:Metalama.Framework.Project>        | This namespace exposes the object model of the project being processed, as well as the service provider. |
| <xref:Metalama.Framework.RunTime>     | This namespace contains the declaration that is used at run time. All other namespaces are used at compilation time only. |
| <xref:Metalama.Framework.Validation>  | This namespace allows you to build aspects that can validate the user code against your own rules, and how the target of aspects is being used.                                                                                    |

