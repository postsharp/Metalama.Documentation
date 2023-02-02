---
uid: adding-aspects-by-inheritable-aspects
---

# Inheriting aspects 
Sometimes you need to apply an aspect on all the types in particular part of a product. One way to do this is to write an inheritable aspect and then use it at the base class of the project. The derived class members will have the aspect automatically. 

Creating such an aspect is quite involved. So that' not the topic we will get deep into right now. But in this section you shall see how to For demonstration purposes let's assume that you have the following _Inheritable_ aspect 

[!code-csharp[](HackedAttribute.cs)]]

> [!NOTE]
> The Aspect has to be marked with `[Inheritable]` attribute to make it inheritable.

The following screenshot shows the application of the inhertiable aspect to the derived class members. 


![](../images/inheritable_aspect_applied.png)

> [!NOTE] 
> The thing in the purple box shows the derived as well as the applied `Log` aspect 