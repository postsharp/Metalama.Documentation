---
uid: process-dump
---

# Creating a process dump

If you report a Metalama bug, when logs are not enough to understand the issue, our team may ask you to send a process dump of the compiler or IDE process.

> [!WARNING]
> Process dumps may contain a copy of your source code. Although we will handle process dumps as confidential material, your company may not allow you to send us a process dump.

If you are using Metalama on build server, the process of enabling process dumps is described in <xref:troubleshooting-unattended-build>.

## Step 1. Install the metalama tool

Install the `metalama` tool as described in <xref:dotnet-tool>.

## Step 2. Edit diagnostics.json

Execute the command:

```
metalama diag edit
```

This should open a `diagnostics.json` file in your default editor.

In the `miniDump/processes` section, set to `true` the processes for which logging should be enabled:
* `Compiler`: the compile-time process.
* `Rider`: the design-time Roslyn process running under Rider.
* `DevEnv`: the UI process of Visual Studio (note that there is no aspect code running in this process).
* `RoslynCodeAnalysisService`: the design-time Roslyn process running under Visual Studio (this is where the aspect code runs).

In the next example, Metalama is configured to capture a process dump for the compiler process.


```json
{
  "miniDump": {
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
    "flags": [
      "WithDataSegments",
      "WithProcessThreadData",
      "WithHandleData",
      "WithPrivateReadWriteMemory",
      "WithUnloadedModules",
      "WithFullMemoryInfo",
      "WithThreadInfo",
      "FilterMemory",
      "WithoutAuxiliaryState",
      "IgnoreInaccessibleMemory"
    ],
    "ExceptionTypes": [
      "*"
    ]
  }
}
```

## Step 3. Execute Metalama

Restart the logged processes:

 * If you are logging the `Compiler` process, restart the Roslyn compiler processes.
 * If you are logging any design-time process, restart the IDE.

Execute the sequence of actions that triggers the issue.

> [!WARNING]
> Do not forget to disable the setting after you are done.

## Step 4. Upload the process dump to an online drive

You will find the process dumps under the `%TEMP%\Metalama\CrashReports` directory, with extension `*.dmp.gz`.
Upload this file to an online storage service like OneDrive.

## Step 5. Send us the URL through a private channel

NEVER share the process dump URL publicly on a service like GitHub Issues.

Instead, send us the hyperlink by [email](mailto:hello@postsharp.net).

