---
uid: installing
---

# Installing Metalama: Quick Start

## 1. Install main the NuGet package

Before you start developing aspects, you must add the [Metalama.Framework](https://www.nuget.org/packages/Metalama.Framework) package to your project:

```xml
<ItemGroup>
    <ProjectReference Include="Metalama.Framework" Version="CHANGE ME"/>
</ItemGroup>    
```

For details of all NuGet packages and their dependencies, see <xref:packages>.

>[!NOTE]
>Metalama requires a target framework that supports .NET Standard 2.0.

## 2. Install Metalama Tools for Visual Studio (optional)

To create new aspects, we suggest that you use Visual Studio 2022 and install [Metalama Tools for Visual Studio](https://marketplace.visualstudio.com/items?itemName=PostSharpTechnologies.metalama). This extension adds syntax highlighting to aspect code (as seen in the examples in this documentation) and can be of great help, especially if you are just getting started with Metalama.

## What's Next?

You are now ready for real meta-programming. Go to <xref:getting-started> to see the next steps.
