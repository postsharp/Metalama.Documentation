---
uid: type-fabrics
---

# Amending the Current Type

If you have a large type and want to add aspects or advices to type members without the use of custom attributes, or if you want to automatically introduce new members to the type, you can do it by adding using a type fabric.

To amend a specific type when you have access to its source code:

1. Create a nested type derived from the <xref:Metalama.Framework.Fabrics.TypeFabric> class,

    > [!NOTE]
    > For design-time performance and usability, it is highly recommended to implement type fabrics in a separate file, and mark the parent class as `partial`.

2. Implement the <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType%2A> method.
3. To add aspects to type members, use the <xref:Metalama.Framework.Aspects.IDeclarationSelector%601.WithTargetMembers%2A?text=amender.WithTargetMembers> method.
4. To add advices to to the current type, or to add new members, use the methods exposed on the <xref:Metalama.Framework.Aspects.IAspectLayerBuilder.Advices?text=amender.Advices> property. You must be familiar with advanced aspects to use this feature. For details, see <xref:advising-code>.

## Example

The following example demonstrates a type fabric that introduces 10 methods to the target type.

[!include[Advising Type Fabric](../../code/Metalama.Documentation.SampleCode.AspectFramework/AdvisingTypeFabric.cs)]