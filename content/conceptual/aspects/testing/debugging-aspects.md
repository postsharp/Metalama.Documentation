---
uid: debugging-aspects
level: 300
---

# Debugging aspects

Debugging the compile-time logic of an aspect can be challenging due to the compiler not executing your source code. Instead, the transformed code, derived from your source code, is stored under an unpredictable path.

## Debugging compile-time logic

To debug compile-time logic, follow the steps below:

1. Insert breakpoints directly into your source code:

    - In a build-time method such as `BuildAspect`, invoke <xref:System.Diagnostics.Debugger.Break?text=Debugger.Break()>.
    - In a template method, invoke <xref:Metalama.Framework.Aspects.meta.DebugBreak?text=meta.DebugBreak()>.

    > [!WARNING]
    > Regular debugger breakpoints will not function. It is crucial to have a breakpoint embedded in your source code.

2. Attach the debugger to the process:

    - In an aspect test, execute the test with the debugger.
    - To debug the compiler, set the `MetalamaDebugCompiler` property to `True` during the build:

    ```powershell
    dotnet build -p:MetalamaDebugCompiler=True
    ```

## Debugging design-time logic

To attach a debugger to the design-time compiler process, follow these steps:

1. Install the Metalama Command Line Tool as instructed in <xref:dotnet-tool>.
2. Execute the command below:

   ```powershell
   metalama config edit diagnostics
   ```

3. In the `diagnostics.json` file, modify the `debugging/processes` section and enable debugging for the appropriate process. If you're using Visual Studio, this process is named `RoslynCodeAnalysisService`.

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


