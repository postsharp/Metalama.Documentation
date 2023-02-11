---
uid: installing
---

# Installing Metalama: Quick Start

## 1. Install the NuGet package

To start working with Metalama, add the [Metalama.Framework](https://www.nuget.org/packages/Metalama.Framework) NuGet package to your project.

You can add the package directly from Visual Studio or using the .NET CLI:

```
dotnet add package Metalama.Framework --prerelease
```

Alternatively, simply paste the following line inside your .csproj file:


```xml
<ItemGroup>
    <PackageReference Include="Metalama.Framework" Version="0.*-*" />
</ItemGroup>
```

For details of all possible NuGet packages and their dependencies, see <xref:packages>.

> [!NOTE]
> Metalama requires [a target framework that supports .NET Standard 2.0](xref:requirements#target-frameworks).


To create new aspects, we suggest that you use Visual Studio 2022 and install [Metalama Tools for Visual Studio](https://marketplace.visualstudio.com/items?itemName=PostSharpTechnologies.metalama). This extension adds syntax highlighting for aspect code (as shown in the examples in this documentation) and can be of great help, especially if you are just getting started with Metalama.

## What's next?

You are now ready for real meta-programming. Go to <xref:getting-started> to see the next steps.

