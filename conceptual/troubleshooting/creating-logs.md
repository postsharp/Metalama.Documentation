---
uid: creating-logs
---

# Enabling logging

If you report a Metalama bug, you may be asked to send Metalama logs. Here is how to produce them.


## Step 1. Install metalama-config

Install `metalama-config` as described in <xref:dotnet-tool>.

## Step 2. Edit diagnostics.json

Execute the command:

```
metalama-config diag edit
```

This should open a `diagnostics.json` file in your default editor. Edit this file as follows:

1. In the `logging/processes` section, set to `true` the processes for which logging should be enabled:
    * `Compiler`: the compile-time process.
    * `Rider`: the design-time Roslyn process running under Rider.
    * `DevEnv`: the UI process of Visual Studio (note that there is no aspect code running in this process).
    * `RoslynCodeAnalysisService`: the design-time Roslyn process running under Visual Studio (this is where the aspect code runs).
2. In the `logging/categories` section, set to `true` the categories for which logging should be enabled. To enable logging for everything, enable the `*` category.

In the next example, logging is enabled for the compiler process and for all categories.


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
    "categories": {
		"*": false
    },
    "stopLoggingAfterHours": 5.0
  }
}
```

## Step 3. Execute Metalama

Restart the logged processes:

 * If you are logging the `Compiler` process, restart the Roslyn compiler processes.
 * If you are logging any design-time process, restart the IDE.

Execute the sequence of actions to be logged.

> [!WARNING]
> Logging is automatically disabled after specified amount of hours since the last modification of `diagnostics.json`. The time value is taken from `stopLoggingAfterHours` property set in `logging` section and defaults to 2 hours. See the optional step below.

## Step 5. Open the log file

You will find the log under the `%TEMP%\Metalama\Logs` directory.

## Optional step: Changing the time before logging is disabled

You can change the amount of hours before logging is disabled by either directly editing the value of  `stopLoggingAfterHours` property or by executing the command to set the amount of hours:
```
metalama-config diag log <hours>
```