---
uid: validation-extending
---

# Creating your own validation rules

In this article, we will show how to extend the [Metalama.Extensions.Architecture](https://www.nuget.org/packages/Metalama.Extensions.Architecture) package.

Instead of using `Metalama.Extensions.Architecture`, you can use directly Metalama Framework and add validators to your aspects or fabrics. For details, see <xref:aspect-validating>. However, the advantage of extending `Metalama.Extensions.Architecture` is that your extension APIs will have the same familiar look and feel as the Metalama APIs. A second benefit is that you may reuse some blocks built in  `Metalama.Extensions.Architecture` , and avoid a few pitfalls.

Note that `Metalama.Extensions.Architecture` is open source. To better understand the indications given in this section, you can study its [source code](https://github.com/postsharp/Metalama.Extensions/tree/master/src/Metalama.Extensions.Architecture).

## Creating custom predicates for usage validation

Predicates are extension methods like <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.CurrentNamespace*>, <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions.NamespaceOf*> of the <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateExtensions> class. The role of predicates is to determine whether a given code reference should report a warning.

To implement a new predicate, follow the following steps:

1. Create a new class and derive it from <xref:Metalama.Extensions.Architecture.PredicatesReferencePredicate>. We recommend making this class `internal`.
2. Add fields for all predicate parameters, and initialize these fields from the constructor.

    > [!NOTE]
    > Predicate objects are serialized. Therefore, all fields must be serializable. Notably, objects of <<xref:Metalama.Framework.Code.IDeclaration> type are not seriable. To serialize a declaration, call the <xref:Metalama.Framework.Code.IDeclaration.ToRef*> method and store the returned <xref:Metalama.Framework.Code.IRef`1>.

3. Implement the <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicate.IsMatch*> method. This method receives a <xref:Metalama.Framework.Validation.ReferenceValidationContext>. It must return `true` if the predicate matches the given context (i.e. the code reference), otherwise `false`.
 
4. Create an extension method for the <xref:Metalama.Extensions.Architecture.Predicates.ReferencePredicateBuilder> type and return a new instance of your predicate class.


### Example: restricting usage based on calling method name

In the following example, we create a custom predicate, `MethodNameEndsWith`, which verifies that the code reference occurs within a method whose name ends with a given prefix.

[!metalama-sample ~/code/Metalama.Documentation.SampleCode.AspectFramework/Architecture/Fabric_CustomPredicate.cs tabs="target"]

