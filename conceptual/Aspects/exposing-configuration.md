---
uid: exposing-configuration
---

# Exposing Configuration

Some complex and widely-used aspects need a central, project-wide way to configure their compile-time behavior.

For instance, a logging aspect may let the user define the logging level. In the debug build, the aspect should generate code to log the beginning and success of all methods. In the release build, only failures are logged. For performance reasons, this decision must be taken at compile time (we don't want the production code to contain debugging code), so a run-time configuration file or API will not be sufficient. The aspect needs to expose a configuration mechanism.

There are two complementary configuration mechanisms: MSBuild properties and configuration API.

## Consuming MSBuild properties

The simplest way for an aspect to accept a configuration property is to read an MSBuild property using the <xref:Metalama.Framework.Project.IProject.TryGetProperty%2A?text=IProject.TryGetProperty> method. MSBuild properties are not visible to aspects by default: you have to instruct MSBuild to pass it to the compiler using the `CompilerVisibleProperty` item.

We recommend the following approach to consume a configuration property:

1. Create a file named `YourProject.targets` (the actual name of the file does not matter but the extension does) 
 
    ```xml
    <Project>
        <ItemGroup>
            <CompilerVisibleProperty Include="YourProperty" />
        </ItemGroup>
    </Project>
    ```


2. Include `YourProject.targets` in your project and mark it for inclusion under the `build` directory of your NuGet package. This ensure that the property will be visible by the aspect for all projects referencing your package. Your `csproj` file should look like this:

    ```xml
    <Project  Sdk="Microsoft.NET.Sdk">
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

3. Instruct the user of your aspect to set this property in their own `csproj` file, like this:
 
   ```xml
    <Project  Sdk="Microsoft.NET.Sdk">
        <!-- ... -->
            <PropertyGroup>
                <YourProperty>TheValue</YourProperty>    
            </ItemGroup>
        <!-- ... -->    
    </Project>
    ```

    > [!WARNING]
    > The value of compiler-visible properties must not contain line breaks or semicolons. Otherwise, your aspect will receive an empty or incorrect value. 


### Example

In the following example, the `Log` aspect reads the default category from the MSBuild project. It assumes the property has been exposed using the approach described above.

[!include[Consuming Property](../../code/Metalama.Documentation.SampleCode.AspectFramework/ConsumingProperty.cs)]


## Exposing a configuration API

For more complex aspects, a set of properties may not be convenient enough. Instead, you can build a configuration API that your users will call from project fabrics.

To create a configuration API:

1. Create a class that derives from <xref:Metalama.Framework.Project.ProjectExtension> and have a default constructor. 
2. Optionally, implement the <xref:Metalama.Framework.Project.ProjectExtension.Initialize%2A> method, which receives the <xref:Metalama.Framework.Project.IProject>. 
3. In your aspect code, call the <xref:Metalama.Framework.Project.IProject.Extension%2A?text=IProject.Extension&lt;T&gt;()> method, where `T` is your configuration class, to get the configuration object.
4. Optionally, create an extension method to the <xref:Metalama.Framework.Project.IProject> method to expose your configuration API, so that it is more discoverable.
5. To configure your aspect, users should implement a project fabric and access your configuration API using this extension method. The class must be annotated with `[CompileTimeOnly]`.

### Example

[!include[Consuming Property](../../code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration.cs)]
