---
uid: implementing-interfaces
level: 300
summary: "The document provides a guide on how to implement interfaces using the programmatic advising API in the Metalama Framework, with examples for IDisposable and Deep cloning."
---
# Implementing interfaces

Certain aspects necessitate modifying the target type to implement a new interface. This can only be achieved by using the programmatic advising API.

## Step 1. Call IAdviceFactory.ImplementInterface

Within your implementation of the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method, invoke the <xref:Metalama.Framework.Advising.IAdviceFactory.ImplementInterface*> method.

## Step 2. Add interface members to the aspect class

Incorporate all interface members into the aspect class and label them with the <xref:Metalama.Framework.Aspects.InterfaceMemberAttribute?text=[InterfaceMember]> custom attribute. It is not necessary for the aspect class to implement the introduced interface.

The following rules pertain to interface members:

- The name and signature of all template interface members must precisely match those of the introduced interface.
- The accessibility of introduced members is inconsequential. The aspect framework will generate public members unless the <xref:Metalama.Framework.Aspects.InterfaceMemberAttribute.IsExplicit> property is set to true. In this scenario, an explicit implementation is created.

Implementing an interface in an entirely dynamic manner, that is, when the aspect does not already recognize the interface, is currently unsupported.

## Example: IDisposable

In the subsequent example, the aspect introduces the `IDisposable` interface. The implementation of the `Dispose` method disposes of all fields or properties that implement the `IDisposable` interface.

[!metalama-test  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Disposable.cs name="Disposable"]

## Example: Deep cloning

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/DeepClone.cs name="Deep Clone"]


## Referencing interface members in other templates

When introducing an interface member to the type, you often want to access it from templates. Unless the member is an explicit implementation, you have two options:

[comment]: # (TODO: better code examples)


### Option 1. Access the aspect template member

```cs
this.Dispose();
```


### Option 2. Use `meta.This` and write dynamic code

```cs
meta.This.Dispose();
```

## Accessing explicit implementations

The following strategies can be employed to access explicit implementations:

- Cast the instance to the interface and access the member:

    ```cs
    ((IDisposable)meta.This).Dispose();
    ```

- Introduce a private method with the concrete method implementation, and call this private member both from the interface member and the templates.


