---
uid: telemetry
---

# Telemetry

This article describes what is collected using telemetry and how you can disable it.

## What is being collected?

By default, Metalama will collect and send to PostSharp Technologies usage and quality reports. These reports are anonymous. They are collected in the following situations:

* In case of unexpected _failure_ or _performance degradation_ that is not caused by user code. In this case, we create an exception report including the anonymized call stack.
* Periodically, for each project you are building, we are collecting data such as a non-reversible hash of the project name, the target framework and version, the project size, number of aspects used, amount of code saved to Metalama, or performance.

All reports include a randomly generated device id, which you can reset at any time using Metalama Command Line Tools (see below).

Telemetry data is collected and processed according to our [Privacy Policy](https://www.postsharp.net/company/legal/privacy-policy).

### License audit

In addition to Telemetry, the use of the software may be subject to a _license audit_. License audit is anonymous but mandatory for Metalama Free and the self-generated Metalama Trial, and is used to estimate the number of users. When you are using a license key, license audit reports include the id of your license key. If you do not agree with license audit, please contact our sales team and we will provide you with a new license key including a license audit waiver flag.

## Disabling telemetry

There are two ways to disable telemetry.

### Option 1. Defining an environment variable

You can disable telemetry by defining the `METALAMA_TELEMETRY_OPT_OUT` environment to any non-empty value. 

Using this approach, you can easily disable telemetry for build agents, or you can disable telemetry for all devices in your domain using remote management tools such as Azure Endpoint Manager.

### Option 2. Using Metalama Command Line Tools

1. Install Metalama Command Line Tools as described in <xref:dotnet-tool>.
2. Execute the following commands:

   ```powershell
   metalama-config telemetry disable
   ```

## Resetting your device id

1. Install Metalama Command Line Tools as described in <xref:dotnet-tool>.
2. Execute the following commands:

   ```powershell
   metalama-config telemetry reset-device-id
   ```

