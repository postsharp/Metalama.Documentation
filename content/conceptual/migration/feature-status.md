---
uid: migration-feature-status
---

# Status of the migration of PostSharp features to Metalama


## PostSharp Framework (PostSharp.dll)

The PostSharp Framework has been entirely ported to Metalama, with a few notable differences:

* Methods from an external assembly cannot be intercepted; only those from the current project can be.
* The event of suspending and resuming an `async` state machine, as in PostSharp, cannot be advised. Specifically, the `await` keyword cannot be advised.
* The _raise_ semantic of an event cannot be intercepted, only the _add_ and _remove_ semantics can be.
* The specific architecture constraints under the `PostSharp.Constraints` namespace have yet to be ported. However, the underlying features are already available in Metalama.

## Patterns.*

__NONE__ of the `PostSharp.Patterns.*` packages have been ported as of this moment.
