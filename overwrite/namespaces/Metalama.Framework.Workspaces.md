---
uid: Metalama.Framework.Workspaces
summary: *content
---

## Overview

This namespace allows you to load a C# project and solution into the <xref:Metalama.Framework.Code> code model from any application -- for instance from LinqPad.

The entry point of this namespace is the <xref:Metalama.Framework.Workspaces.WorkspaceCollection> class.

## Class Diagram

```mermaid
classDiagram

    class ICompilationSet {
        Compilations
        Types
        Methods
        Fields
        Properties
        FieldsAndProperties
        Constructors
        Events
        TargetFrameworks
    
    }

class IIntrospectionCompilationDetails {
    Diagnostics
    AspectClasses
    AspectLayers
    AspectInstances
    Advice
    Transformations
    IsMetalamaEnabled
    HasMetalamaSucceeded
}

    class IProjectSet {
    Projects
    GetSubset()
    GetDeclaration()
    }

    class ICompilationSetResult {
    
    }

IProjectSet --> ICompilationSet  : SourceCode
ICompilationSetResult --|> IIntrospectionCompilationDetails
ICompilationSetResult --> ICompilationSet  : TransformedCode


    IProjectSet --|>  ICompilationSet
    IProjectSet --|> ICompilationSetResult
    Project --|> IProjectSet
    Workspace  --|> IProjectSet
    WorkspaceCollection --* Workspace
    
    
    IProjectSet --* Project : Projects