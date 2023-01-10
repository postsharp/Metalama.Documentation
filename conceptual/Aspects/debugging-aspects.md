---
uid: debugging-aspects
---

# Debugging Aspects

Debugging the compile-time logic of an aspect is currently difficult because the compiler does not execute your source code, but the transformed code produced from your source code and stored under an unpredictable path.

## Debugging compile-time logic

To debug compile-time logic:

1. Inject breakpoints directly in your source code:

    - In a build-time method such as `BuildAspect`, call <xref:System.Diagnostics.Debugger.Break?text=Debugger.Break()>.
    - In a template method, call <xref:Metalama.Framework.Aspects.meta.DebugBreak?text=meta.DebugBreak()>.

    > [!WARNING]
    > Normal debugger breakpoints will not work. You must have a breakpoint directly in your source code.

2. Attach the debugger to the process

    - In an aspect test, run the test with the debugger.
    - To debug the compiler, set the `MetalamaDebugCompiler` property to `True`: 

    ```powershell
    dotnet build -p:MetalamaDebugCompiler=True
    ```

## Debugging design-time logic

To attach a debugger to the design-time compiler process:

1. Install Metalama Command Line Tools as described in <xref:dotnet-tool>.
2. Execute the following commands:

   ```powershell
   metalama diag edit
   ```

3. In the `diagnostics.json` file, edit the `debugger/processes` section and enable debugging for the proper process. If you are using Visual Studio, this process is named `RoslynCodeAnalysisService`.

    ```json
     {
        "logging": {
            "processes": {
                "Compiler": false,
                "Rider": false,
                "DevEnv": false,
                "RoslynCodeAnalysisService": false
            },
            "categories": {
            "*": true,
            "Licensing": false
            }
        },
        "debugger": {
            "processes": {
                "Compiler": false,
                "Rider": false,
                "DevEnv": false,
                "RoslynCodeAnalysisService": true
            }
        }
    }
    ```

4. Restart your IDE.

> [!div class="see-also"]
> <xref:debugging>


