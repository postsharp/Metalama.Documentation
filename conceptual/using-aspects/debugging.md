---
uid: debugging
---

# Debugging Code With Aspects

When debugging code that uses Metalama, by default, the debugger only shows you your original code, without Metalama-applied modifications. This is convenient when you're using an existing aspect, but when you're developing an aspect, you want to be able to see the transformed code. Metalama offers several options for that, set from a `<PropertyGroup>` section inside your `.csproj` or `Directory.Build.props` file:


| Property Name                          | Description                                                                                                                                                                                  |
|----------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `MetalamaEmitCompilerTransformedFiles` | Setting this property to `True` means that the transformed code will be written to disk, to the `obj/$Configuration/$Framework/transformed` directory (e.g. `obj/Debug/net5.0/transformed`). |
| `MetalamaDebugTransformedCode`         | Setting this property to `True` means transformed code will be used when debugging and when producing errors and warnings.                                                                   |

> [!div class="see-also"]
> <xref:debugging-aspects>