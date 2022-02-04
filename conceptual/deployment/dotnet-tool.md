---
uid: dotnet-tool
---

# Installing Metalama Command Line Tools

In order to register a license key or access some configuration settings, you ned to install Metalama Command-Line Tools, an extension to the `dotnet` command-line utility.

In order to install Metalama Command Line Tools, execute this at the command prompt:

```
dotnet tool install -g metalama-config [-version <VERSION>]
```

The `-version <VERSION>` option is required to install a pre-release version of the package.

Once the tool is installed, you can access it from the command line using the `metalama-config` command.