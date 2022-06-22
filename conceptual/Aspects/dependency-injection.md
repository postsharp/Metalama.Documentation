---
uid: dependency-injection
---

# Injecting Dependencies Into Aspects

Many aspects need to consume services that must be injected from a dependency injection container. For instance, a caching aspect may depend on the `IMemoryCache` service. If you are using the `Microsoft.Extensions.DependencyInjection` framework, your aspect will need to pull this service from the constructor. If the target type of the aspect does not already accept this service from the constructor, the aspect will have to append this parameter to the constructor.

However, the code pattern that must be implemented to pull any dependency depends on the dependency injection framework in use for that project. As we have seen, the default .NET Core framework require a constructor parameter, but other  frameworks may use an `[Import]` or `[Inject]` custom attribute. 

In some cases, you as the author of the aspect do not know which dependency injection framework will be used for the classes to which your aspect is applied.

Enter the <xref:Metalama.Framework.DependencyInjection> project. Thanks to this namespace, your aspect can consume and pull a dependency with a single custom attribute. The code pattern to pull the dependency is implemented by the implementation of the <xref:Metalama.Framework.DependencyInjection.Implementation.IDependencyInjectionFramework> interface, which is chosen by the _user_ project. 

The <xref:Metalama.Framework.DependencyInjection> namespace is an open source hosted on [GitHub](https://github.com/postsharp/Metalama.Framework.Extensions). It currently has implementations for the following dependency injection framework:
* <xref:Metalama.Framework.DependencyInjection.Implementation.DefaultDependencyInjectionFramework> implements the default .NET Core pattern  (see `Microsoft.Extensions.DependencyInjection` ).
* <xref:Metalama.Framework.DependencyInjection.ServiceLocator.ServiceLocatorDependencyInjectionFramework> can be used by classes or projects that are not instantiated by a dependency injection framework thanks a simple _service locator_ pattern.

The <xref:Metalama.Framework.DependencyInjection> project is designed to make it easy to implement other dependency injection frameworks.


## Consuming dependencies from your aspect

To consume a dependency from an aspect:

1. Add the `Metalama.Framework.DependencyInjection` package to your project.
2. Add a field or automatic property of the desired type in your aspect class.
3. Annotate this field or property with the <xref:Metalama.Framework.DependencyInjection.IntroduceDependencyAttribute> custom attribute. The following attribute properties are available:
  * <xref:Metalama.Framework.DependencyInjection.IntroduceDependencyAttribute.IsLazy> resolves the dependency upon first use, instead of upon initialization, and
  * <xref:Metalama.Framework.DependencyInjection.IntroduceDependencyAttribute.IsRequired> throws an exception if the dependency is not available.
4. Use this field or property from any template member of your aspect.

### Example

The following example uses the `Microsoft.Extensions.Hosting`, typical to .NET Core applications, to build an application and inject services. The `Program.Main` method builds the host, which then instantiates our `Worker` class. We add a `[Log]` aspect to this class. The `Log` aspect class has a field of type `IMessageWriter`, marked with the <xref:Metalama.Framework.DependencyInjection.IntroduceDependencyAttribute> custom attribute. As you can see in the transformed code, this field is introduced into the `Worker` class and pulled from the constructor.

[!include[Dependency Injection](../../code/Metalama.Documentation.SampleCode.DependencyInjection/Hello.cs)]


## Selecting a dependency injection framework

By default, Metalama generates code for the default .NET dependency injection framework implemented in the ``Microsoft.Extensions.DependencyInjection`` namespace (also called the .NET Core dependency injection framework).

If you want to select a different framework for a project, it is generally sufficient to add a reference to the package implementing this dependency framework i.e. for instance `Metalama.Framework.DependencyInjection.ServiceLocator`. These packages generally include a <xref:Metalama.Framework.Fabrics.TransitiveProjectFabric> that register themselves. This works well when there is a single dependency injection framework in the project.

When there are several dependency injection frameworks in a project, Metalama will call the <xref:Metalama.Framework.DependencyInjection.DependencyInjectionOptions.Selector?text=DependencyInjectionOptions.Selector> delegate. Its default implementation is to return the first eligible framework in the input list, i.e. the topmost in the <xref:Metalama.Framework.DependencyInjection.DependencyInjectionOptions.RegisteredFrameworks> list. 

To customize the selection strategy of the dependency injection framework for a specific aspect and dependency:

1. Add a <xref:Metalama.Framework.Fabrics.ProjectFabric> to your project as described in <xref:fabrics-configuring>.
2. From the <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*> method, call the <xref:Metalama.Framework.DependencyInjection.DependencyInjectionExtensions.DependencyInjectionOptions*?text=amender.Project.DependencyInjectionOptions()> method to access the options.
3. Set the <xref:Metalama.Framework.DependencyInjection.DependencyInjectionOptions.Selector?text=DependencyInjectionOptions.Selector> property.

 
 ## Implementing an adaptor for a new dependency injection framework

 If you need to support a dependency injection framework or pattern for which no ready-made implementation exists, you can implement an adapter yourself.

 See [Metalama.Framework.DependencyInjection.ServiceLocator on GitHub](https://github.com/postsharp/Metalama.Framework.Extensions/tree/master/src/Metalama.Framework.DependencyInjection.ServiceLocator) for a working example.

 The steps are the following:

1. Create a class library project that targets `netstandard2.0`.
2. Add a reference to the `Metalama.Framework.DependencyInjection` package.
3. Implement the <xref:Metalama.Framework.DependencyInjection.Implementation.IDependencyInjectionFramework> interface in a new public class. It is easier to start from the <xref:Metalama.Framework.DependencyInjection.Implementation.DefaultDependencyInjectionFramework> class. In this case, you will also need to override the <xref:Metalama.Framework.DependencyInjection.Implementation.DefaultDependencyInjectionStrategy> class. See the source code and the class documentation for details.
4. Optionally create a <xref:Metalama.Framework.Fabrics.TransitiveProjectFabric> that registers the framework in <xref:Metalama.Framework.DependencyInjection.DependencyInjectionExtensions.DependencyInjectionOptions*?text=amender.Project.DependencyInjectionOptions()> using the <xref:Metalama.Framework.DependencyInjection.DependencyInjectionOptions.RegisterFramework*> method.

### Example

The following example shows how to implement the right code generation pattern for the `ILogger` service under .NET Core. Unlike normal services which require a parameter of the same type of the constructor, the `ILogger` service requires a parameter of the generic type `ILogger<T>`, where `T` is the current type, used as a category.

Our implementation of <xref:Metalama.Framework.DependencyInjection.Implementation.IDependencyInjectionFramework> implements the <xref:Metalama.Framework.DependencyInjection.Implementation.DefaultDependencyInjectionFramework.CanHandleDependency*> method, and returns `true` only when the dependency is of type `ILogger`. The only difference in the default implementation strategy is the parameter type.


[!include[Custom Adapter](../../code/Metalama.Documentation.SampleCode.DependencyInjection/CustomFramework.cs)]