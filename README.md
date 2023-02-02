# Metalama Documentation

[![Documentation Snippets](https://github.com/postsharp/Metalama.Documentation/actions/workflows/main.yml/badge.svg)](https://github.com/postsharp/Metalama.Documentation/actions/workflows/main.yml) [![Metalama.Samples](https://github.com/postsharp/Metalama.Samples/actions/workflows/main.yml/badge.svg)](https://github.com/postsharp/Metalama.Samples/actions/workflows/main.yml)

This repo contains the documentation of Metalama, including the testable code snippets under the `code` subdirectory.

## Building everything

> [!WARNING]
> You must be a PostSharp employee to build the documentation.

```powershell
.\Build.ps1 build
```

## Building the code snippets

```powershell
cd code
dotnet test
```

## Building the documentation

## First build

1. Clone the current repo in `c:\src\Metalama.Documentation`.
2. Clone the `Metalama` repo in `c:\src\Metalama`.
3. From `c:\src\Metalama.Documentation`, do `.\build.ps1`.

## Incremental builds

From `c:\src\Metalama.Documentation`, do `.\Publish.ps1 -Incremental`.

## Publishing the documentation

1. From `c:\src\Metalama.Documentation`, do `.\Publish.ps1 -Pack`.
2. Upload `c:\src\Metalama.Documentation\docfx\output\Metalama.Documentation.zip` to `s3://doc.postsharp.net/Metalama.Documentation.zip`.
3. Invalidate the cache of `https://doc.metalama.net` using the API key.



## Our Markdown extensions

### Include a sample (i.e. an aspect test)
```
[!metalama-sample <path> [tabs="<tabs>"] ]

```
where:

* `<tabs>` is a comma-separated list of one or more of the following values:
    
    * `aspect`
    * `target` 
    * `transformed`
    * `output`

* `<path>` is a relative path, usually starting by `~`, where `~` is replaced by the root of the current repo.
