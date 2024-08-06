---
uid: troubleshooting-unattended-build
summary: "The document provides instructions on how to enable logging and process dumps for an unattended build on a build server without installing the 'metalama' tool."
---

# Troubleshooting an unattended build

This article describes the process of enabling logging and processing dumps in an unattended build on a build server without installing the `metalama` tool.

## Step 1. Create diagnostics.json on your local machine

You can refer to the other articles in this chapter to learn how to create a `diagnostics.json` file for troubleshooting a specific scenario.

### Example: enabling logging

In the following example, we present the resultant `diagnostics.json` file after editing. Here, logging is enabled for the compiler process and all categories.

```json
{
  "logging": {
    "processes": {
      "Compiler": true
    },
    "categories": {
      "*": true
    }
  }
}
```

## Step 2. Copy diagnostics.json to the METALAMA_DIAGNOSTICS environment variable

In your build or pipeline configuration, create an environment variable named `METALAMA_DIAGNOSTICS` and set its value to the content of the `diagnostics.json` file.

> [!WARNING]
> Using diagnostics set by an environment variable always overrides local diagnostics settings used by the `metalama` tool.

## Step 3. Run the build on the build server

Metalama will automatically read the diagnostics configuration from the environment variable. The build will produce diagnostics based on the configuration set specified in the environment variable.

## Step 4. Download the logs

You can find the logs under the `%TEMP%\Metalama\Logs` directory.

