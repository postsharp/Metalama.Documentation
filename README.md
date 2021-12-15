# Metalama Documentation

[![Documentation Snippets](https://github.com/postsharp/Metalama.Documentation/actions/workflows/main.yml/badge.svg)](https://github.com/postsharp/Metalama.Documentation/actions/workflows/main.yml) [![Metalama.Samples](https://github.com/postsharp/Metalama.Samples/actions/workflows/main.yml/badge.svg)](https://github.com/postsharp/Metalama.Samples/actions/workflows/main.yml)

This repo contains the documentation of Metalama, including the testable code snippets under the `code` subdirectory.


## Building the code snippets

```
cd code
dotnet test
```

## Building the documentation

> [!WARNING]
> You must be a PostSharp employee to build the documentation.

## First build

1. Clone the current repo in `c:\src\Metalama.Documentation`.
2. Clone the `Metalama` repo in `c:\src\Metalama`.
3. From `c:\src\Metalama.Documentation`, do `.\build.ps1`.

## Incremental builds

From `c:\src\Metalama.Documentation`, do `.\build.ps1 -Incremental`.

## Publishing the documentation

1. From `c:\src\Metalama.Documentation`, do `.\build.ps1 -Pack`.
2. Upload `c:\src\Metalama.Documentation\docfx\output\Metalama.Documentation.zip` to `s3://doc.postsharp.net/Metalama.Documentation.zip`.
3. Invalidate the cache of `https://doc.metalama.net` using the API key.




