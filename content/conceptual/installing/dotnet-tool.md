---
uid: dotnet-tool
level: 200
---

# Installing the Metalama Command Line Tool

The Metalama Command Line Tool offers the following features:

* install a license or switch to Metalama Free,
* configure different settings,
* kill Metalama processes,
* clean up Metalama temporary files.

To install Metalama Command Line Tool:

1. While Metalama is in preview, review the latest version number of the [Metalama.Tool](https://www.nuget.org/packages/Metalama.Tool) package on NuGet.
2. Execute the following at the command prompt, replacing `<VERSION>` with the version number you found in the first step.

    ```powershell
    dotnet tool install -g metalama.tool
    ```

The Metalama Command Line Tool is now available through the `metalama` command.
