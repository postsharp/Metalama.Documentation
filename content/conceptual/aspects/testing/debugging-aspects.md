---
uid: debugging-aspects
level: 300
---

# Debugging aspects

Debugging the compile-time logic of an aspect is currently difficult because the compiler does not execute your source code.  Instead, the transformed code is produced from your source code and stored under an unpredictable path.

## Debugging compile-time logic

To debug compile-time logic:

1. Inject breakpoints straight into your source code:

    - In a build-time method such as `BuildAspect`, call <xref:System.Diagnostics.Debugger.Break?text=Debugger.Break()>.
    - In a template method, call <xref:Metalama.Framework.Aspects.meta.DebugBreak?text=meta.DebugBreak()>.

    > [!WARNING]
    > Normal debugger breakpoints will not work. You must have a breakpoint directly in your source code.

2. Attach the debugger to the process:

    - In an aspect test, run the test with the debugger.
    - To debug the compiler, set the `MetalamaDebugCompiler` property to `True` during the build:

    ```powershell
    dotnet build -p:MetalamaDebugCompiler=True
    ```

## Debugging design-time logic

To attach a debugger to the design-time compiler process:

1. Install the Metalama Command Line Tool as described in <xref:dotnet-tool>.
2. Execute the following commands:

   ```powershell
   metalama config edit diagnostics
   ```

3. In the `diagnostics.json` file, edit the `debugging/processes` section and enable debugging for the proper process. If you are using Visual Studio, this process is named `RoslynCodeAnalysisService`.

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


