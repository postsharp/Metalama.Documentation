---
uid: register-license
---

# Registering a license

When you use Metalama for the first time it will automatically activate the trial mode which will work for a period of 45 days. To use Metalama beyond this period you will need to switch to Metalama Free or register a license key.

You can use all features of any preview of Metalama for up to 45 days after release without having any license registered.

## Switching to Metalama Free

To switch to the Metalama Free license:

1. Install Metalama Command Line Tools as described in <xref:dotnet-tool>.
2. Execute the following command:

   ```
   metalama license register free
   ```

## Registering a license key

To register a license for the current user:

1. Install Metalama Command Line Tools as described in <xref:dotnet-tool>.
2. Execute the following command:
   
   ```
   metalama license register <LICENSE KEY>
   ```

## Registering a license key manually

To register a license for a user manually:

1. Open the Metalama licensing JSON cofiguration file `licensing.json`. It is located in `%appdata%\.metalama` directory on Windows and in `~/.metalama` directory on Linux and Mac.
2. Set the license key as the `license` value. If the file doesn't exist, it should contain the following content. (`123-ABCDEFGHIJKLMNOPQRSTUVXYZ` is a placeholder for the actual license key.)

   ```
   {
     "license": "123-ABCDEFGHIJKLMNOPQRSTUVXYZ"
   }
   ```

## Registering a license key via environment variable or MSBuild

The license key can be stored as a value of the `MetalamaLicense` MSBuild property. This allows for:

- Configuring the license as a value of the `MetalamaLicense` environment variable.
- Configuring the license in the source code repository using [Directory.Build.props](https://learn.microsoft.com/en-us/visualstudio/msbuild/customize-your-build?view=vs-2022#directorybuildprops-and-directorybuildtargets) file.
- Configuring the license in the `.csproj` project file.