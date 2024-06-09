---
uid: xaml-command
---

# XAML Command

The <xref:Metalama.Patterns.Xaml.CommandAttribute?text=[Command]> aspect generates a XAML Command property based on a plain C# method executing the command, and optionally, a `bool` property or method determining if the command is enabled.

## Generating a XAML command property from a method

To generate a XAML command property from a method:

1. Add a reference to the `Metalama.Patterns.Xaml` package to your project.
2. Add the <xref:Metalama.Patterns.Xaml.CommandAttribute?text=[Command]> attribute to the method that must be executed when the command is invoked. This method will become the implementation of the <xref:System.Windows.Input.ICommand.Execute*?text=ICommand.Execute> interface method. It must return `void` and have zero or one parameter.
3. Make the class `partial` to make it possible to reference the generated command properties from C# or XAML source code.

### Example: Simple commands

The following example implements a window with two commands: `Increment` and `Decrement`. As illustrated, the <xref:Metalama.Patterns.Xaml.CommandAttribute?text=[Command]> aspect generates two properties, `IncrementCommand` and `DecrementCommand`, assigned to an instance of the <xref:Metalama.Patterns.Xaml.Implementation.DelegateCommand> helper class. This class accepts a delegate to the `Increment` or `Decrement` method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Xaml/Commands/SimpleCommand.cs]

## Adding a CanExecute method or property

In addition to the <xref:System.Windows.Input.ICommand.Execute*> method, you can also supply an implementation of <xref:System.Windows.Input.ICommand.CanExecute*?text=ICommand.CanExecute>. This implementation can be either a `bool` property or, when the `Execute` method has a parameter, a method that accepts the same parameter type and returns `bool`.

There are two ways to associate a `CanExecute` implementation with the `Execute` member:

* Implicitly, by respecting naming conventions. For a command named `Foo`, the `CanExecute` member can be named `CanFoo`, `CanExecuteFoo`, or `IsFooEnabled`. See below to learn how to customize these naming conventions.
* Explicitly, by setting the <xref:Metalama.Patterns.Xaml.CommandAttribute.CanExecuteMethod> or <xref:Metalama.Patterns.Xaml.CommandAttribute.CanExecuteProperty> property of the <xref:Metalama.Patterns.Xaml.CommandAttribute>.

When the `CanExecute` member is a property and the declaring type implements the <xref:System.ComponentModel.INotifyPropertyChanged> interface, the <xref:System.Windows.Input.ICommand.CanExecuteChanged?text=ICommand.CanExecuteChanged> event will be raised whenever the `CanExecute` property changes. You can use the <xref:Metalama.Patterns.Observability.ObservableAttribute?text=[Observable]> aspect to implement <xref:System.ComponentModel.INotifyPropertyChanged>. See <xref:observability> for details.


### Example: Commands with a CanExecute property and _implicit_ association

The following example demonstrates two commands, `Increment` and `Decrement`, coupled to properties that determine if these commands are available: `CanIncrement` and `CanDecrement`.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Xaml/Commands/CanExecute.cs]


### Example: Commands with a CanExecute property and _explicit_ association

This example is identical to the one above, but we use the <xref:Metalama.Patterns.Xaml.CommandAttribute.CanExecuteProperty> property to explicitly associate the `CanExecute` property with their `Execute` method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Xaml/Commands/CanExecute_Explicit.cs]

### Example: Commands with a CanExecute property and [Observable]

The following example demonstrates the code generated when the `[Command]` and `[Observable]` aspects are used together. Notice the compactness of the source code and the sheer size of the generated code.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Xaml/Commands/CanExecute_Observable.cs]

## Customizing naming conventions

All examples above relied on the default naming convention, which is based on the following assumptions:
* The command name is obtained by trimming the `Execute` method name (the one with the `[Command]` aspect) from:
    * prefixes: `_` and `Execute`,
    * suffix: `_` and `Command`.
* Given a command name `Foo` determined by the previous step:
    * The command property is named `FooCommand`.
    * The `CanExecute` command or method can be named `CanFoo`, `CanExecuteFoo`, or `IsFooEnabled`.

This naming convention can be modified by calling the <xref:Metalama.Patterns.Xaml.Configuration.CommandExtensions.ConfigureCommand*> fabric extension method, then <xref:Metalama.Patterns.Xaml.Configuration.CommandOptionsBuilder.AddNamingConvention?text=builder.AddNamingConvention*>, and supply an instance of the <xref:Metalama.Patterns.Xaml.Configuration.CommandNamingConvention> class.

If specified, the <xref:Metalama.Patterns.Xaml.Configuration.CommandNamingConvention.CommandNamePattern?text=CommandNamingConvention.CommandNamePattern> is a regular expression that matches the command name from the name of the main method. If this property is unspecified, the default matching algorithm is used. The <xref:Metalama.Patterns.Xaml.Configuration.CommandNamingConvention.CanExecutePatterns> property is a list of patterns used to select the `CanExecute` property or method, and the <xref:Metalama.Patterns.Xaml.Configuration.CommandNamingConvention.CommandPropertyName> property is a pattern that generates the name of the generated command property. In the <xref:Metalama.Patterns.Xaml.Configuration.CommandNamingConvention.CanExecutePatterns> and <xref:Metalama.Patterns.Xaml.Configuration.CommandNamingConvention.CommandPropertyName>, the `{CommandName}` substring is replaced by the name of the command returned by <xref:Metalama.Patterns.Xaml.Configuration.CommandNamingConvention.CommandNamePattern>.

### Example: Czech Naming Conventions

The following example illustrates a naming convention for the Czech language. There are two conventions. The first matches the `Vykonat` prefix in the main method, for instance, it will match a method named `VykonatBlb` and return `Blb` as the command name. The second naming convention matches everything and removes the conventional prefixes `_` and `Execute` as described above.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Xaml/Commands/CanExecute_Czech.cs]

