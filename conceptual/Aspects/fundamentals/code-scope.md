---
uid: code-scope
---

# Compile-Time vs Run-Time Code

## Scopes of code

A fundamental concept of Metalama is that any type of your source code belongs to one of the following _scopes_:

### Run-time code

_Run-time code_ is the code that you are used to: it compiles to a binary assembly and typically executes on the end user's device. In a project that does not reference _Metalama.Framework_, all code is considered run-time.

The entry point of run-time code is typically the _Program.Main_ method.

### Compile-time code

_Compile-time code_ is code that is executed either at compile time by the compiler, or at design time by the IDE. 

<!--- the second sentence here makes no sense as it's written, perhaps you meant to say...Metalama will look for the attribute    -->
Metalama recognizes compile-time-only code thanks to the <xref:Metalama.Framework.Aspects.CompileTimeAttribute> custom attribute. It will look at the attribute on the member, on the declaring type, and at the base types and interfaces. Most classes and interfaces of the _Metalama.Framework_ assembly are compile-time-only.

You can create compile-time classes by annotating them with <xref:Metalama.Framework.Aspects.CompileTimeAttribute>.

All compile-time code _must_ be strictly compatible with .NET Standard 2.0, even if the containing project targets a richer platform. We will see why in a minute.

There are two kinds of entry points for compile-time code:

* <xref:aspect-abilities?text=Aspects> which are added to source code as custom attributes, and
* <xref:fabric-abilities?text=Fabrics> which are executed just because they exist.

### Scope-neutral code

_Scope-neutral code_ is code that can execute either at run time or at compile time.

Scope-neutral code is annotated with the <xref:Metalama.Framework.Aspects.RunTimeOrCompileTimeAttribute> custom attribute.

Aspect classes are scope-neutral because aspects are a special kind of class. Aspects are typically represented as custom attributes, and these attributes can be accessed at run time using _System.Reflection_, but they are also instantiated at compile time by Metalama. Therefore, it is important that the constructors and public properties of the aspects are both run-time and compile-time.

However, some methods of aspect classes are purely compile-time. They cannot be executed at run time because they access APIs that exist only at compile time. These methods are annotated with <xref:Metalama.Framework.Aspects.CompileTimeAttribute> or one of the other derived attribute classes.


## Compilation process

When Metalama compiles your project, one of the first steps is to separate the compile-time code from the run-time code. From your initial project, Metalama creates two compilations: 

1. The _compile-time_ compilation contains only compile-time code. It is compiled against .NET Standard 2.0. It is then loaded within the compiler or IDE process, and executed at compile or design time. 
2. The _run-time_ compilation contains the run-time code. It also contains the compile-time _declarations_, but their implementation is replaced by `throw new NotSupportedException()`.

During compilation, Metalama compiles the [T# templates](xref:templates) into standard C# code that generates the run-time code using the Roslyn API. This generated code, as well as any non-template compile-time code, is then zipped and embedded in the run-time assembly as a managed resource.

> [!WARNING]
> *Intellectual property alert.* The _source_ of your compile-time code is embedded in clear text, without any obfuscation, in the run-time binary assemblies as a managed resource.


