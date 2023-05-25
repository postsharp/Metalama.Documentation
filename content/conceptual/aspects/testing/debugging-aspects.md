---
uid: debugging-aspects
level: 300
---

# Debugging aspects

Debugging the compile-time logic of an aspect is currently difficult because the compiler does not execute your source code.  Instead, the transformed code is produced from your source code and stored under an unpredictable path.

> [!WARNING]
> Normal debugger breakpoints in aspects will not work. You must insert code that calls the debugger API.


## Debugging compile-time logic

To debug compile-time logic:

1. Inject breakpoints straight into your source code:

    - In a _build-time_ method such as `BuildAspect`, call <xref:System.Diagnostics.Debugger.Break?text=Debugger.Break()>.
    - In a _template_ method, call <xref:Metalama.Framework.Aspects.meta.DebugBreak?text=meta.DebugBreak()>.

2. Attach the debugger to the process:

    - In an aspect test, run the test with the debugger.
    - To debug the compiler, set the `MetalamaDebugCompiler` property to `True` during the build:

    ```powershell
    dotnet build -p:MetalamaDebugCompiler=True
    ```

Once you see where the transformed compile-time code is located, you can put breakpoints in this file using the debugger's UI.

## Debugging design-time logic

To attach a debugger to the design-time compiler process:

1. Install the Metalama Command Line Tool as described in <xref:dotnet-tool>.
2. Execute the following commands:

   ```powershell
   metalama config edit diagnostics
   ```

3. In the `diagnostics.json` file, edit the `debugging/processes` section and enable debugging for the proper process. If you use Visual Studio, this process is named `RoslynCodeAnalysisService`.

    ```json
     {

        // ...

        "debugger": {
            "processes": {
                 "Rider": false,
                  "RoslynCodeAnalysisService": true,
                  "CodeLensService": false,
                  "Other": false,
                  "TestHost": false,
                  "DevEnv": false,
                  "OmniSharp": false,
                  "BackstageWorker": false,
                  "DotNetTool": false,
                  "Compiler": false
            }
        }

        // ...

    }
    ```

4. Restart your IDE.

> [!div class="see-also"]
> <xref:debugging-aspect-oriented-code>


## Simulating a run-time breakpoint in your aspect

To debug run-time code enhanced with aspects, first see <xref:debugging-aspect-oriented-code>.

Adding a debugger breakpoint directly into an aspect class will not work because the template code of the aspect is expanded into many files. 

You can try the following approach to debug an aspect at run time.

First, create a helper class as follows:


```cs
internal static class DebuggingHelper
{
  [Conditional("DEBUG")]
  public static void ConditionalBreak( string aspectName, string targetTypeName, string targetMemberName )
  {
    // Intentionally empty.
    // You can put a conditional breakpoint with your UI here as needed.
  }
}

```

Then, include this code in your template code:

```cs
DebuggingHelper.ConditionalBreak( "TheAspect", meta.Target.Type.Name, meta.Target.Member.Name );
```

To stop the debugger in the aspect code at run time, add a debugger breakpoint in the `ConditionalBreak` method. Use conditional breakpoints to filter the aspects, target types, or target methods for which the debugger should break.