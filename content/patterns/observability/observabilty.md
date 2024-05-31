---
uid: observability
---

# Observability

## Standard cases

### Simple properties

### Computed properties

### Derived types

### Child objects

### Child objects and derived types

## Skipping a property

## Limitations and workarounds

### Limitations

- method calls: only static methods of with primitive arguments
- other methods cause a warning to be reported
- two mechanisms to work around the warning: `#pragma` and `[SuppressObservabilityWarnings]`
- Mark methods as constants: `[Constant]` or fabric method