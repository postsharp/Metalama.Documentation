---
uid: dotnet-tool
level: 200
summary: "This document provides instructions on how to install the Metalama Command Line Tool, which offers several features for managing the Metalama software."
keywords: "Metalama Command Line Tool, install Metalama, managing Metalama software, configuring settings, terminating processes, cleaning temporary files, inspecting license usage, .NET, dotnet tool install, Metalama.Tool package."
---

# Installing the Metalama Command Line Tool

The Metalama Command Line Tool provides the following features:

* Installing a license or switching to Metalama Free.
* Configuring various settings.
* Terminating Metalama processes.
* Cleaning up Metalama temporary files.
* Inspecting and summarizing license usage limits.

To install the Metalama Command Line Tool, follow these steps:

1. While Metalama is in preview, check the latest version number of the [Metalama.Tool](https://www.nuget.org/packages/Metalama.Tool) package on NuGet.
2. Run the following command at the command prompt:

    ```powershell
    dotnet tool install -g metalama.tool
    ```

The Metalama Command Line Tool is now accessible through the `metalama` command.

