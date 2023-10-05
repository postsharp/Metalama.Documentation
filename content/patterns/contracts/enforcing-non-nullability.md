---
uid: enforcing-non-nullability
---

# Checking all non-nullable fields, properties and parameters

If you are already using the nullability feature introduced in C# 8.0, you know that C# only reports a warning when there is an attempt to assign a null value a non-nullable field, property or parameter. C# does not generate the code that would throw an exception at run time if this happens. However, if your API is being consumed by code that is not under your control, it is still a good idea to check all values for null.

If you find this task repetitive, frustruating and unworthy of clean code, we are with you. 

Rejoice, solving this problem is a one-liner with Metalama! Just call the <xref:Metalama.Patterns.Contracts.ContractExtensions.VerifyNotNullableDeclarations*> method from your <xref:Metalama.Framework.Fabrics.ProjectFabric>.

> [!NOTE]
> By default, only your public API is verified. To add checks to your internal API, set the `includeInternalApis` parameter to `true`.

## Example: enforcing all non-nullable fields, properties and parameters

In the following example, we use the <xref:Metalama.Patterns.Contracts.ContractExtensions.VerifyNotNullableDeclarations*> method to inject null-checks for our complete public API. Yes, in just one line.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.Contracts/NotNullFabric.cs]
