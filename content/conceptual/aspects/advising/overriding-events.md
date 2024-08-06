---
uid: overriding-events
level: 300
summary: "The document discusses how to override events in a similar manner to overriding properties, but notes that overriding event invocation is not implemented."
---

# Overriding events

Overriding events follows a similar process to [overriding properties](overriding-properties.md). You need to create an aspect from the <xref:Metalama.Framework.Aspects.OverrideEventAspect> class and override the `add` and `remove` accessors. However, please note that overriding the invocation of an event is currently not implemented.

Given that overridden `add` and `remove` accessors have limited applications without an overridden `raise`, we will forego providing an example at this time.



