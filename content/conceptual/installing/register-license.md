---
uid: register-license
level: 200
---

# Registering a license

When you use Metalama for the first time, it will automatically activate the trial mode, which will work for 45 days. To use Metalama beyond this period, switch to Metalama Free or register a license key.

You can use all features of any preview of Metalama for up to 45 days after its release without registering a license.

## Switching to Metalama Free

To switch to the Metalama Free license:

1. Install Metalama Command Line Tools as described in <xref:dotnet-tool>.
2. Execute the following command:

   ```powershell
   metalama license free
   ```

## Registering a license key

To register a license key for the current user:

1. Install Metalama Command Line Tools as described in <xref:dotnet-tool>.
2. Execute the following command:

   ```powershell
   metalama license register <LICENSE KEY>
   ```

## Registering a license key manually

To register a license manually for the current user:

1. Open the Metalama licensing JSON configuration file `licensing.json`. It is located in the `%appdata%\\.metalama` directory on Windows and in the `~/.metalama` directory on Linux and Mac.
2. Set the license key as the `license` value. If the file doesn't exist, it should contain the following content. (`123-ABCDEFGHIJKLMNOPQRSTUVXYZ` is a placeholder for the actual license key.)

   ```json
   {
     "license": "123-ABCDEFGHIJKLMNOPQRSTUVXYZ"
   }
   ```

## Registering a license key using an environment variable or MSBuild

The license key can be stored as the value of the `MetalamaLicense` MSBuild property. This allows for the following:

- Configuring the license as a value of the `MetalamaLicense` environment variable.
- Configuring the license in the source code repository using [Directory.Build.props](https://learn.microsoft.com/en-us/visualstudio/msbuild/customize-your-build?view=vs-2022#directorybuildprops-and-directorybuildtargets) file.
- Configuring the license in the `.csproj` project file.

