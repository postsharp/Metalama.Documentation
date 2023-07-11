---
uid: exposing-configuration
level: 300
---
# Exposing configuration

Complex and widely-used aspects often require a centralized, project-wide method for configuring their compile-time behavior.

There are two complementary mechanisms for configuration: MSBuild properties and the configuration API.

## Benefits

* **Centralized aspect options**. Providing a configuration API allows the entire project to be configured from a single location. Without a configuration API, users must supply the configuration each time a custom attribute is used.

* **Debug/Release-aware options**. Without a configuration API, setting options based on the `Debug`/`Release` build configuration can be challenging.

* **Run-time performance**. Decisions made at compile time and the generation of optimal run-time code can enhance the run-time performance of your application.

## Consuming MSBuild properties

The simplest method for an aspect to accept a configuration property is through reading an MSBuild property using the <xref:Metalama.Framework.Project.IProject.TryGetProperty*?text=IProject.TryGetProperty> method. MSBuild properties are not visible to aspects by default: you must instruct MSBuild to pass it to the compiler using the `CompilerVisibleProperty` item.

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

### Example

In the example below, the `Log` aspect reads the default category from the MSBuild property.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/ConsumingProperty.cs name="Consuming Property"]

## Exposing a configuration API

For more complex aspects, a set of MSBuild properties may not suffice. In such cases, you can construct a configuration API for your users to call from their project fabrics.

To create a configuration API:

1. Create a class that inherits from <xref:Metalama.Framework.Project.ProjectExtension> and has a default constructor.
2. Optionally, override the <xref:Metalama.Framework.Project.ProjectExtension.Initialize*> method, which receives the <xref:Metalama.Framework.Project.IProject>.
3. In your aspect code, call the [IProject.Extension\<T>()](xref:Metalama.Framework.Project.IProject.Extension*) method, where `T` is your configuration class, to get the configuration object.
4. Optionally, create an extension method for the <xref:Metalama.Framework.Project.IProject> type to expose your configuration API, making it more discoverable. The class must be annotated with `[CompileTime]`.
5. To configure your aspect, users should implement a project fabric and access your configuration API using this extension method.

### Example

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration.cs name="Consuming Property"]


