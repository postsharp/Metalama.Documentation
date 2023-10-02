---
uid: exposing-configuration-before-2023-4
level: 300
---

# Exposing configuration (before v2023.4)

> [!NOTE]
> Starting from Metalama 2023.4, this approach is obsolete.


For more complex aspects, a set of MSBuild properties may not suffice. In such cases, you can construct a configuration API for your users to call from their project fabrics.

To create a configuration API:

1. Create a class that inherits from <xref:Metalama.Framework.Project.ProjectExtension> and has a default constructor.
2. Optionally, override the <xref:Metalama.Framework.Project.ProjectExtension.Initialize*> method, which receives the <xref:Metalama.Framework.Project.IProject>.
3. In your aspect code, call the [IProject.Extension\<T>()](xref:Metalama.Framework.Project.IProject.Extension*) method, where `T` is your configuration class, to get the configuration object.
4. Optionally, create an extension method for the <xref:Metalama.Framework.Project.IProject> type to expose your configuration API, making it more discoverable. The class must be annotated with `[CompileTime]`.
5. To configure your aspect, users should implement a project fabric and access your configuration API using this extension method.

## Example

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AspectConfiguration.cs name="Consuming Property"]
