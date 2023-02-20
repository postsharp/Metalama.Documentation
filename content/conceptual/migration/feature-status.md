---
uid: migration-feature-status
---

# Status of the Migration of PostSharp Features to Metalama


## PostSharp Framework (PostSharp.dll)

The PostSharp Framework has been ported entirely to Metalama, with a few notable differences:

* You cannot intercept methods of an external assembly, but only of the current project.
* You cannot advise the event of suspending and resuming an `async` state machine like in PostSharp. Specifically, you cannot advise the `await` keyword.
* You cannot intercept the _raise_ semantic of an event, only the _add_ and _remove_ semantics.
* The concrete architecture constraints under the `PostSharp.Constraints` namespace have not been ported yet, but the underlying features are available in Metalama.


## Patterns.*

__NONE__ of the `PostSharp.Patterns.*` packages have been ported yet.

