---
uid: dotnet-tool
---

# Installing the Metalama Command-Line Tool

In order to register a license key or access notifications, you need to install Metalama Command-Line Tool, an extension to the `dotnet` command-line utility.

To install Metalama Command-Line Tool, execute the following at the command prompt:

1. While Metalama is in preview, review the latest version number of the [Metalama.Tool](https://www.nuget.org/packages/Metalama.Tool) package on on Nuget
2. Execute the following at the command prompt:

    ```
    dotnet tool install -g metalama.tool -version <VERSION>
    ```

    where `<VERSION>` is the version found in the first step.

Metalama Command-Line Tool is now available through the `metalama` command.

