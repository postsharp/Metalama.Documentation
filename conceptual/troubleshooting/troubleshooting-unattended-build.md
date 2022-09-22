---
uid: troubleshooting-unattended-build
---

# Troubleshooting unattended build

This article describes steps to enable logging and process dumps on an unattended build on a build server without having to install `metalama-config` tool.


## Step 1. Install metalama-config locally

Install `metalama-config` as described in <xref:dotnet-tool> on your local device.

## Step 2. Edit diagnostics.json

Execute the command:

```
metalama-config diag edit
```

This should open a `diagnostics.json` file in your default editor.

1. To set up logging, edit the file accordingly following steps in <xref:creating-logs> article.
2. To set up process dumps, edit the file accordingly following steps in <xref:process-dump> article.

In the next example you can find entire resulting `diagnostics.json` file after finishing editting it.
- Logging is enabled for the compiler process and for all categories.
- Metalama is configured to capture a process dump for the compiler process.


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
    "stopLoggingAfterHours": 2.0
  },
  "debugger": {
    "processes": {
      "Other": false,
      "Compiler": false,
      "DevEnv": false,
      "RoslynCodeAnalysisService": false,
      "Rider": false,
      "BackstageWorker": false,
      "MetalamaConfig": false,
      "TestHost": false
    }
  },
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
      "WithoutAuxiliaryState"
    ],
    "ExceptionTypes": [
      "*"
    ]
  }
}
```

## Step 3. Copy the diagnostics configuration to environment variable

Having the logging and process dumps options set in `diagnostics.json`, copy the entire content of the file and set the copied content as a value of environment variable called `METALAMA_DIAGNOSTICS`. Pass this environment variable to the build server.

> [!WARNING]
> Using diagnostics set by environment variable always overrides local diagnostics settings used by `metalama-config` tool. The `stopLoggingAfterHours` property is also ignored and therefore logging will never be disabled autoamtically.

## Step 4. Run the build on build server

Metalama will automatically read the diagnostics configuration from environment variable,
You will find the log under the `%TEMP%\Metalama\Logs` directory.