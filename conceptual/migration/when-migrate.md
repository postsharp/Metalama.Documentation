---
uid: when-migrate
---



## When to Migrate from PostSharp to Metalama

> [!WARNING]
> It may not be the right time for you to migrate your existing projects from PostSharp to Metalama. Consider this decision carefully.

Before you decide to migrate your code from PostSharp to Metalama, it is important to consider _whether_ it is a good idea to do it now. After all, PostSharp is still going to be maintained for several years, so you may have no urge to move to Metalama.

## Avoid mixing PostSharp and Metalama in the same project

Mixing PostSharp and Metalama in the same project is in theory possible, but it is not recommended for the following reasons:

* Metalama, like PostSharp, introduces helper methods and properties, i.e. methods that have no equivalent in source code. PostSharp, since it runs _after_ Metalama, will see these helper declarations and the aspects will pick them as if they were user code, which could create incorrect and confusing situations.
* All Metalama aspects will be applied before any PostSharp aspect, just because Metalama runs before PostSharp. It limits the way you will be able to order aspects.
* We have not tested PostSharp with Metalama and will not investigate or address issues arising from the combined use of these products. In other words, this scenario is not _supported_.

## Migrating is not an all-or-nothing decision

As a team or company, you can use both PostSharp and Metalama at the same time. There is no need to take a global, company-wide decision to perform the migration.

You may decide to keep using PostSharp for one product but migrate to Metalama for another product. As long as these products have no dependencies that would result in a single C# project using both Metalama and PostSharp, this may be a sound decision.


## When to Migrate to Metalama

Consider migrating to Metalama if:

* your project relies on a platform to which PostSharp will not be ported (e.g. an ARM64 build environment, WinUI projects, ...), or
* there are strong enough benefits to justify the effort of the migration. For details, see <xref:benefits-over-postsharp>.

DO NOT migrate to Metalama at the moment if:

* your project relies on a platform that is supported by PostSharp and _not_ by Metalama, i.e. typically a pre-2020 platform:

    * Visual Studio 2019 or earlier,
    * .NET Standard 1.6 or earlier,
    * .NET Framework 4.6.0 or earlier, or
    * Visual Basic (Metalama is available for C# projects only);

* your project relies on PostSharp features that have not yet been ported to Metalama. For details, see <xref:migration-feature-status>.
* you have a large team on a business-critical mission with a tight deadline, and therefore prefer the production-honed PostSharp (2008) over the relatively new Metalama (2023).

