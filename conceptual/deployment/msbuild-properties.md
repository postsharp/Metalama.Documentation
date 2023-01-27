---
uid: msbuild-properties
---

# MSBuild Configuration Properties and Items

## Properties

| Property                        | Type | Description
|---------------------------------|-----|-----------------------------------------
| `MetalamaCompilerTransformerOrder` |  Semicolon-separated list  | Specifies the execution order of transformers in the current project. Transformers are identified by their namespace-qualified type name, but without the assembly name. This property is generally not important because the only transformer is typically provided by _Metalama.Framework_, as users extend _Metalama.Framework_ instead of directly extending _Metalama.Compiler_.
| `MetalamaDebugTransformedCode`  |  Boolean | Determines if you want to debug the _transformed_ code instead of the _source_ code. The default value is `False`.
| `MetalamaEmitCompilerTransformedFiles` | Boolean | Determines if Metalama.Compiler should write the transformed code files to disk. The default is `True` if `MetalamaDebugTransformedCode` is enabled and `False` otherwise.
| `MetalamaCompilerTransformedFilesOutputPath` | Path | Path of the directory where the transformed code files are written. The default is `obj/$(Configuration)/metalama`.
| `MetalamaDebugCompiler` | Boolean | Specifies that you want to attach a debugger to the compiler process. The default value is `False`
| `MetalamaLicense` | String | A Metalama license key or license server URL. License key or license server URL provided this way takes precedence over license registered via the `metalama` global tool.
| `MetalamaEnabled`               | Boolean |  When set to `False`, specifies that _Metalama.Framework_ should not execute in this project, although the _Metalama.Framework_ package is referenced in the project. It does not affect the _Metalama.Compiler_ package.
| `MetalamaFormatOutput`          | Boolean | Determines if the transformed code should be nicely formatted (`True`), `False` or undefined if formatting should be skipped. Formatting the transformed code has a performance overhead and should be only performed when the code will be troubleshooted or exported. The default value is `False`.
| `MetalamaFormatCompileTimeCode` | Boolean | Determines if the compile-time code should be nicely formatted. Formatting the compile-time code has a performance overhead and should be only performed when the code will be troubleshooted or exported. The default value is `False`.
| `MetalamaCompileTimeProject`    | Boolean | Specifies that the complete project is compile-time code. This property is set to `True` by the _Metalama.Framework.Sdk_ package. Otherwise, the default value is `False`.
| `MetalamaDesignTimeEnabled`     | Boolean | Determines if the real-time design-time experience is enabled. The default value is `True`. It can be set to `False` to work around performance issues. When this property is set to `False`, refreshing Intellisense requires you to rebuild the project.
| `MetalamaRemoveCompileTimeOnlyCode` | Boolean | Determines if Metalama should replace compile-time-only code by `throw NotSupportedException()` in produced assemblies. The default value is `True` because Metalama normally executes compile-time-only code from the compile-time sub-project embedded in the assembly as a managed resource. This property should be set to `False` in public assemblies that are referenced by a weaver-style project (using Metalama SDK), because Metalama SDK needs to execute compile-time-only code from the main assembly. |

## Items

| Item | Description 
|------|------------
| `MetalamaTransformedCodeAnalyzer` | List of analyzers that must execute on the transformed code instead of the source code. Items can be set to a namespace or a full type name.
| `MetalamaCompileTimePackage` | List of packages that are made accessible from the compile-time code. These packages must explicitly target .NET Standard 2.0 and must be included in the project as a `ProjectReference`.
