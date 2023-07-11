---
uid: dotnet-tool
level: 200
---

# Installing the Metalama Command Line Tool

The Metalama Command Line Tool provides the following features:

* Installing a license or switching to Metalama Free,
* Configuring various settings,
* Terminating Metalama processes,
* Cleaning up Metalama temporary files.
* Inspecting and summarizing the use of license credits.

To install the Metalama Command Line Tool, follow these steps:

1. While Metalama is in preview, check the latest version number of the [Metalama.Tool](https://www.nuget.org/packages/Metalama.Tool) package on NuGet.
2. Run the following command at the command prompt, replacing `<VERSION>` with the version number you obtained in the first step.

    ```powershell
    dotnet tool install -g metalama.tool
    ```

The Metalama Command Line Tool is now accessible through the `metalama` command.
