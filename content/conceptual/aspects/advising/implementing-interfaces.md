---
uid: implementing-interfaces
level: 300
---
# Implementing interfaces

Some aspects need to modify the target type so that it implements a new interface. This can be done only by using the programmatic advising API.

## Step 1. Call IAdviceFactory.ImplementInterface

In your implementation of the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method, call the <xref:Metalama.Framework.Advising.IAdviceFactory.ImplementInterface*> method.

## Step 2. Add interface members to the aspect class

Add all interface members to the aspect class and mark them with the <xref:Metalama.Framework.Aspects.InterfaceMemberAttribute?text=[InterfaceMember]> custom attribute. There is no need to have the aspect class implement the introduced interface.

The following rules apply to interface members:

- The name and the signature of all template interface members must match exactly those of the introduced interface.
- The accessibility of introduced members is irrelevant.
- The aspect framework will generate public members unless the <xref:Metalama.Framework.Aspects.InterfaceMemberAttribute.IsExplicit> property is set to true. In this case, an explicit implementation is created.

Implementing an interface in a complete dynamic manner, when the interface is not already known by the aspect, is not yet supported.

## Example: IDisposable

The aspect in the next example introduces the `IDisposable` interface. The implementation of the `Dispose` method disposes of all fields or properties that implement the `IDisposable` interface.

[!metalama-sample  ~/code/Metalama.Documentation.SampleCode.AspectFramework/Disposable.cs name="Disposable"]

## Example: Deep cloning

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/DeepClone.cs name="Deep Clone"]


## Referencing interface members in other templates

When you introduce an interface member to the type, you often want to access it from templates. Unless the member is an explicit implementation, you have two options:

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

The following strategies are possible to access explicit implementations:

- Cast the instance to the interface and access the member:

    ```cs
    ((IDisposable)meta.This).Dispose();
    ```

- Introduce a private method with the real method implementation, and call this private member both from the interface member and from other templates.

