---
uid: validating-usage
---

# Validating usage of a class, member, or namespace

When designing software, one of the most important activities is to define _dependencies_ between components - i.e. to define who is allowed to call whom. In C#, this concept is called _accessibility_. To ensure optimal design, it is advisable to always grant the _least necessary_ accessibility. This is similar to the "need to know" principle in intelligence services and has benefits for software architecture by minimizing unintended _coupling_ between components and making it easier to change individual components in the future.

In C#, accessibility is defined within two kinds of scope: _assemblies_ and _types_. As you know, `private` members are only accessible from the current type, `protected` members are visible accessible from the current and any child type, `public` members are universally visible, and `internal` members are only accessible from the current assembly unless an `InternalsVisibleTo` extends the accessibility of internal members to other assemblies.

Large projects, however, often require finer control over accessibility than what C# can provide out-of-the-box. 

For instance, you might want to enforce one of the following rules:

* Require a specific method or constructor to be called from unit tests only, based on the caller namespace.
* Forbid a type from being accessed from outside its own namespace.
* Require a whole namespace to only be used by a friend namespace.
* Forbid internal members of a namespace from being accessed outside of that namespace.

The traditional approach to enforcing such rules is to use code comments and then rely on manual code reviews to enforce the desired design intent. Yet, this has two major weaknesses: it is prone to human errors and suffers from a lengthy feedback loop. Another approach is to split the codebase into a more fine-grained structure of projects, but this increases the complexity of the build and deployment, and negatively affects the application start-up time.

Thanks to Metalama, you can easily fine-tune the intended accessibility of your namespaces, types, or members using custom attributes or a compile-time API.

## Validating usage with custom attributes

When you want to fine-tune the accessibility of hand-picked types or members, the easiest solution is to use custom attributes.

Follow these steps:

1. Add the `Metalama.Extensions.Architecture` package to your project.

2. Apply one of the following custom attributes to the type or member for which you want to limit the accessibility. 

    | Attribute | Description |
    |-----------|-------------|
    | <xref:Metalama.Extensions.Architecture.Aspects.CanOnlyBeUsedFromAttribute> | Reports a warning when the target declaration is accessed from outside of the given scope.
    | <xref:Metalama.Extensions.Architecture.Aspects.InternalsCanOnlyBeUsedFromAttribute> |  Reports a warning when any `internal` member of the type is accessed from outside of the given scope. 
    | <xref:Metalama.Extensions.Architecture.Aspects.CannotBeUsedFromAttribute> | Reports a warning when the target declaration is accessed from the given scope.
    | <xref:Metalama.Extensions.Architecture.Aspects.InternalsCannotBeUsedFromAttribute> | Reports a warning when any `internal` member of the type is accessed from the given scope.
    
3. Set one or many of the following properties of the custom attribute, which control the scope i.e. which declarations can, or cannot, access the target declaration:
    
    | Property  | Description  |
    |---------|---------|
    | <xref:Metalama.Extensions.Architecture.Aspects.BaseUsageValidationAttribute.CurrentNamespace>     |  Includes the current namespace in the scope.       |
    | <xref:Metalama.Extensions.Architecture.Aspects.BaseUsageValidationAttribute.Types> | Includes a list of types in the scope. |
    | <xref:Metalama.Extensions.Architecture.Aspects.BaseUsageValidationAttribute.Namespaces> | Includes a list of namespaces in the scope by identifying them with a string. One asterisk (`*`) matches any namespace component but not the dot (`.`). A double asterisk (`**`) matches any substring including the dot (`.`).
    | <xref:Metalama.Extensions.Architecture.Aspects.BaseUsageValidationAttribute.NamespaceOfTypes>     |  Includes a list of the namespaces in the scope by identifying them with a type that they contain.
    
4. Optionally set the <xref:Metalama.Extensions.Architecture.Aspects.BaseUsageValidationAttribute.Description> property. The value of this property will be appended to the standard error message.

### Example: Test-only constructor

In the following example, the class `Foo` has two constructors, and one of them should only be used in tests. Tests are identified as any code in a namespace ending with the `.Tests` suffix. We define the <xref:Metalama.Extensions.Architecture.Aspects.BaseUsageValidationAttribute.Description> to improve the error message. You can also set the <xref:Metalama.Extensions.Architecture.Aspects.BaseUsageValidationAttribute.ReferenceKinds> to limit the kinds of references that are validated.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Attribute_ForTestOnly.cs  tabs="target"]

### Example: Type internals reserved for the current namespace

In the following example, the class `Foo` in namespace `A` uses the <xref:Metalama.Extensions.Architecture.Aspects.InternalsCanOnlyBeUsedFromAttribute> constraint to verify that internal members are only accessed from the same namespace. A warning is reported when an internal method of `Foo` is accessed from a different method.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Attribute_CurrentNamespace.cs tabs="target"]

## Validating usage programmatically

Custom attributes are adequate when the types or members to be validated have to be hand-picked. However, when these types or members can be selected by _rule_, it is better to do it programmatically, with compile-time code and [fabrics](xref:fabrics).

Follow these steps:

1. Add the `Metalama.Extensions.Architecture` package to your project.

