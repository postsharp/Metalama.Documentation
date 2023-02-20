---
uid: fabrics-advising
---

# Advising a single type with a fabric

Instead of using aspects, you can advise the current type using a type fabric. A type fabric is a compile-time nested class that works as a type-level aspect added to the target type.

To advise a type using a type fabric:

1. Create a nested type derived from the <xref:Metalama.Framework.Fabrics.TypeFabric> class.

    > [!NOTE]
    > For design-time performance and usability, we recommend implementing type fabrics in a separate file, and mark the containing type as `partial`.

2. Override the <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> method.

3. Use the methods exposed on the <xref:Metalama.Framework.Aspects.IAspectBuilder.Advice?text=amender.Advice> property. You must be familiar with advanced aspects before you can use this feature. For details, see <xref:advising-code>.

4. You can also add declarative advice, such as member introductions, to your type fabrics. See <xref:introducing-members> for details.


> [!NOTE]
> Type fabrics are always executed first, before any aspect. Therefore, they can only add advice to members defined in source code. If you need to add advice to members that are introduced by an aspect, you need to use a helper aspect and order it _after_ the aspects that provide the members that you want to advice.


### Example

The following example demonstrates how to create a type fabric that introduces 10 methods to the target type.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/AdvisingTypeFabric.cs name="Type Fabric Adding Advice"]

