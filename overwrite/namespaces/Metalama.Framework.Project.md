---
uid: Metalama.Framework.Project
summary: *content
---

This namespace serves the following purposes:

* It provides read access to the project configuration, which encompasses project references, preprocessor symbols, build properties, and other related information. Refer to the <xref:Metalama.Framework.Project.IProject> interface for more details. This interface can be accessed from any code element through the <xref:Metalama.Framework.Code.ICompilation.Project?text=ICompilation.Project> property.

* It enables the implementation of a configuration API for your aspect library. Refer to <xref:exposing-configuration> for more details.

* It discloses information about the current execution context via the <xref:Metalama.Framework.Project.MetalamaExecutionContext.Current?text=MetalamaExecutionContext.Current> property.

* It reveals the service provider, which facilitates access from high-level code to the low-level plugins.

## Conceptual documentation

Refer to <xref:exposing-configuration>.

## Class diagram

```mermaid
classDiagram

    class ICompilation {
        Project
    }

    class IProject {
        AssemblyReferences
        Configuration
        Path
        PreprocessorSymbols
        ServiceProvider
        TargetFramework
        TryGetProperty()
        Extension< T >()
    }

    class IExecutionContext {
        Compilation
        ExecutionScenario
        FormatProvider
        ServiceProvider
    }

    class MetalamaExecutionContext {
        Current
    }

    class ProjectExtension {
        IsReadOnly
        Initialize()
        MakeReadOnly()
    }

    class IServiceProvider {
        GetService()
    }

    class IService {

    }

    class IExecutionScenario {
        Name
        IsDesignTime
        CapturesCodeFixImplementations
        CapturesCodeFixTitles
        CapturesNonObservableTransformations
    }

    IProject <-- ICompilation : exposes
    IServiceProvider <-- IProject: exposes
    ProjectExtension <-- IProject: exposes
    IExecutionContext <-- MetalamaExecutionContext : exposes
    IServiceProvider <-- IExecutionContext: exposes
    IExecutionScenario <-- IExecutionContext: exposes
    IService <-- IServiceProvider: provides

```

## Namespace members

