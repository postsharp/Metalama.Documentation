---
uid: introducing-members
---
# Introducing Members

In the previous articles, you have learned how to override the implementation of existing type members. In this article, you will learn how to add new members to an existing type.

You can currently add the following kinds of members:

- methods,
- fields,
- properties,
- events.

However, the following kind of members are _not_ yet supported:

- operators,
- conversions,
- constructors.

## Introducing members declaratively

The easiest way to introduce a member from an aspect is to implement this member in the aspect and annotate it with the <xref:Metalama.Framework.Aspects.IntroduceAttribute?text=[Introduce]> custom attribute.  This custom attribute has the following interesting properties:

<table>
    <tr>
        <th>Property</th>
        <th>Description</th>
    </tr>
    <tr>
        <td>
            <xref:Metalama.Framework.Aspects.TemplateAttribute.Name>
        </td>
        <td>
            Sets the name of the introduced member. If not specified, the name of the introduced member is the name of the template itself.
        </td>
    </tr>
    <tr>
        <td>
            <xref:Metalama.Framework.Aspects.IntroduceAttribute.Scope>
        </td>
        <td>
            Decides whether the introduced member will be `static` or not. See <xref:Metalama.Framework.Aspects.IntroductionScope> for possible strategies. By default, it is copied from the template, except when the aspect is applied to a static member, in which case the introduced member is always `static`.
        </td>
    </tr>
    <tr>
        <td>
            <xref:Metalama.Framework.Aspects.TemplateAttribute.Accessibility>
        </td>
        <td>
            Determines if the member will be `private`, `protected`, `public`, etc. By default, the accessibility of the template is copied.
        </td>
    </tr>
    <tr>
        <td>
            <xref:Metalama.Framework.Aspects.TemplateAttribute.IsVirtual>
        </td>
        <td>
            Determines if the member will be `virtual`. By default, the characteristic of the template is copied.
        </td>
    </tr>
    <tr>
        <td>
            <xref:Metalama.Framework.Aspects.TemplateAttribute.IsSealed>
        </td>
        <td>
            Determines if the member will be `sealed`. By default, the characteristic of the template is copied.
        </td>
    </tr>
</table>

### Example: ToString

The following example shows an aspect that implements the `ToString` method. It will return a string including the object type and a reasonably unique identifier for that object.

Note that this aspect will replace any hand-written implementation of `ToString`, which is not desirable. It can can only be avoided by introducing the method programmatically and conditionally (TODO 28807).

[!include[ToString](../../../code/Metalama.Documentation.SampleCode.AspectFramework/IntroduceMethod.cs)]

## Introducing members programmatically

The principal limitation of declarative introductions is that the name, type and signature of the introduced member must be known upfront. They cannot depend on the target aspect. The programmatic approach allows your aspect to completely customize the declaration based on the target code.

There are three steps to introduce a member programmatically:

### Step 1. Implement the template

Implement the template in your aspect class and annotate it with the <xref:Metalama.Framework.Aspects.TemplateAttribute?text=[Template]> custom attribute. The template does not need to have the final signature.

### Step 2. Invoke IAdviceFactory.Introduce*

In your implementation of the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method, call one of the following methods and store the return value in a variable.

- <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceMethod*> returning an <xref:Metalama.Framework.Code.DeclarationBuilders.IMethodBuilder>;

- <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceProperty*> returning an <xref:Metalama.Framework.Code.DeclarationBuilders.IPropertyBuilder>;

- <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceEvent*> returning an <xref:Metalama.Framework.Code.DeclarationBuilders.IEventBuilder>;

- <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceField*> returning an <xref:Metalama.Framework.Code.DeclarationBuilders.IFieldBuilder>.

A call to these method creates a member that has the same characteristics as the template (name, signature, ...), taking into account the properties of the <xref:Metalama.Framework.Aspects.TemplateAttribute?text=[Template]> custom attribute. However, they return a _builder_ object that allows you to modify these characteristics.

### Step 3. Adjust the name and signature using the builder

