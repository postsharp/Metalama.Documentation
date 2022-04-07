---
uid: Metalama.Framework.CodeFixes
summary: *content
---

This namespace allows you to suggest code fixes and code refactorings, i.e. changes to source code that appear in the lightbulb or screwdriver menu of your IDE.

Code fixes can be instantiated using the static methods of the <xref:Metalama.Framework.CodeFixes.CodeFixFactory> class.

To add code fixes to a diagnostic, use the <xref:Metalama.Framework.Diagnostics.IDiagnostic.WithCodeFixes*?text=IDiagnostic.WithCodeFixes> method.

To suggest a code refactoring without reporting a diagnostic, use the <xref:Metalama.Framework.Diagnostics.IDiagnosticSink.Suggest*>text=IDiagnosticSink.Suggest> method.

## Class diagram

```mermaid
classDiagram

    class IDiagnosticSink {
        Report()
        Suppress()
        Suggest()
    }

    class IDiagnostic {
        CodeFixes
        WithCodeFixes(CodeFixes[])
    }

  class CodeFix {
        Id
        Title
    }
    class CodeFixFactory {
        ApplyAspect()$
        AddAttribute()$
        RemoteAttribute()$
        CreateCustomCodeFix(Func~ICodeFixBuilder,Task~)$
    }
    class ICodeFixBuilder {
       ApplyAspectAsync()
       AddAttributeAsync()
       RemoteAttributeAsync()
    }

    CodeFixFactory --> ICodeFixBuilder : custom code fixes\nimplemented with
    CodeFixFactory --> CodeFix : creates
    CodeFix --> IDiagnostic: add to
    CodeFix --> IDiagnosticSink: suggest to
    IDiagnostic --> IDiagnosticSink: reports to
```

## Namespace members