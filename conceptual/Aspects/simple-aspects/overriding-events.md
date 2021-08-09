---
uid: overriding-events
---
# Overriding Events

Overriding events works similarly than overriding properties. You have to create an aspect from the <xref:Caravela.Framework.Aspects.OverrideEventAspect> class. You can now override the `add` and `remove` accessors, but overriding the invocation of an event is not yet implemented.

Since overriding `add` and `remove` without `raise` is of limited use, we are skipping the example for now.

>[!div class="see-also"]
> <xref:overriding-members>