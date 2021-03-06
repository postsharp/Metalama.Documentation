---
uid: distributing
---

# Distributing Projects That Use Aspects

When your project uses aspects, you need to consider whether the projects that _reference_ your projects will also need to use aspects just because of this reference.

## Flowing the use of aspects

Your project may _flow_ the necessity to use the aspect framework to consuming projects for one the following reasons:

* Your project exposes public aspects that can be used by referencing projects.
* Your project has non-sealed, public classes that have _inheritable_ aspects. 
* Your project has public classes that have _reference validators_.
* Your project contains a _transitive project fabric_ or references a project that contains one.

If this is the case in your project, you do not need to take any action. Your package reference to [Metalama.Framework](https://www.nuget.org/packages/Metalama.Framework) will flow to the consumers of your project.

## Preventing the transitive use of aspects

If, conversely, the consumers of your project will _not_ need to use aspects just because of your project, you can prevent the [Metalama.Framework](https://www.nuget.org/packages/Metalama.Framework) from flowing to the consumers of your project by setting the `PrivateAssets="all"` property to the package reference. Additionally, you need to include the [Metalama.Framework.Redist](https://www.nuget.org/packages/Metalama.Framework.Redist) package, which is the only package that needs to flow to consumers.

This is achieved by the following code snippet in your `.csproj` file:

```xml
<ItemGroup>
    <PackageReference Include="Metalama.Framework" Version="CHANGE ME" PrivateAssets="all" />
    <PackageReference Include="Metalama.Framework.Redist" Version="CHANGE ME" />
</ItemGroup>
```
