---
uid: process-dump
level: 200
---

# Creating a process dump

If you are experiencing issues with Metalama, our support team may ask you to provide a process dump of the compiler or IDE process.

> [!WARNING]
> **Process dumps contain possibly confidential information**
>
> Process dumps may contain a copy of your source code. Although we will handle process dumps as confidential material, your company may not allow you to send us a process dump.

## Step 1. Install the PostSharp Command-Line Tool

Install the `metalama` command-line tool as described in <xref:dotnet-tool>.

## Step 2. Install the `dotnet dump` tool

Execute the following command:

```powershell
dotnet tool install --global dotnet-dump
```

## Step 3. Edit diagnostics.json

Execute the command:

```powershell
metalama config edit diagnostics
```

This should open a `diagnostics.json` file in your default editor.

The `miniDump/processes` section lists processes for which process dumps need to be collected. The values are `false` by default. Set the values to `true` if you want to collect the process dumps of the following processes if they crash:

* `Compiler`: the compile-time process.
* `Rider`: the design-time Roslyn process running under Rider.
* `DevEnv`: the UI process of Visual Studio (note that there is no aspect code running in this process).
* `RoslynCodeAnalysisService`: the design-time Roslyn process running under Visual Studio (this is where the aspect code runs).

In the next example, Metalama is configured to capture a process dump for the compiler process.


```json
{
 // ...
"crashDumps": {
    "processes": {
     "DotNetTool": false,
      "BackstageWorker": false,
      "OmniSharp": false,
      "Compiler": true,
      "TestHost": false,
      "CodeLensService": false,
      "Other": false,
      "ResharperTestRunner": false,
      "DevEnv": false,
      "Rider": false,
      "RoslynCodeAnalysisService": false
    }
//...
}

```

## Step 4. Execute Metalama

Restart the logged processes:

 * If you decided to log the `Compiler` process, restart the Roslyn compiler processes using `metalama kill`.
 * If you decided to log any design-time processes, restart your IDE.

Execute the actions that trigger the issue.

> [!WARNING]
> Do not forget to disable the diagnostic setting after you are done.

## Step 5. Upload the process dump to an online drive

You will find process dumps under the `%TEMP%\Metalama\CrashReports` directory with extension `*.dmp.gz`.
Upload this file to an online storage service like OneDrive.

## Step 6. Send us the URL through a private channel

> [!WARNING]
> **NEVER** share the process dump URL publicly on a service like GitHub Issues.

Instead, send us the hyperlink by [email](mailto:hello@postsharp.net).

