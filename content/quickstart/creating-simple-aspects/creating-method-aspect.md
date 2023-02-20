---
uid: creating-simple-method-aspect
---

# Enhancing a method 


So far you have used the prebuilt aspects. If you were thinking to get your feet wet, by creating simple aspects that could enhance the behaviour of your methods, then you shall learn how to do that in this chapter. 

# Creating your first method aspect 
In this section you shall create your first ever method aspect. Follow the following steps 
to create the first aspect. 


**Step 1**: To create the simplest possible method aspect, create a class in your project.

**Step 2**: Call it `SimpleLogAttribute` and make it `public`

**Step 3**: Inherit `OverrideMethodAspect` as shown in the code.


[!metalama-sample ~/code/Metalama.Documentation.SimpleAspects/SimpleLogAttribute.cs tabs="target"]

This is a dummy text 


> [!NOTE]
> To use `OverrideMethodAspect` you need to install Metalama Nuget package. Follow the steps at 
  <xref:install> to install it in your project. 
  
**Step 4**: 
