---
uid: adding-aspects-in-bulk
---

# Adding many aspects at once

In the previous section <xref:adding-aspects> you learnt how to apply aspects to a given target one at a time. However, you can use `Fabrics` to add aspects to your targets programmatically. 



## Fabrics
Fabrics are really helpful when you need to add aspects to different targets programmatically.  

There are three different types of fabrics available to alter a solution by adding aspects.

|Fabric type | Purpose 
|------------|---------
|Project Fabric| Used to add aspects to different targets in a given project 
|Namespace Fabric| Used to add aspects to different targets in a given namespace
|Type Fabric | Used to add aspects to different members of a type 



## Adding aspect to all public methods of a type
In this section, you shall learn how to add `[Log]` attribute to all public methods of a given type. 


[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/ProjectFabric.cs name="Type-level inherited aspect"]

## Viewing applied aspects via CodeLense



