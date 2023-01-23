---
uid: dotnet-tool
---

# Installing Metalama Command Line Tools

In order to register a license key or access some configuration settings, you need to install Metalama Command-Line Tools, an extension to the `dotnet` command-line utility.

In order to install Metalama Command Line Tools:

1. While Metalama is in preview, check the latest version of the `Metalama.Tool` package on [nuget.org](https://www.nuget.org/packages/Metalama.Tool).
2. Execute this at the command prompt:

    ```
    dotnet tool install -g metalama.tool -version <VERSION>
    ```

    where `<VERSION>` is the version found in the first step.

Once the tool is installed, you can access it from the command line using the `metalama.tool` command.