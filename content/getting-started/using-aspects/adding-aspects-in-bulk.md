---
uid: adding-aspects-in-bulk
---

# Adding many aspects at once

In the previous section <xref:get-adding-aspects> you learnt how to apply aspects to a given target one at a time. However, there are two other ways that you can use to add more aspects to your target programmatically. 

These are two ways to add aspects programmatically 

* Using <xref:Metalama.Framework.Aspects.InheritableAttribute?text=[Inheritable]> aspects
* Using Fabrics 

## Inheritable aspects 
For some situations, it is needed to apply a single aspect for all the methods of all derived classes of a base class. In this situation, you can use `Inheritable` aspect 

## Fabrics
Aspects have to be applied to a target to alter the target or its behaviour. But sometimes it is not known or difficult to apply fabrics manually. In such situations, Fabrics are really helpful. You can add many aspects to different targets via Fabric, without explicitly adding an attribute that defines the aspect you want to add. 


