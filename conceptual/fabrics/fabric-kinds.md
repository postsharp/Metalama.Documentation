---
uid: fabric-kinds
---

# Kinds of Fabrics

There are four kinds of fabrics. Each kind applies to a different scope. All fabric kinds can add aspect and validators within its scope, but type fabrics can additionally advise its scope, and project fabrics can set configuration options.

| Kind | Base Class | Scope | Abilities |
|-------|-|---------|--|
| Type Fabrics | <xref:Metalama.Framework.Fabrics.TypeFabric> | The containing type (type fabrics are nested types) and any member. | Add aspects, advices, and validators.
| Namespace Fabrics | <xref:Metalama.Framework.Fabrics.NamespaceFabric> | Any type in the namespace that contains the fabric type. | Add aspects and validators.
| Project Fabrics | <xref:Metalama.Framework.Fabrics.ProjectFabric> | Any type in the project that contains the fabric type or in any project. | Add aspects and validators, and by set configuration options. Project fabrics can be inherited from parent directories.
| Transitive Project Fabrics | <xref:Metalama.Framework.Fabrics.TransitiveProjectFabric> | Any type in  any project _referencing_ the containing project | Add aspects and validators, set configuration options.

<!---  I think it would be a good idea to have at least one example of a type fabric somewhere in the documentation that illustrates the point you make below.  -->

> [!NOTE]
> For design-time performance and usability, it is highly recommended to implement type fabrics in a separate file, and mark the parent class as `partial`.