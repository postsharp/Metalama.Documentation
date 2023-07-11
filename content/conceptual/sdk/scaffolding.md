---
uid: sdk-scaffolding
level: 400
---

# Creating a Metalama SDK solution structure

A Metalama SDK solution typically contains the following projects:

* The _public_ project (`MyExtension` in the diagram below) contains the classes exposed to the consumers of your solution. This project is a standard class library and targets .NET Standard 2.0.
* The _weaver_ project (`MyExtension.Weaver` in the diagram below) contains the implementation of the public API, utilizing the Metalama SDK and Roslyn APIs. This project is deployed as a _Roslyn analyzer_ and must target _only_ .NET Standard 2.0.
* The _test_ project (`MyExtension.UnitTests` in the diagram below) is optional but recommended as it contains the test suite.
* Consumer projects (`MyProject` in the diagram below) can be part of the same solution or may reference the Metalama extension as a NuGet package.

## Dependency diagram

The graph below illustrates the different projects and their dependencies.

```mermaid
graph BT

    MyExtension.UnitTests --> MyExtension.Weaver
    MyExtension.UnitTests --> Metalama.Testing.AspectTesting
    MyExtension.Weaver --> MyExtension
    MyExtension.Weaver --> Metalama.Framework.Sdk
    Metalama.Framework.Sdk --> Roslyn
    Metalama.Testing.AspectTesting --> Metalama.Framework
    MyExtension --> Metalama.Framework
    MyProject --> MyExtension.Weaver
    Metalama.Framework.Sdk --> Metalama.Framework

    Metalama.Framework.Sdk([Metalama.Framework.Sdk<br/>package])
    Roslyn([Roslyn<br/>packages])
    Metalama.Framework([Metalama.Framework<br/>package])
    Metalama.Testing.AspectTesting([Metalama.Testing.AspectTesting<br/>package])
    MyExtension[MyExtension<br/>project]
    MyExtension.Weaver[MyExtension.Weaver<br/>project]
    MyExtension.UnitTests[MyExtension.UnitTests<br/>project]
    MyProject[MyProject<br/>project]
```

## 1. The public API project

The public project:

* References `Metalama.Framework`.
* Targets `netstandard2.0`.
* Redefines the `PackageId` property to add the `.Redist` suffix to the assembly name.
* Typically makes internals visible to the weaver project.

### Example

[!code-xml[](~\source-dependencies\Metalama.Community\src\Metalama.Community.Virtuosity\Metalama.Community.Virtuosity\Metalama.Community.Virtuosity.csproj)]

## 2. The weaver project

The weaver project:

* Typically has a name that ends with the `.Weaver` suffix. However, not following this convention does not have any impact.
* References `Metalama.Framework.Sdk`.
* References the public project.
* Targets exclusively `netstandard2.0`.
* Is typically the main project of the NuGet package.
* Redefines the `PackageId` to the real package name, i.e., removes the `.Weaver` suffix from the package name.

### Example

[!code-xml[](~\source-dependencies\Metalama.Community\src\Metalama.Community.Virtuosity\Metalama.Community.Virtuosity.Weaver\Metalama.Community.Virtuosity.Weaver.csproj)]

## 3. The unit test project

The unit test project:

* References the public project.
* References the weaver project with both `OutputItemType="Analyzer"` and `ReferenceOutputAssembly="false"`.
* References the following packages:
  * `Metalama.Testing.AspectTesting`
  * `Microsoft.NET.Test.Sdk`
  * `xunit`
  * `xunit.runner.visualstudio`
* Can target any platform supported by the test framework.

### Example

[!code-xml[](~\source-dependencies\Metalama.Community\src\Metalama.Community.Virtuosity\Metalama.Community.Virtuosity.UnitTests\Metalama.Community.Virtuosity.UnitTests.csproj)]

## 4. Consuming projects in the same solution

If the consuming projects are part of the same solution as the Metalama extension projects, they need to:

* Reference the public project.
* Reference the weaver project with both `OutputItemType="Analyzer"` and `ReferenceOutputAssembly="false"` so that it is included only at compile time.
* Reference the `Metalama.Framework` package.

### Example

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net6.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Metalama.Open.Virtuosity.Weaver\Metalama.Open.Virtuosity.Weaver.csproj" OutputItemType="Analyzer" ReferenceOutputAssembly="false" />
    <ProjectReference Include="..\Metalama.Open.Virtuosity\Metalama.Open.Virtuosity.csproj" />
    <PackageReference Include="Metalama.Framework" Version="$(MetalamaVersion)" />
  </ItemGroup>

</Project>
```

## 5. Consuming projects in a different solution

If the consuming projects are not part of the same solution as the Metalama extension projects, they need to:

* Reference the main package of the extension, produced from the weaver project.

### Example

[!code-xml[](~\source-dependencies\Metalama.Community\src\Metalama.Community.Virtuosity\Metalama.Community.Virtuosity.TestApp\Metalama.Community.Virtuosity.TestApp\Metalama.Community.Virtuosity.TestApp.csproj)]
