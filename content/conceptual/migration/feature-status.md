---
uid: migration-feature-status
---

# Status of the migration of PostSharp features to Metalama

## PostSharp Aspect Framework

The PostSharp Framework has been entirely ported to Metalama, with a few notable differences:

* Methods from an external assembly cannot be intercepted; only those from the current project can be.
* The event of suspending and resuming an `async` state machine, as in PostSharp, cannot be advised. Specifically, the `await` keyword cannot be advised.
* The _raise_ semantic of an event cannot be intercepted, only the _add_ and _remove_ semantics can be.
* Some constructor-related advice types are not yet implemented:

    * Around constructor body
    * After last constructor
    * After MemberwiseClone

## PostSharp Architecture Framework

The equivalent of PostSharp Architecture Framework (e.g. the `PostSharp.Constraints` namespace) is the `Metalama.Extensions.Architecture` package. 

See <xref:validation> for details.

## PostSharp Patterns

| Library                | Migration Status | Description                                                                                                                                    |
| ---------------------- | ---------------- | ---------------------------------------------------------------------------------------------------------------------------------------------- |
| Contracts              | Completed. All features matched.        | See <xref:contract-patterns>.                                                                                                                  |
| Caching                | Completed. All features matched.        | See <xref:caching>.                                                                                                                            |
| INotifyPropertyChanged | In progress      | See <xref:Metalama.Patterns.Observability.ObservableAttribute>.                                                                                |
| XAML                   | In progress      | See the [GitHub repo](https://github.com/postsharp/Metalama.Patterns/tree/develop/2024.0/src/Metalama.Patterns.Xaml) for the work in progress. |
| Undo/Redo | Not started | |
| Multi-threading | Not started | |
| Aggregatable | Not started | |
| Weak event | Not started | |