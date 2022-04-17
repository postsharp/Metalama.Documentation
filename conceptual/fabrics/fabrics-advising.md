---
uid: fabrics-advising
---

# Advising the Current Type

<!---  I'm at something of a loss as to what to suggest here.

In Engish the is no plural for Advice.  I could give you a piece of advice or I could give you several pieces of advice. 
Similarly you could have one sheep or an entire flock of sheep, you wouldn't have a flock of sheeps.
This raises something of an issue becuase reading about advices simply doesn't sit well in English.  
Of course they are baked into Metalama so the arguement can be made for leaving it as is, except for the fact that 
it distracts from ones understanding of the topic in general becuase as you read it ( and most of us to an extent read 
aloud in our mind as we read) it sounds wrong, and because it sounds wrong it makes comprehension that bit more difficult.  
This is a topic where comprehension is paramount.

When talking about advice  in the plural sense we would normaly refer to bits of, or pieces of advice.
In most cases that distinction would work in this documentation (in its entirety, but sadly not all).
To that end I have not corrected those here (save for the one distinction between noun and verb.  
However i do belive that leaving advices as is hinders comprehension. -->
 

Instead of using aspects, you can advise the current type using a type fabric. A type fabric is a compile-time nested class that acts as a type-level aspect added to the nesting type.

To advise a type using a type fabric:

1. Create a nested type derived from the <xref:Metalama.Framework.Fabrics.TypeFabric> class,

    > [!NOTE]
    > For design-time performance and usability, it is highly recommended to implement type fabrics in a separate file, and mark the parent class as `partial`.

2. Implement the <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> method.

3. Use the methods exposed on the <xref:Metalama.Framework.Aspects.IAspectLayerBuilder.Advices?text=amender.Advices> property. You must be familiar with advanced aspects to use this feature. For details, see <xref:advising-code>.
   
4. You can also add declarative advices such as member introductions to your type fabrics. See <xref:introducing-members> for details.


> [!NOTE]
> Type fabrics are always executed first, before any aspect. Therefore, they can only add advices to members defined in source code. If you need to add advices to members that are introduced by an aspect, you need to use a helper aspect and order it _after_ the aspects that provide the members that you want to advice.


### Example

The following example demonstrates a type fabric that introduces 10 methods to the target type.

[!include[Type Fabric Adding Advices](../../code/Metalama.Documentation.SampleCode.AspectFramework/AdvisingTypeFabric.cs)]

