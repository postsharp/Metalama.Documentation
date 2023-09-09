---
uid: templates
level: 200
---

# Writing T# templates

Code templates in Metalama are written in a dialect of C#, known as T#. The syntax of T# is fully compatible with C#, but T# is compiled differently.

This chapter includes the following articles:

<table>
    <tr>
        <th>Article</th>
        <th>Description</th>
    </tr>
    <tr>
        <td>
            <xref:template-overview>
        </td>
        <td>
            This article provides an introduction to T#, the template language for Metalama.
        </td>
    </tr>
    <tr>
        <td>
            <xref:template-compile-time>
        </td>
        <td>
            This article outlines the subset of the C# language that can be used as compile-time code and illustrates how to create templates with rich compile-time logic.
        </td>
    </tr>
    <tr>
        <td>
            <xref:template-dynamic-code>
        </td>
        <td>
            This article details different techniques for generating run-time code dynamically.
        </td>
    </tr>
    <tr>
        <td>
            <xref:reflection>
        </td>
        <td>
            This article clarifies how to generate run-time `System.Reflection` objects for compile-time `Metalama.Framework.Code` objects from a template.
        </td>
    </tr>
    <tr>
        <td>
            <xref:template-parameters>
        </td>
        <td>
            This article describes how to pass parameters, including generic parameters, from the `BuildAspect` method to the template.
        </td>
    </tr>
    <tr>
        <td>
            <xref:auxiliary-templates>
        </td>
        <td>
            This article explains how templates can invoke other templates, referred to as auxiliary templates.
        </td>
    </tr>

    <tr>
        <td>
            <xref:debugging-aspects>
        </td>
        <td>
            This article provides guidance on how to debug templates.
        </td>
    </tr>
</table>
