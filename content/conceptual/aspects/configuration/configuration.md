---
uid: aspect-configuration
---


# Making aspects configurable

Complex and widely-used aspects often require a centralized, project-wide method for configuring their compile-time behavior. For aspects, accepting compile-time configuration has the following benefits:


* **Centralized aspect options**. Providing a configuration API allows the entire project, namespaces or class families to be configured from a single location. Without a configuration API, users must supply the configuration each time a custom attribute is used.

* **Run-time performance**. By taking into account compile-time options, your aspect can generate optimal run-time code, which results in higher run-time performance of your application.

There are two complementary mechanisms for configuration: the configuration API and MSBuild properties.

> [!NOTE]
> Compile-time configuration has significnatly changed with Metalama 2023.4. If you are looking for the previous configuration API, see <xref:exposing-configuration-before-2023-4>

## In this chapter

| Article | Description |
|---------|-------------|
| <xref:exposing-options> | This article explains how to build a programmatic configuration API that can be called from fabrics. |
| <xref:reading-msbuild-properties> | This article describes how your aspect can consume MSBuild property. |
| <xref:configuration-custom-merge> | This article explains in detail the options merging process, and how to customize it. |

