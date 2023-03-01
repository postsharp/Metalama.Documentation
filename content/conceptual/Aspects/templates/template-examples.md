---
uid: template-examples
---


# Some templates
Templates can be simple or they can be arbitrarily complex. In this section, you shall see how a simple template is introduced and gradually modified to showcase different features of templating. 


## Creating a fullname from First and last name

The following aspect shows how you can create a fullname from two settable properties (`FirstName` and `LastName` in this case). This template method shows how `meta.This` is used to get to the `this` which represents the target type.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.EnhanceProperties/Name.cs] 




## Using C# directly 

This example relies on the fact the field or property 'FirstName' or 'LastName' exists. If you want to bypass the meta way and directly write the expression you can do so by using `ExpressionFactory.Parse` method as shown in the aspect below. 

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.EnhanceProperties/NameDirect.cs] 



## Finding misspelt property names 
Sometimes, the property names can be a combination of upper and lower case letters and you can still use Metalama to locate the right property name close to `Firstname` and close to `LastName` to find the actual property names and glue them together inside the aspect to emit the `fullname`. The following code shows that 

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.EnhanceProperties/NameMisspelt.cs] 




## Calling functions to get the name
Here we locate the property which is probably the firstName because there can be different spelling errors. 

However, you can completely rely on an outside resource to generate the name. If you keep the type partial and have a `GetName` function implemented by a separate person, you can call that function like this to get the Name. 

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.EnhanceProperties/NameViaGetName.cs] 