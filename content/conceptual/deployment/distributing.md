---
uid: distributing
---

# Distributing projects that use aspects

When your project uses aspects, you need to consider whether the projects that _reference_ your projects will also need to use aspects just because of this reference.

## Flowing the use of aspects

Your project may _flow_ the necessity to use aspects and the aspect framework to consumers of your project for one of the following reasons:

* Your project exposes public aspects that can be used by the referencing projects.
* Your project has non-sealed, public classes that have _inherited_ aspects.
* Your project has public classes that have _reference validators_.
* Your project contains a _transitive project fabric_ or references a project that contains one.

If this is the case, you do not need to take any action. Your package reference to [Metalama.Framework](https://www.nuget.org/packages/Metalama.Framework) will flow to the consumers of your project.

## Preventing the transitive use of aspects

If consumers of your project will _not_ need to use aspects just because of your project, you can prevent [Metalama.Framework](https://www.nuget.org/packages/Metalama.Framework) from flowing to its consumers by setting the `PrivateAssets="all"` property of the `PackageReference`.

Additionally, you need to include the [Metalama.Framework.Redist](https://www.nuget.org/packages/Metalama.Framework.Redist) package, the only package that needs to flow to consumers.

This is achieved by the following code snippet in your `.csproj` file:

```xml
<ItemGroup>
  <PackageReference Include="Metalama.Framework" Version="CHANGE ME" PrivateAssets="all"/>
  <PackageReference Include="Metalama.Framework.Redist" Version="CHANGE ME"/>
</ItemGroup>
```

