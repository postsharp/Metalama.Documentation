---
uid: overriding-events
---
# Overriding Events
The way to override events is similar to [overriding properties](overriding-properties.md). You must create an aspect from the <xref:Metalama.Framework.Aspects.OverrideEventAspect> class and override the `add` and `remove` accessors. However, overriding the invocation of an event is not yet implemented.

Since overridden `add` and `remove` accessors without overriding `raise` have limited applications, we skip the example for now.

