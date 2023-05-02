# Metalama Documentation

This repo contains the documentation of Metalama. It's best to read it online at https://doc.metalama.net.

The code snippets of this documentation can be under the `code` subdirectory and are fully unit testable. Other examples stem from the [Metalama.Samples](https://github.com/postsharp/Metalama.Samples) and [Metalama.Community](https://github.com/postsharp/Metalama.Community) repos.



## Building everything

```powershell
.\Build.ps1 build
```


## Our Markdown extensions

### metalama-test

This markup includes an aspect test in a tab group. The target code is displayed as a side-by-side diff.

```
[!metalama-test <path> [tabs="<tabs>"] ]

```

where:

* `<path>` is a relative path, usually starting with `~`, where `~` is replaced by the root of the current repo.
* `<tabs>` is a comma-separated list of one or more of the following values:
    
    * `aspect`
    * `target` 
    * `transformed`
    * `output`


### metalama-compare

This markup displayed source and transformed code side by side as a diff.

```
[!metalama-compare <path>]
```

where:

* `<path>` is a relative path, usually starting with `~`, where `~` is replaced by the root of the current repo.


### metalama-file

This markup includes a source file or a portion of it.

```
[!metalama-file <path> [transformed] [marker='foo'] [member='<member>']]
```

where:

* `<path>` is a relative path, usually starting with `~`, where `~` is replaced by the root of the current repo.
* `transformed` means that the transformed code should be displayed instead of the source code.
* `marker` means that only the code between lines with comments `/*<Foo>*/` and `/*</Foo>*/` should be included.
* `member` means that only the the given member (given in form `TypeName.MemberName` without namespace) should be included.


### metalama-files

This markup creates a tab group with several files.


```
[!metalama-files <path1> <path2> ... <path_n> [links="true|false"]]
```

where:

* `<path1>` ... `<path_n>` are relative paths, usually starting with `~`, where `~` is replaced by the root of the current repo.
* `links` indicates whether GitHub links should be generated.



### metalama-project-buttons

This markup generates buttons that open the whole directory in GitHub or in the sandbox.

```
[!metalama-project-buttons <path>]
```

where:

* `<path>` is a relative path to the directory.
