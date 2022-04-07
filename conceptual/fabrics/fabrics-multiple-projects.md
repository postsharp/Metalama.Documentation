---
uid: fabrics-multiple-projects
---


# Targeting Multiple Projects with Fabrics

## Transitive Project Fabrics

Transitive project fabrics are compile-time types that can amend any project _referencing_ the containing project by adding aspects and validators, and by setting configuration options.

Transitive project fabrics work exactly like project fabrics, but they execute on referencing projects instead of on the current project.


## Shared Fabrics

This is currently not implemented.

(the idea is that any file named `MetalamaFabric.cs` in any ancestor directory of the current project would be added to the compile-time project and therefore could define a fabric)