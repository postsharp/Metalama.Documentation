---
uid: implementing-interfaces
---
# Implementing Interfaces

Many aspects need to modify the target type so it implements a new interface. This can be done only using the programmatic advising API.

## Step 1. Call IAdviceFactory.ImplementInterface

In your implementation of the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method, call the <xref:Metalama.Framework.Advising.IAdviceFactory.ImplementInterface*> method.

## Step 2. Add interface members to the aspect class

Add all interface members to the aspect class and mark them with the <xref:Metalama.Framework.Aspects.InterfaceMemberAttribute?text=[InterfaceMember]> custom attribute. There is no need to have the aspect class implement the introduced interface.

The following rules apply to interface members:

- The name and signature of all interface members must exactly match.
- The accessibility of introduced members have no importance.
- The aspect framework will generate public members unless the <xref:Metalama.Framework.Aspects.InterfaceMemberAttribute.IsExplicit> property is set to `true`. In this case, an explicit implementation is generated.

Implementing an interface in a complete dynamic manner, when the interface itself is not known by the aspect, is not yet supported.

## Example: IDisposable

The aspect in the next example introduces the `IDisposable` interface. The implementation of the `Dispose` method disposes of all fields or properties that implement the `IDisposable` interface.

[!include[Disposable](../../../code/Metalama.Documentation.SampleCode.AspectFramework/Disposable.cs)]

## Example: deep cloning

[!include[Deep Clone](../../../code/Metalama.Documentation.SampleCode.AspectFramework/DeepClone.cs)]


## Referencing interface members in other templates

When you introduce an interface member to a type, your will often want to access it from templates. Unless the member is an explicit implementation, you have two options:

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

The following strategies are possible:

- cast the instance to the interface and access the member, e.g.

    ```cs
    ((IDisposable)meta.This).Dispose();
    ```

- introduce a private method with the real method implementation, and call this private member both from the interface member and from other templates.