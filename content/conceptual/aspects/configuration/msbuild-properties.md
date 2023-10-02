---
uid: reading-msbuild-properties
level: 200
---

# Reading MSBuild properties

Instead or additionally to having a programmatic configuraiton API, an aspect can accept configuration by reading MSBuild properties using the <xref:Metalama.Framework.Project.IProject.TryGetProperty*?text=IProject.TryGetProperty> method. 

This strategy allows to configure the aspect without modifying the source code. It can be useful when you want the aspect to behave differently according to a property supplied from the command line, for instance.

Another benefit of accepting MSBuild properties for configuration is that they can be defined in `Directory.Build.props` and shared among all projects in the repository. For details, see [Customize the build by folder](https://learn.microsoft.com/en-us/visualstudio/msbuild/customize-by-directory) in the Visual Studio documentation.


## Exposing MSBuild properties


MSBuild properties are not visible to Metalama by default: you must instruct MSBuild to pass it to the compiler using the `CompilerVisibleProperty` item.

We recommend the following approach to consume a configuration property:

1. Create a file named `YourProject.targets` (the actual file name is not important, but the extension is):

    ```xml
    <Project>
        <ItemGroup>
            <CompilerVisibleProperty Include="YourProperty" />
        </ItemGroup>
    </Project>
    ```

2. Include `YourProject.targets` in your project and mark it for inclusion in the `build` directory of your NuGet package. This ensures that the property will be visible to the aspect for all projects referencing your package. Your `csproj` file should look like this:

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
        <!-- ... -->
        <ItemGroup>
            <None Include="YourProject.targets">
                <Pack>true</Pack>
                <PackagePath>build</PackagePath>
            </None>
        </ItemGroup>
        <!-- ... -->
    </Project>
    ```

3. To configure the aspect, users should set this property in the `csproj` file, as shown in the following snippet:

   ```xml
    <Project Sdk="Microsoft.NET.Sdk">
        <!-- ... -->
        <PropertyGroup>
            <YourProperty>TheValue</YourProperty>
        </PropertyGroup>
        <!-- ... -->
    </Project>
    ```

     > [!WARNING]
     > Line breaks and semicolons are not allowed in the values of compiler-visible properties as they will cause your aspect to receive an incorrect value.


## Reading MSBuild properties from an aspect or fabric

To read an MSBuild property, use the <xref:Metalama.Framework.Project.IProject.TryGetProperty*?text=IProject.TryGetProperty> method. The <xref:Metalama.Framework.Project.IProject> object is available almost everywhere. If you have an <xref:Metalama.Framework.Code.IDeclaration>, use <xref:Metalama.Framework.Code.ICompilation.Project?text=declaration.Compilation.Project>.

### Example: reading MSBuild properties from an aspect

In the example below, the `Log` aspect reads the default category from the MSBuild property.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ConsumingProperty.cs name="Consuming Property"]


## Combining MSBuild properties with the options API

Whenever your aspect library relies on both MSBuild properties and a configuration API, it is preferable to integrate the MSBuild properties with your option class instead of reading the properties from directly from the aspect classes.

Instead, properties should be read from the <xref:Metalama.Framework.Options.IHierarchicalOptions.GetDefaultOptions*> method of your option class. This method receives a parameter of type <xref:Metalama.Framework.Options.OptionsInitializationContext>, which exposes the <xref:Metalama.Framework.Project.IProject> interface, and let you read the properties. The object also lets you report errors or warnings if the properties have an invalid value. Thanks to this approach, you can make the default options dependent on MSBuild properties.

### Examples: building default options from MSBuild properties

In the following example, the options class implements the <xref:Metalama.Framework.Options.IHierarchicalOptions.GetDefaultOptions*> to read default values from the MSBuild properties. It reports a diagnostic if their value is incorrect.

[!metalama-file ~/code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration_ProjectDefault.Options.cs]
