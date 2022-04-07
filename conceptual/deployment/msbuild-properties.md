---
uid: msbuild-properties
---

# MSBuild Configuration Properties


| Property                        | Type | Description
|---------------------------------|-----|-----------------------------------------
| `MetalamaCompilerTransformerOrder` |  Semicolon-separated list  | Specifies the execution order of transformers in the current project. Transformers are identified by their namespace-qualified type name, but without the assembly name. This property is generally not important because the only transformer is typically provided by _Metalama.Framework_, as user extend _Metalama.Framework_ instead of directly _Metalama.Compiler_.
| `MetalamaDebugTransformedCode`  |  Boolean | Determines if you want to debug the _transformed_ code instead of the _source_ code. The default value is `False`.
| `MetalamaEmitCompilerTransformedFiles` | Boolean | Determines if Metalama.Compiler should write the transformed code files to disk. The default is `True` if `MetalamaDebugTransformedCode` is enabled and `False` otherwise.
| `MetalamaCompilerTransformedFilesOutputPath` | Path | Path of the directory where the transformed code files are written. The default is `obj/$(Configuration)/metalama`.
| `MetalamaDebugCompiler` | Boolean | Specifies that you want to attach a debugger to the compiler process. The default value is `False`
| `MetalamaSourceOnlyAnalyzers` | Comma-separated list | List of analyzers that must execute on the source code instead of the transformed code. Each item in the comma-separated list can contain the assembly name, an exact namespace (namespace inheritance rules do not apply) or the exact full name of an analyzer type.
| `MetalamaLicense` | Semicolon-separated list | A semicolon-separated list of Metalama license keys or license server URLs.
| `MetalamaEnabled`               | Boolean |  When set to `False`, specifies that _Metalama.Framework_ should not execute in this project, although the _Metalama.Framework_ package is referenced in the project. It does not affect the _Metalama.Compiler_ package.
| `MetalamaFormatOutput`          | Boolean | Determines if the transformed code should be nicely formatted, `False` or undefined if formatting should be skipped. Formatting the transformed code has a performance overhead and should be only performed when the code will be troubleshooted or exported. The default value is `False`.
| `MetalamaFormatCompileTimeCode` | Boolean | Determines if the compile-time code should be nicely formatted. Formatting the compile-time code has a performance overhead and should be only performed when the code will be troubleshooted or exported. The default value is `False`.
| `MetalamaCompileTimeProject`    | Boolean | Specifies that the complete project is compile-time code. This property is set to _True_ by the _Metalama.Framework.Sdk_ package. Otherwise, the default value is `False`.
| `MetalamaDesignTimeEnabled`     | Boolean | Determines if the real-time design-time experience is enabled. The default value is `True`. It can be set to `False` to work around performance issues. When this property is set to `False`, refreshing Intellisense requires you to rebuild the project.
