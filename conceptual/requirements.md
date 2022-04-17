---
uid: requirements
---

# Requirements

## Development Envionment

* .NET SDK 6.0 needs to be installed.
* Metalama has been tested on Windows and Ubuntu.
* Building on an ARM machine is not as yet possible but it should be supported in the future.

## IDEs

Metalama primarily integrates with Roslyn, so it will also integrate with any Roslyn-based IDE.

| IDE | Design-Time Correctness | Code Fixes | Other UI features
|-----|-------------------------|------------|--|
| Visual Studio 2022 _with_ Metalama Tools | Yes | Yes | Transformed code diff, info bar, syntax highlighting.
| Visual Studio 2022 _without_ Metalama Tools | Yes | Yes | |
| Rider | Yes | Yes | |
| Visual Studio Code (OmniSharp) | Yes | No | |

> [!NOTE]
> When using Visual Studio, the use of Metalama Tools for Visual Studio is not required but is highly recommended.


## Projects

Only SDK-style projects are supported.

Your projects can target any framework that supports .NET Standard 2.0. 

This includes:

| Framework | Versions | 
|-----------|-----------|
| NET and .NET Core	| 2.0 or later 
| .NET Framework | 4.7.2 
| Mono |	5.4 or later 
| Xamarin.iOS	 | 10.14 or later
| Xamarin.Mac |	3.8 or later
| Xamarin.Android |	8.0 or later 
| Universal Windows Platform	| 10.0.16299 or later 
| Unity | ??

