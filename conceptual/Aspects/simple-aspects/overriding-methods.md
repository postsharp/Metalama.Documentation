---
uid: overriding-methods
---
# Overriding Methods

The simplest and most common aspect is to wrap the hand-written body of a method with some automatically-generated code, but without modifying the method body itself. 

You can achieve this thanks to the <xref:Caravela.Framework.Aspects.OverrideMethodAspect> abstract class. <xref:Caravela.Framework.Aspects.OverrideMethodAspect> is the aspect-oriented implementation of the [decorator design pattern](https://en.wikipedia.org/wiki/Decorator_pattern) for methods.

## Creating an OverrideMethod aspect

1. Create a new class derived from the <xref:Caravela.Framework.Aspects.OverrideMethodAspect> abstract class. This class will be a custom attribute, so it is a good idea to name it with the `Attribute` suffix.

2. Implement the <xref:Caravela.Framework.Aspects.OverrideMethodAspect.OverrideMethod> method in plain C#:
   - To insert code or expressions that depend on the target method of the aspect (such as the method name or the parameter type), use the <xref:Caravela.Framework.Aspects.meta> API.
   - Where the original implementation must be invoked, call the <xref:Caravela.Framework.Aspects.meta.Proceed?text=meta.Proceed> method.

3. The aspect is a custom attribute. To transform a method using the aspect, just add the aspect custom attribute to the method.

### Example: an empty OverrideMethod aspect

The following code shows an empty <xref:Caravela.Framework.Aspects.OverrideMethodAspect>, which does not do anything:

[!include[Empty OverrideMethodAttribute](../../../code/Caravela.Documentation.SampleCode.AspectFramework/EmptyOverrideMethodAttribute.cs)]

## Accessing the metadata and parameters of the overridden method

The metadata of the method being overridden are available from the template method on the <xref:Caravela.Framework.Aspects.IMetaTarget.Method?text=meta.Target.Method> property . This property gives you all information about the name, type, parameters and custom attributes of the method. For instance, the metadata of method parameters are exposed on `meta.Target.Method.Parameters`. But note that only _metadata_ are exposed there.

To access the parameter _values_, you need to access <xref:Caravela.Framework.Aspects.IMetaTarget.Parameters?text=meta.Target.Parameters>. For instance:

- `meta.Target.Parameters[0].Value` gives you the value of the first parameter,
- `meta.Target.Parameters.Values.ToArray()` creates an `object[]` array with all parameter values,
- `meta.Target.Parameters["a"].Value = 5` sets the `a` parameter to `5`.

### Example: simple logging

The following code writes a message to the system console before and after the method execution. The text includes the name of the target method.

[!include[Simple Logging](../../../code/Caravela.Documentation.SampleCode.AspectFramework/SimpleLogging.cs)]

## Invoking the method with different arguments

When you call `meta.Proceed`, the aspect framework generates a call to the overridden method and passes the parameters it received. If the parameter value has been changed thanks to a statement like `meta.Target.Parameters["a"].Value = 5`, the modified value will be passed.

If you want to invoke the method with a totally different set of arguments, you can do it using <xref:Caravela.Framework.Code.Advised.IAdvisedMethod.Invoke(System.Object[])?text=meta.Target.Method.Invoke>.

> [!NOTE]
> Invoking a method with `ref` or `out` parameters is not yet supported.

## Overriding async and iterator methods

By default, the <xref:Caravela.Framework.Aspects.OverrideMethodAspect.OverrideMethod> method is used as a template for all methods, including async and iterator methods. This behavior works great most of the time, but it has a few limitations:

* You cannot use `await` or `yield` in the default template.
* When you call `meta.Proceed()` in the default template, it causes the complete evaluation of the async method or iterator.

> [!WARNING]
> Applying the default <xref:Caravela.Framework.Aspects.OverrideMethodAspect.OverrideMethod> template to an iterator the stream to be _buffered_ into a `List<T>`. In case of long-running streams, this buffering may be undesirable. In this case, specific iterator templates must be specified (see below).

### Example: the default template applied to all kinds of methods

The following example demonstrates the behavior of the default template when applied to different kinds of methods. Note that the output of iterators methods is buffered. This is visible in the program output.

[!include[Default template applied to all kinds of methods](../../../code/Caravela.Documentation.SampleCode.AspectFramework/OverrideMethodDefaultTemplateAllKinds.cs)]


### Implementing a specific template

When the default template is not sufficient to implement your aspect, you can implement different variants of the `OverrideMethod`. For each variant, instead of calling <xref:Caravela.Framework.Aspects.meta.Proceed?text=meta.Proceed>, you will call a variant of this method that has a relevant return type.

| Template Method                 | Proceed Method                            | Description |
|---|---|--|
| <xref:Caravela.Framework.Aspects.OverrideMethodAspect.OverrideAsyncMethod> | <xref:Caravela.Framework.Aspects.meta.ProceedAsync> | Applies to all async methods, including async iterators, except if a more specific template is implemented.
| <xref:Caravela.Framework.Aspects.OverrideMethodAspect.OverrideEnumerableMethod> | <xref:Caravela.Framework.Aspects.meta.ProceedEnumerable> | Applies to iterator methods returning an `IEnumerable<T>` or `IEnumerable`.
| <xref:Caravela.Framework.Aspects.OverrideMethodAspect.OverrideEnumeratorMethod> | <xref:Caravela.Framework.Aspects.meta.ProceedEnumerator> | Applies to iterator methods returning an `IEnumerator<T>` or `IEnumerator`.
| <xref:Caravela.Framework.Aspects.OverrideMethodAspect.OverrideAsyncEnumerableMethod> | <xref:Caravela.Framework.Aspects.meta.ProceedAsyncEnumerable> | Applies to async iterator methods returning an `IAsyncEnumerable<T>`.
| <xref:Caravela.Framework.Aspects.OverrideMethodAspect.OverrideAsyncEnumeratorMethod> | <xref:Caravela.Framework.Aspects.meta.ProceedAsyncEnumerator> | Applies to async iterator methods returning an `IAsyncEnumerator<T>`.

### Example: specific templates for all kinds of methods

The following example derives from the previous one implements all specific template methods instead of just the default template methods. Note that now the output of iterators is no longer buffered, because this new version of the aspect supports iterator streaming.

[!include[Specific templates for all kinds of methods](../../../code/Caravela.Documentation.SampleCode.AspectFramework/OverrideMethodSpecificTemplateAllKinds.cs)]