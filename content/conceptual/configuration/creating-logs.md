---
uid: creating-logs
summary: "The document provides instructions on how to generate log files for reporting Metalama bugs, including installing the CLI tool, editing diagnostics.json, restarting processes, executing Metalama, and accessing the log file."
---

# Enabling logging

When reporting a Metalama bug, it is often helpful to attach Metalama log files. This document provides instructions on how to generate these logs.

There are possible approaches: produce log files, or write the logging output to the console.

## Producing log files

### Step 1. Install the Metalama CLI tool

First, install the `metalama` .NET tool as outlined in <xref:dotnet-tool>.

### Step 2. Edit diagnostics.json

Run the following command:

```
metalama config edit diagnostics
```

This command will open a `diagnostics.json` file in your default editor. Make the following changes to this file:

1. In the `logging/processes` section, set the processes that should have logging enabled to `true`:
    * `Compiler`: This refers to the compile-time process.
    * `Rider`: This is the design-time Roslyn process of the Rider IDE.
    * `DevEnv`: This is the UI process of Visual Studio. Note that no aspect code runs in this process.
    * `RoslynCodeAnalysisService`: This is the design-time Roslyn process running under Visual Studio, where the aspect code runs.
2. In the `logging/trace` section, set the categories that should have logging enabled to `true`. To enable logging for all categories, set the `*` category to `true`.

The following example shows how to enable logging for the compiler process for all categories.

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

To validate the correctness of the JSON file, run the following command:

```powershell
metalama config validate diagnostics
```

### Step 3. Restart processes

Diagnostic settings are cached in all processes, including background compiler processes and IDE helper processes.

To restart background compiler processes, run the following command:

```powershell
metalama kill
```

If you need to alter the logging configuration of the IDE processes, you will need to manually restart your IDE.

### Step 4. Execute Metalama

Perform the sequence of actions that you wish to log.

> [!WARNING]
> Logging is automatically disabled after a certain number of hours following the last modification of `diagnostics.json`. The duration is specified in the `stopLoggingAfterHours` property in the `logging` section and defaults to 2 hours. To change this duration, you can edit the `diagnostics.json` file.

### Step 5. Open the log file

You can find the log in the `%TEMP%\Metalama\Logs` directory.


## Logging to the Console

To diagnose the build on build agents, the above procedure may be cumbersome because of the need to upload the logs from the build agent to some artifact repository.

It may be instead preferable to log directly to the console.

To enable console logging, set the `METALAMA_CONSOLE_TRACE` environment variable to `*` or to a comma-separated list of trace categories.

Note that `dotnet build` or `msbuild` process, as well as the Metalama compiler process, reuse background processes by default. These processes may fail to receive the `METALAMA_CONSOLE_TRACE` environment variable. To ensure that the Metalama compiler process receives the environment variable, you must disable build servers using the `--disable-build-servers` flag.

It is also important to enable detailed verbosity in `dotnet build` or `msbuild` because the default verbosity does not pass through the standard output of the compiler process.

### Example: PowerShell
Combining all these notes, here is how to enable console logging for all categories:

```powershell
$env:METALAMA_CONSOLE_TRACE="*"
dotnet build -t:rebuild --disable-build-servers -v:detailed
```

### Example: GitHub action

```yaml
name: Build and Test
on:
    push:
        branches:
        - master
env:
    METALAMA_CONSOLE_TRACE: '*'
jobs:
    build-and-test:
        strategy:
            fail-fast: false
            matrix:
                os: [ubuntu-latest, windows-latest, macos-latest]
                dotnet-version: ['8.x']
        runs-on: ${{ matrix.os }}
        name: Build and Test on ${{ matrix.os }} with .NET Core ${{ matrix.dotnet-version }}

        steps:
            - uses: actions/checkout@v4
            - name: Setup .NET Core
              uses: actions/setup-dotnet@v4
              with:
                  dotnet-version: ${{ matrix.dotnet-version }}
            - run: dotnet restore
            - run: dotnet build --configuration Debug --no-restore -v:detailed --disable-build-servers
            - run: dotnet test --configuration Release --no-restore -v:detailed --disable-build-servers 
```
