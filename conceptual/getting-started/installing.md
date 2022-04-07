---
uid: installing
---

# Adding Metalama to Your Project

## Installing the NuGet package

Before you start developing aspects, you must add the `Metalama.Framework` package to your project:

```xml
<ItemGroup>
    <ProjectReference Include="Metalama.Framework" Version="CHANGE ME"/>
</ItemGroup>    
```

For details of all NuGet packages and their dependencies, see <xref:packages>.

>[!NOTE]
>Metalama requires a target framework that supports .NET Standard 2.0.

## Installing Metalama Tools for Visual Studio

To create new aspects, we suggest that you use Visual Studio 2019 and install [Metalama Tools for Visual Studio](https://marketplace.visualstudio.com/items?itemName=PostSharpTechnologies.metalama). This extension adds syntax highlighting to aspect code (like in the present documentation) and can be of great help, especially if you are just getting started with Metalama.