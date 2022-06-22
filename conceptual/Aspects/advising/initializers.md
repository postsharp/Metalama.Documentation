---
uid: initializers
---

# Adding Initializers


## Initialization of fields and properties

### Inline initialization of declarative advice

The simple way to initialize a field or property introduced by an aspect is to add an initializer to the template. That is, if your aspects introduce a field `int f` and you want to initialize it to `1`, simply write:

 ```cs
 [Introduce] 
 int f = 1;
 ```

#### Example: introducing a Guid property

In the following example, the aspect introduces an `Id` property of type `Guid`, and initializes it to a new unique value.

[!include[Introduce Id](../../../code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceId.cs)]

#### Example: initializing with a template

You can also use the T# template language inside field and property analyzers. The aspect in the following example introduces a property that is initialized to the build configuration and target framework.

[!include[Introduce Build Info](../../../code/Metalama.Documentation.SampleCode.AspectFramework/BuildInfo.cs)]

### Initialization of programmatic advice

If you use the programmatic advice <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceProperty*>, <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceField*> or <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceEvent*> you can set the `Metalama.Framework.Code.DeclarationBuilders.IFieldOrPropertyBuilder.InitializerExpression` of the builder object that these advice methods return, and set it to any expression.

#### Example: initializing a programmatically introduced field

The example in the following aspect introduces a field using the <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceField*> programmatic advice and sets its initializer expression to an array that contains the name of all methods in the target type.

[!include[Programmatic Initializer](../../../code/Metalama.Documentation.SampleCode.AspectFramework/ProgrammaticInitializer.cs)]

## Before object constructor

To inject some initialization before any user code of the instance constructor is called:

1. Add a method of signature `void BeforeInstanceConstructor()` to your aspect class and annotate it with the `[Template]` custom attribute. The name of this method is arbitrary.
2. Call the <xref:Metalama.Framework.Advising.IAdviceFactory.AddInitializer*?text=builder.Advice.AddInitializer> method in your aspect (or <xref:Metalama.Framework.Advising.IAdviceFactory.AddInitializer*?text=amender.Advice.AddInitializer>) in a fabric). Pass the type that must be initialized, then the name of the method of the previous step, and finally the value `InitializerType.BeforeInstanceConstructor`.

The `AddInitializer` advice will _not_ affect the constructors that call a chained `this` constructor. That is, the advice always runs before any constructor of the current class. However, the initialization logic does run _after_ the call to the `base` constructor, if any.

If the type does not contain any constructor, a default constructor will be created.

### Example: registering live instances

The aspect in the following aspect registers any new aspect of the target class in a registry of live instances. When an instance is garbage collected, it is automatically removed from the registry. The aspect injects the registration logic into the constructor of the target class.

[!include[Register Instance](../../../code/Metalama.Documentation.SampleCode.AspectFramework/RegisterInstance.cs)]


## Before type constructor

The same approach as above can be used to add logic to the type constructor (i.e. static constructor) instead of the object constructor. The `InitializerType.BeforeTypeConstructor` value needs to be used instead.