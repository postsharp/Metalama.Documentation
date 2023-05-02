---
uid: creating-logs
---

# Enabling logging

If you report a Metalama bug, you may be asked to send Metalama log files. These instructions teach you how to produce them.

## Step 1. Install the Metalama CLI tool

Install the `metalama` .NET tool as described in <xref:dotnet-tool>.

## Step 2. Edit diagnostics.json

Execute the following command:

```
metalama config edit diagnostics
```

This should open a `diagnostics.json` file in your default editor. Edit this file as follows:

1. In the `logging/processes` section, set to `true` the processes for which logging should be enabled:
    * `Compiler`: the compile-time process.
    * `Rider`: the design-time Roslyn process of the Rider IDE.
    * `DevEnv`: the UI process of Visual Studio (note that there is no aspect code running in this process).
    * `RoslynCodeAnalysisService`: the design-time Roslyn process running under Visual Studio (this is where the aspect code runs).
2. In the `logging/trace` section, set to `true` the categories for which logging should be enabled. To enable logging for everything, allow the `*` category.

In the following example, logging is enabled for the compiler process for all categories.


```json
{
    "logging": {
        "processes": {
                "Other": false,
                "Compiler": true,
                "DevEnv": false,
                "RoslynCodeAnalysisService": false,
                "Rider": false,
                "BackstageWorker": false,
                "MetalamaConfig": false,
                "TestHost": false
      },
    "trace": {
        "*": false
      },
    "stopLoggingAfterHours": 5.0
  }
}
```

To verify that the JSON file is correct, you can run the following:

```powershell
metalama config validate diagnostics
```

## Step 3. Restart processes

Diagnostic settings are cached in all processes, including background compiler processes and IDE helper processes.

To restart background compiler processes, do the following:

```powershell
metalama kill
```

If you want to change the logging configuration of the IDE processes, restart your IDE manually.


## Step 4. Execute Metalama

Execute the sequence of actions to be logged.

> [!WARNING]
> Logging is automatically disabled after a specified number of hours since the last modification of `diagnostics.json`. The time value is taken from the `stopLoggingAfterHours` property in the `logging` section and defaults to 2 hours. You can edit `diagnostics.json` to change the value.

## Step 5. Open the log file

You will find the log under the `%TEMP%\Metalama\Logs` directory.

