---
uid: templates
---

## T# Templates

Aspects and aspect attributes are implemented using a templating language. Their unobtrusive syntax does not come straight from the compiler, but rather from the "aspect compiler". We call this language T#, because its is syntactically a superset of C#.

However, templates are compiled differently, therefore the T# language has very specific requirements in order to obtain predictable results. While any snippet of code can be imported from C# and will compile and execute, this compilation is somewhat obscure. For example, generic types must be fully parameterized and cannot be inferred.

This chapter contains the following articles:

<table>
    <tr>
        <th>Article</th>
        <th>Description</th>
    <tr>
    <tr>
        <td>
            <xref:template-overview>
        </td>
        <td>
            This article introduces T#, the template language for Metalama.
        </td>
    </tr>
    <tr>
        <td>
            <xref:template-compile-time>
        </td>
        <td>
            This article describes which subset of the C# language can be used as compile-time code, and how templates with rich compile-time logic can be built.
        </td>
    </tr>
    <tr>
        <td>
            <xref:template-dynamic-code>
        </td>
        <td>
            This article explains different techniques to generate run-time code dynamically.
        </td>
    </tr>
    <tr>
        <td>
            <xref:template-parameters>
        </td>
        <td>
            This article explains how to pass parameters (including generic parameters) from the `BuildAspect` method to the template.
        </td>
    </tr>
    <tr>
        <td>
            <xref:reflection>
        </td>
        <td>
            This article explains how to generate run-time `System.Reflection` objects for compile-time `Metalama.Framework.Code` objects from a template.
        </td>
    </tr>
    <tr>
        <td>
            <xref:debugging-aspects>
        </td>
        <td>
            This article explains how to debug templates.
        </td>
    </tr>
</table>

