---
uid: reflection
level: 300
---

# Generating System.Reflection objects

The code model is represented by the `Metalama.Framework.Code` namespace objects at compile time, and by the `System.Reflection` namespace at run time. Metalama is designed to eliminate the need for reflection in run-time code. However, there may be situations where you might require a run-time `System.Reflection` object.

To assist in these situations, the `Metalama.Framework.Code` namespace provides several methods that return the `System.Reflection` objects, representing the desired declaration at run time.

| Compile-time type | Run-time type | Conversion method |
|------------------|---------------|-------------------|
| <xref:Metalama.Framework.Code.IType> | <xref:System.Type> | <xref:Metalama.Framework.Code.IType.ToType> |
| <xref:Metalama.Framework.Code.IMemberOrNamedType> | <xref:System.Reflection.MemberInfo> | <xref:Metalama.Framework.Code.IMemberOrNamedType.ToMemberInfo> |
| <xref:Metalama.Framework.Code.IField> | <xref:System.Reflection.FieldInfo> | <xref:Metalama.Framework.Code.IField.ToFieldInfo> |
| <xref:Metalama.Framework.Code.IPropertyOrIndexer> | <xref:System.Reflection.PropertyInfo> | <xref:Metalama.Framework.Code.IPropertyOrIndexer.ToPropertyInfo> |
| <xref:Metalama.Framework.Code.IMethodBase> | <xref:System.Reflection.MethodBase> | <xref:Metalama.Framework.Code.IMethodBase.ToMethodBase> |
| <xref:Metalama.Framework.Code.IMethod> | <xref:System.Reflection.MethodInfo> | <xref:Metalama.Framework.Code.IMethod.ToMethodInfo> |
| <xref:Metalama.Framework.Code.IConstructor> | <xref:System.Reflection.ConstructorInfo> | <xref:Metalama.Framework.Code.IConstructor.ToConstructorInfo> |
| <xref:Metalama.Framework.Code.IParameter> | <xref:System.Reflection.ParameterInfo> | <xref:Metalama.Framework.Code.IParameter.ToParameterInfo> |

## Example

The following example demonstrates a method that returns a list of all methods represented as <xref:System.Reflection.MethodInfo> objects in the target type.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/EnumerateMethodInfos.cs name="Enumerate MethodInfos"]


