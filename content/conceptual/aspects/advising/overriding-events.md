---
uid: overriding-events
level: 300
---

# Overriding events

Overriding events follows a similar process to [overriding properties](overriding-properties.md). You need to create an aspect from the <xref:Metalama.Framework.Aspects.OverrideEventAspect> class and override the `add` and `remove` accessors. However, please note that overriding the invocation of an event is currently not implemented.

Given that overridden `add` and `remove` accessors have limited applications without an overridden `raise`, we will forego providing an example at this time.


