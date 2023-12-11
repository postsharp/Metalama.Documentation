---
uid: register-license
level: 200
summary: "Metalama automatically activates a 45-day trial upon first use. Users can switch to Metalama Free or register a license key to continue usage beyond the trial period."
---

# Registering a license

When you use Metalama for the first time, it will automatically activate the trial mode, which will work for 45 days. To use Metalama beyond this period, switch to Metalama Free or register a license key.

You can use all features of any preview of Metalama for up to 45 days after its release without registering a license.

## Switching to Metalama Free

To transition to the Metalama Free license, follow the steps below:

1. Install the Metalama Command Line Tools as detailed in <xref:dotnet-tool>.
2. Run the following command:

   ```powershell
   metalama license free
   ```

## Registering a license key

To register a license key for the current user, perform the following steps:

1. Install the Metalama Command Line Tools as outlined in <xref:dotnet-tool>.
2. Run the following command:

   ```powershell
   metalama license register <LICENSE KEY>
   ```

## Registering a license key manually

To manually register a license for the current user, do the following:

1. Open the Metalama licensing JSON configuration file `licensing.json`. This file is located in the `%appdata%\\.metalama` directory on Windows, and in the `~/.metalama` directory on Linux and Mac.
2. Set the license key as the `license` value. If the file doesn't exist, it should contain the following content (where `123-ABCDEFGHIJKLMNOPQRSTUVXYZ` is a placeholder for the actual license key):

   ```json
   {
     "license": "123-ABCDEFGHIJKLMNOPQRSTUVXYZ"
   }
   ```

## Registering a license key using an environment variable or MSBuild

The license key can be stored as the value of the `MetalamaLicense` MSBuild property. This allows for the following:

- Storing the license as a value of the `MetalamaLicense` environment variable.
- Storing the license in the source code repository using the [Directory.Build.props](https://learn.microsoft.com/en-us/visualstudio/msbuild/customize-your-build?view=vs-2022#directorybuildprops-and-directorybuildtargets) file.
- Storing the license in the `.csproj` project file.


