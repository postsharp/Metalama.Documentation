---
uid: overriding-methods
---
# Overriding Methods

The simplest and most common aspect is to wrap the hand-written body of a method with some automatically-generated code, but without modifying the method body itself. 

You can achieve this thanks to the <xref:Metalama.Framework.Aspects.OverrideMethodAspect> abstract class. <xref:Metalama.Framework.Aspects.OverrideMethodAspect> is the aspect-oriented implementation of the [decorator design pattern](https://en.wikipedia.org/wiki/Decorator_pattern) for methods.

## The simple way: deriving the OverrideMethod abstract class

1. Add Metalama to your project as described in <xref:installing>.
   
2. Create a new class derived from the <xref:Metalama.Framework.Aspects.OverrideMethodAspect> abstract class. This class will be a custom attribute, so it is a good idea to name it with the `Attribute` suffix.

3. Implement the <xref:Metalama.Framework.Aspects.OverrideMethodAspect.OverrideMethod> method in plain C#. This method will serve as a <xref:templates?text=template> defining the way the aspect overrides the hand-written target method.
   - To insert code or expressions that depend on the target method of the aspect (such as the method name or the parameter type), use the <xref:Metalama.Framework.Aspects.meta> API.
   - Where the original implementation must be invoked, call the <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed> method.

4. The aspect is a custom attribute. To transform a method using the aspect, just add the aspect custom attribute to the method.

### Example: an empty OverrideMethod aspect

The following code shows an empty <xref:Metalama.Framework.Aspects.OverrideMethodAspect>, which does not do anything:

[!include[Empty OverrideMethodAttribute](../../../code/Metalama.Documentation.SampleCode.AspectFramework/EmptyOverrideMethodAttribute.cs)]

## Accessing the metadata and parameters of the overridden method

The metadata of the method being overridden are available from the template method on the <xref:Metalama.Framework.Aspects.IMetaTarget.Method?text=meta.Target.Method> property . This property gives you all information about the name, type, parameters and custom attributes of the method. For instance, the metadata of method parameters are exposed on `meta.Target.Method.Parameters`. But note that only _metadata_ are exposed there.

To access the parameter _values_, you need to access <xref:Metalama.Framework.Aspects.IMetaTarget.Parameters?text=meta.Target.Parameters>. For instance:

- `meta.Target.Parameters[0].Value` gives you the value of the first parameter,
- `meta.Target.Parameters.Values.ToArray()` creates an `object[]` array with all parameter values,
- `meta.Target.Parameters["a"].Value = 5` sets the `a` parameter to `5`.

### Example: simple logging

The following code writes a message to the system console before and after the method execution. The text includes the name of the target method.

[!include[Simple Logging](../../../code/Metalama.Documentation.SampleCode.AspectFramework/SimpleLogging.cs)]

## Invoking the method with different arguments

When you call `meta.Proceed`, the aspect framework generates a call to the overridden method and passes the parameters it received. If the parameter value has been changed thanks to a statement like `meta.Target.Parameters["a"].Value = 5`, the modified value will be passed.

If you want to invoke the method with a totally different set of arguments, you can do it using <xref:Metalama.Framework.Code.Advised.IAdvisedMethod.Invoke*?text=meta.Target.Method.Invoke>.

> [!NOTE]
> Invoking a method with `ref` or `out` parameters is not yet supported.

## Overriding async and iterator methods

> [!div id="async-iterator-default-template" class="anchor"]

By default, the <xref:Metalama.Framework.Aspects.OverrideMethodAspect.OverrideMethod> method is used as a template for all methods, including async and iterator methods. 

To make the default template work naturally even for async and iterator methods, calls to `meta.Proceed()` and `return` statements are interpreted differently in each situation in order to respect the intent of normal (non-async, non-iterator) code. That is, the default behavior tries to respect the _decorator_ pattern.

> [!WARNING]
> Applying the default <xref:Metalama.Framework.Aspects.OverrideMethodAspect.OverrideMethod> template to an iterator makes the stream to be _buffered_ into a `List<T>`. In case of long-running streams, this buffering may be undesirable. In this case, specific iterator templates must be specified (see below).

The following table lists the transformations applied to the `meta.Proceed()` expression and the `return` statement when a default template is applied to an async or iterator method:

<table>
   <tr>
      <th>Target Method</th>
      <th>
        Transformation of `"meta.Proceed()"`
      </th>
      <th>
         Transformation of `"return result;"`
      </th>
   </tr>
   <tr>
      <td>
         `async`
      </td>
      <td>
         `await meta.Proceed()`
      </td>

      <td>
         `return result`
      </td>
   </tr>
    <tr>
      <td>
         `IEnumerable<T>` iterator
      </td>
      <td>
        `RunTimeAspectHelper.Buffer( meta.Proceed() )`
        returning a `List<T>`
      </td>
      <td>
         `return result`
      </td>
   </tr>
   <tr>
      <td>
         `IEnumerator<T>` iterator
      </td>
      <td>
        `RunTimeAspectHelper.Buffer( meta.Proceed() )`
        returning a `List<T>.Enumerator`
      </td>
      <td>
         `return result`
      </td>
   </tr>
    <tr>
      <td>
         `IAsyncEnumerable<T> async`
      </td>
      <td>
         `await RunTimeAspectHelper.BufferAsync( meta.Proceed() )`
        returning an <xref:Metalama.Framework.RunTime.AsyncEnumerableList`1>
      </td>
      <td>
         ```cs
         await foreach (var r in result)
         {
               yield return r;
         }

         yield break;
         ```         
      </td>
   </tr>
     <tr>
      <td>
         `IAsyncEnumerator<T> async`
      </td>
      <td>
        `await RunTimeAspectHelper.BufferAsync( meta.Proceed() )`
        returning an <xref:Metalama.Framework.RunTime.AsyncEnumerableList`1.AsyncEnumerator>
      </td>
      <td>
         ```cs
         await using ( result )
         {
               while (await result.MoveNextAsync())
               {
                  yield return result.Current;
               }
         }
         yield break;
         ``` 
      </td>
   </tr>
   </table>


As you can see, the buffering of iterators is performed by the <xref:Metalama.Framework.RunTime.RunTimeAspectHelper.Buffer*> and <xref:Metalama.Framework.RunTime.RunTimeAspectHelper.BufferAsync*> methods.


### Example: the default template applied to all kinds of methods

The following example demonstrates the behavior of the default template when applied to different kinds of methods. Note that the output of iterators methods is buffered. This is visible in the program output.

[!include[Default template applied to all kinds of methods](../../../code/Metalama.Documentation.SampleCode.AspectFramework/OverrideMethodDefaultTemplateAllKinds.cs)]

### Implementing a specific template for async or iterator methods

> [!div id="async-iterator-specific-template" class="anchor"]

The default template works great most of the time even on async methods and iterators, but it has a few limitations:

* You cannot use `await` or `yield` in the default template.
* When you call `meta.Proceed()` in the default template, it causes the complete evaluation of the async method or iterator.

To overcome these limitations, you can implement different variants of the `OverrideMethod`. For each variant, instead of calling <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed>, you will call a variant of this method that has a relevant return type.

| Template Method                 | Proceed Method                            | Description |
|---|---|--|
| <xref:Metalama.Framework.Aspects.OverrideMethodAspect.OverrideAsyncMethod> | <xref:Metalama.Framework.Aspects.meta.ProceedAsync> | Applies to all async methods, including async iterators, except if a more specific template is implemented.
| <xref:Metalama.Framework.Aspects.OverrideMethodAspect.OverrideEnumerableMethod> | <xref:Metalama.Framework.Aspects.meta.ProceedEnumerable> | Applies to iterator methods returning an `IEnumerable<T>` or `IEnumerable`.
| <xref:Metalama.Framework.Aspects.OverrideMethodAspect.OverrideEnumeratorMethod> | <xref:Metalama.Framework.Aspects.meta.ProceedEnumerator> | Applies to iterator methods returning an `IEnumerator<T>` or `IEnumerator`.
| <xref:Metalama.Framework.Aspects.OverrideMethodAspect.OverrideAsyncEnumerableMethod> | <xref:Metalama.Framework.Aspects.meta.ProceedAsyncEnumerable> | Applies to async iterator methods returning an `IAsyncEnumerable<T>`.
| <xref:Metalama.Framework.Aspects.OverrideMethodAspect.OverrideAsyncEnumeratorMethod> | <xref:Metalama.Framework.Aspects.meta.ProceedAsyncEnumerator> | Applies to async iterator methods returning an `IAsyncEnumerator<T>`.

Note that there is no obligation to implement these methods as `async` methods or `yield`-based iterators.

### Example: specific templates for all kinds of methods

The following example derives from the previous one implements all specific template methods instead of just the default template methods. Note that now the output of iterators is no longer buffered, because this new version of the aspect supports iterator streaming.

[!include[Specific templates for all kinds of methods](../../../code/Metalama.Documentation.SampleCode.AspectFramework/OverrideMethodSpecificTemplateAllKinds.cs)]

### Using specific templates for non-async awaitable or non-yield enumerable methods

If you want to use the specific templates for methods that have the correct return type but are not implemented using `await` or `yield`, set the <xref:Metalama.Framework.Aspects.OverrideMethodAspect.UseAsyncTemplateForAnyAwaitable>
or <xref:Metalama.Framework.Aspects.OverrideMethodAspect.UseEnumerableTemplateForAnyEnumerable> property of the <xref:Metalama.Framework.Aspects.OverrideMethodAspect> class to `true` in the aspect constructor.

## Overriding several methods with the same aspect

In the above sections, we have always derived our aspect class from the <xref:Metalama.Framework.Aspects.OverrideMethodAspect> abstract class. This class exists for simplificy and convenience. It is merely a shortcut that derives from the <xref:System.Attribute> class and implements the `IAspect<IMethod>` interface. The only thing it does is to add an `Override` advice to the target of the custom attribute.

Here is the simplified source code of the <xref:Metalama.Framework.Aspects.OverrideMethodAspect> class:

[!include[Main](../../../code/Metalama.Documentation.SampleCode.AspectFramework/OverrideMethodAspect.cs)]


In many cases, you will want your aspect to override _many_ methods. For instance, a _synchronized object_ aspect has to override all public instance methods and wrap them with a `lock` statement.

To override one or more methods, your aspect needs to implement the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method and invoke <xref:Metalama.Framework.Advising.IAdviceFactory.Override*?text=builder.Advice.Override> method.

The _first argument_ of `Override` is the <xref:Metalama.Framework.Code.IMethod> that you want to override. This method must be in the type being targeted by the current aspect instance.

The _second argument_ of `Override` is the name of the template method. This method must exist in the aspect class and, additionally:

* The template method must be annotated with the `[Template]` attribute,
* The template method must have a compatible return type and must have only parameters that exist in the target method with a compatible type. When the type is unknown, `dynamic` can be used. For instance, the following template method will match any method because it has no parameter (therefore will match any parameter list) and have the universal `dynamic` return type, which also matches `void`.

    ```cs
    dynamic? Template()
    ```

### Example: synchronized object

The following aspects wraps all instance methods with a `lock( this )` statement.

> [!NOTE]
> In a production-ready implementation, you should not lock `this` but a private field. You can introduce this field as described in <xref:introducing-members>. A product-ready implementation should also wrap properties.

[!include[Synchronized](../../../code/Metalama.Documentation.SampleCode.AspectFramework/Synchronized.cs)]

### Specifying templates for async and iterator methods

Instead of providing a single template method, you can provide several of them and let the framework choose which one is the most suitable. The principle of this feature is described above. Instead of passing a string to the second argument of `OverrideMethod`, you can pass a <xref:Metalama.Framework.Aspects.MethodTemplateSelector> and initialize it with many templates. See the reference documentation of <xref:Metalama.Framework.Advising.IAdviceFactory.Override*?> and <xref:Metalama.Framework.Aspects.MethodTemplateSelector> for details.

TODO: example