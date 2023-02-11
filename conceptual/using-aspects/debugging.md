---
uid: debugging
---

# Debugging code with aspects

When you debug code that uses Metalama, by default, the debugger shows you the original code, without the modifications applied by Metalama. This is convenient when you are using an existing aspect, but when developing an aspect, you want to be able to see the transformed code.

There are two ways to debug the transformed code.


## Option 1: Using the LamaDebug build configuration

The easiest way to debug the transformed code (instead of the source code) is to switch to the build configuration named `LamaDebug`. This build configuration -
along with the default `Debug` and `Release` configurations -
is automatically defined for any project that imports the [_Metalama.Framework_](https://www.nuget.org/packages/Metalama.Framework) package.

To create a `LamaDebug` build configuration for your whole solution using Visual Studio:

1. In the _Build_ menu, choose _Configuration Manager_.
2. In the _Active solution configuration_ list box, choose _\<New...>_.
3. Fill this dialog as follows:

    * Name: `LamaDebug` (or your own name)
    * Copy settings from: `<Empty>`
    * Create new project configurations: `No`

4. Then, make sure that the solution configuration uses the project configuration named `LamaDebug` for each project that uses Metalama.

    ![Screenshot](LamaDebugConfigurationManager.png)

The benefit of this approach is that you can easily switch between source code and transformed code debugging by switching between the `Debug` and `LamaDebug` build configurations.


## Option 2: Setting properties manually

Alternatively, set the following MSBuild properties in your project file:

```xml
<PropertyGroup>
    <MetalamaFormatOutput>True</MetalamaFormatOutput>
    <MetalamaDebugTransformedCode>True</MetalamaDebugTransformedCode>
    <MetalamaEmitCompilerTransformedFiles>True</MetalamaEmitCompilerTransformedFiles>
</PropertyGroup>
```

For details about these properties, see <xref:msbuild-properties>.


> [!div class="see-also"]
> <xref:debugging-aspects>