Modify the name and signature of the introduced declaration as needed using the _builder_ object returned by the advice factory method.

### Example: Update method

The following aspect introduces an `Update` method that assigns all writable field in the target type. The method signature is dynamic: there is one parameter per writable field or property.

[!include[Update method](../../../code/Metalama.Documentation.SampleCode.AspectFramework/UpdateMethod.cs)]

## Overriding existing implementations

### Specifying the override strategy

When you want to introduce a member to a type, it may happen that the same member is already defined in this type or in a parent type. The default strategy of the aspect framework in this case it simply to report an error and fail the build. You can change this behavior by setting the <xref:Metalama.Framework.Aspects.OverrideStrategy> for this advice:

- For declarative advice, set the <xref:Metalama.Framework.Aspects.IntroduceAttribute.WhenExists> property of the custom attribute,
- For programmatic advice, set the _whenExists_ optional parameter of the advice factory method.

[comment]: # (TODO: The implementation and documentation are not final. Another property and parameter should be defined to cope with the case when the member is inherited.)

### Accessing the overridden declaration

Most of the times, when you override a method, you will want to invoke the aspect to invoke the base implementation. The same applies to properties and events. In plain C#, when you override a base-class member in a derived class, you call the member with the `base` prefix. A similar approach exists in Metalama.

- To invoke the base method or accessor with exactly the same arguments, call <xref:Metalama.Framework.Aspects.meta.Proceed?text=meta.Proceed*>.
- To invoke the base method with different arguments, use <xref:Metalama.Framework.Code.Advised.IAdvisedMethod.Invoke(System.Object[])?text=meta.Target.Method.Invoke>.
- To call the base property getter or setter, use <xref:Metalama.Framework.Code.IExpression.Value?text=meta.Property.Value>.
- To access the base event, use <xref:Metalama.Framework.Code.Advised.IAdvisedEvent.AddMethod?text=meta.Event.AddMethod.Invoke*>.

[comment]: # (TODO: When it will work, Disposable example.)

## Referencing introduced members in template

When you introduce a member to a type, your will often want to access it from templates. There are three ways to do it:

### Option 1. Access the aspect template member

[!include[Introduce OnPropertyChanged](../../../code/Metalama.Documentation.SampleCode.AspectFramework/IntroducePropertyChanged1.cs)]

### Option 2. Use `meta.This` and write dynamic code

[!include[Introduce OnPropertyChanged](../../../code/Metalama.Documentation.SampleCode.AspectFramework/IntroducePropertyChanged3.cs)]

### Option 3. Use the invoker of the builder object

If none of the approaches above offer you the required flexibility (typically because the name of the introduced member is dynamic), use the invokers exposed on the builder object returned from the advice factory method.

> [!NOTE]
> Declarations introduced by an aspect or aspect layer are not visible in the `meta` code model exposed to in the same aspect or aspect layer. To reference builders, you have to reference them differently. For details, see <xref:sharing-state-with-advice>.

For details, see <xref:Metalama.Framework.Code.Invokers>

[!include[Introduce OnPropertyChanged](../../../code/Metalama.Documentation.SampleCode.AspectFramework/IntroducePropertyChanged2.cs)]

## Referencing introduced members from source code

If you want the _source_ code (not your aspect code) to reference declarations introduced by your aspect, the _user_ of your aspect needs to make the target types `partial`. Without this keyword, the introduced declarations will not be visible at design time in syntax completion, and the IDE will report errors. Note that the _compiler_ will not complain because Metalama replaces the compiler, but the IDE will because it does not know about Metalama, and here Metalama, and therefore your aspect, has to follow the rules of the C# compiler. How inconvenient it may be, there is nothing you as an aspect author, or us as the authors of Metalama, can do.

If the user does not add the `partial` keyword, Metalama will report a warning and offer a code fix.

> [!NOTE]
> In __test projects__ built using `Metalama.TestFramework`, the Metalama compiler is _not_ activated. Therefore, the source code of test projects cannot reference introduced declarations. Since the present documentation relies on `Metalama.TestFramework` for all examples, we cannot include an example here.
