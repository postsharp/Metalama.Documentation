---
uid: fabrics-execution-order
---

# Execution Order of Fabrics

Fabrics are executed in the following order:

1. **Project Fabrics**. Project fabrics are ordered by the following criteria:

    1. Distance of the source file from the root directory: fabrics closer to the root directory are processed first.
    2. Fabric namespace.
    3. Fabric type name.


2. **Transitive Project Fabrics**. Transitive project fabrics are ordered by the following criteria:

     1. Depth in the dependency graph: dependencies with lower depth (i.e. "nearer" to the main project) are processed first.
     2. Assembly name (alphabetical order).

     Transitive dependencies are intentionally running after compilation dependencies, so compilation dependencies have a chance to configure the transitive dependencies before they run.

4. At this point, the project configuration is frozen (by calling <xref:Metalama.Framework.Project.ProjectExtension.MakeReadOnly> on all configuration objects), so the execution order of the following fabrics should not matter.

5. **Namespace Fabrics**.

5. **Type Fabrics**. Note that type fabrics can provide advice. This advice is executed before any aspect.
6. **Aspects**. See <xref:ordering-aspects>.
