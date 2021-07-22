# PostSharp "Caravela" Documentation

[![Documentation Snippets](https://github.com/postsharp/Caravela.Documentation/actions/workflows/main.yml/badge.svg)](https://github.com/postsharp/Caravela.Documentation/actions/workflows/main.yml) [![Caravela.Samples](https://github.com/postsharp/Caravela.Samples/actions/workflows/main.yml/badge.svg)](https://github.com/postsharp/Caravela.Samples/actions/workflows/main.yml)

This repo contains the documentation of PostSharp "Caravela", including the testable code snippets under the `code` subdirectory.


## Building the code snippets

```
cd code
dotnet test
```

## Building the documentation

> [!WARNING]
> You must be a PostSharp employee to build the documentation.

## First build

1. Clone the current repo in `c:\src\Caravela.Documentation`.
2. Clone the `Caravela` repo in `c:\src\Caravela`.
3. Do `cd c:\src\Caravela.Documentation\docfx`
4. Do `.\build.ps1`

## Incremental builds

From `c:\src\Caravela.Documentation\docfx`, do `.\build.ps1 -Incremental`

## Publishing the documentation

1. From `c:\src\Caravela.Documentation\docfx`, do `.\build.ps1 -Pack`
2. Upload `c:\src\Caravela.Documentation\docfx\output\Caravela.Documentation.zip` to `s3://doc.postsharp.net/Caravela.Documentation.zip`.
3. Invalidate the cache of `https://doc.postsharp.net` using the API key.




