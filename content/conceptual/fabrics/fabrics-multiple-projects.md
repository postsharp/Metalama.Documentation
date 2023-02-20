---
uid: fabrics-multiple-projects
---


# Targeting multiple projects with fabrics

## Transitive project fabrics

Transitive project Fabrics are compile-time types that can add aspects, validators and configuration to projects _referencing_ the current project. 
Transitive project fabrics work exactly like project fabrics, but they execute on referencing projects instead of on the current project.

## Shared Fabrics

This is currently not implemented.

(The idea is that any file named `MetalamaFabric.cs` in any ancestor directory of the current project would be added to the compile-time project and thus could define a fabric.)