2. Create or reuse a fabric type as described in <xref:fabrics>:

    * To concentrate the whole validation logic for the whole project into a single location, create a <xref:Metalama.Framework.Fabrics.ProjectFabric>.
    * To share the validation logic among several projects, see <xref:fabrics-many-projects>.
    * To split the logic on a per-namespace basis, create one <xref:Metalama.Framework.Fabrics.NamespaceFabric> in each namespace that you want to validate.
    * To validate specific types, you can use custom attributes, or you can add a nested <xref:Metalama.Framework.Fabrics.TypeFabric> to this type.

3. Import the <xref:Metalama.Extensions.Architecture.Fabrics> and <xref:Metalama.Extensions.Architecture.Predicates> namespaces to benefit from extension methods.

4. Edit the  <xref:Metalama.Framework.Fabrics.ProjectFabric.AmendProject*>,  <xref:Metalama.Framework.Fabrics.NamespaceFabric.AmendNamespace*> or  <xref:Metalama.Framework.Fabrics.TypeFabric.AmendType*> of this method. Open the dance by calling [amender.Verify()](xref:Metalama.Extensions.Architecture.Fabrics.AmenderExtensions.Verify*).

5. Call one of the following methods:

    | Attribute | Description |
    |-----------|-------------|
    | <xref:Metalama.Extensions.Architecture.Fabrics.ArchitectureVerifierExtensions.CanOnlyBeUsedFrom*> | Reports a warning when the target declaration is accessed from outside of the given scope.
    | <xref:Metalama.Extensions.Architecture.Fabrics.ArchitectureVerifierExtensions.InternalsCanOnlyBeUsedFrom*> |  Reports a warning when any `internal` member of the type is accessed from outside of the given scope. 
    | <xref:Metalama.Extensions.Architecture.Fabrics.ArchitectureVerifierExtensions.CannotBeUsedFrom*> | Reports a warning when the target declaration is accessed from the given scope.
    | <xref:Metalama.Extensions.Architecture.Fabrics.ArchitectureVerifierExtensions.InternalsCannotBeUsedFrom*> | Reports a warning when any `internal` member of the type is accessed from the given scope.

6. Pass a delegate like `r => r.ScopeMethod()` where `ScopeMethod` is one of the following methods:

    | Method | Description |
    |--------|-------------|
    | <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.CurrentNamespace*>|  Includes the current namespace in the scope.       |
    | <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.NamespaceOf*> | Includes the parent namespace of a given type in the scope |
    | <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.Type*> | Includes a given type in the scope.
    | <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.Namespace*> | Includes a given namespace in the scope. One asterisk (`*`) matches any namespace component but not the dot (`.`). A double asterisk (`**`) matches any substring including the dot (`.`).

    For instance:

    ```cs
    amender.Verify().CanOnlyBeUsedFrom( r => r.CurrentNamespace() );
    ```

    You can create complex conditions thanks to the <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.And*>, <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.Or*> and <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.Not*> methods.

7. Optionally, you can pass a value for the `description` parameter. This text will be appended to the warning message. You can also supply a <xref:Metalama.Framework.Validation.ReferenceKinds> to limit the kinds of references that are validated.

### Example: Namespace internals reserved for the current namespace

In the following example, we use a namespace fabric to restrict the accessibility of internal members to this namespace. A warning is reported when this rule is violated, like in the `ForbiddenInheritor` class.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Fabric_InternalNamespace.cs tabs="target"]

### Example: Forbidding the use of floating-point arithmetic from the Invoicing namespace

Using floating-point arithmetic in operations involving currencies is a common pitfall. Instead, `decimal` numbers should be used. In the following example, we use a project fabric to validate all references to the `float` and `double` types, and report a diagnostic when they are used from the `**.Invoicing` namespaces.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Fabric_ForbidFloat.cs tabs="target"]

## Extending the API with custom predicates

Predicates are extension methods like <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.CurrentNamespace*>, <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.NamespaceOf*> of the <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions> class. The role of predicates is to determine whether a given code reference should report a warning.

To implement a new predicate, follow the following steps:

1. Create a new class and derive it from <xref:Metalama.Extensions.Architecture.PredicatesReferencePredicate>. We recommend making this class `internal`.
2. Add fields for all parameters of the predicate, and initialize these fields from the constructor.

    > [!NOTE]
    > Predicate objects are serialized. Therefore, all fields must be serializable. Notably, objects of <<xref:Metalama.Extensions.Code.IDeclaration> type are not seriable. To serialize a declaration, call the <xref:Metalama.Extensions.Code.IDeclaration.ToRef*> method and store the returned <xref:Metalama.Extensions.Code.IRef`1>.

3. Implement the <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicate.IsMatch*> method. This method receives a <xref:Metalama.Framework.Validation.ReferenceValidationContext>. It must return `true` if the predicate matches the given context (i.e. the given code reference), otherwise `false`.
 
4. Create an extension method for the <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateBuilder> type and return a new instance of your predicate class.

To better understand the indications given in this section, you can study the [source code](https://github.com/postsharp/Metalama.Extensions/tree/master/src/Metalama.Extensions.Architecture) of the `Metalama.Extensions.Architecture`.


### Example: restricting usage based on calling method name

In the following example, we create a custom predicate `MethodNameEndsWith` which verifies that the code reference occurs within a method whose name ends with a given prefix.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Fabric_CustomPredicate.cs tabs="target"]

