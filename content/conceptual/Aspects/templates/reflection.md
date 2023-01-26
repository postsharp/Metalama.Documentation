---
uid: reflection
---

# Generating System.Reflection objects

At compile time, the code mode is represented by objects of the `Metalama.Framework.Code` namespace. However, at run time, the code model is represented by the `System.Reflection` namespace. Although Metalama is designed in such a way that you do need any reflection in run-time code, there are still situation where you will want to get a `System.Reflection` object in your run-time code.

To help in this situation, the  `Metalama.Framework.Code` exposes a few methods that generate expressions that return the `System.Reflection` object that represents the desired declaration at run time.


| Compile-time type | Run-time type | Conversion method
|------------------|-|-
| <xref:Metalama.Framework.Code.IType> | <xref:System.Type> | <xref:Metalama.Framework.Code.IType.ToType>
| <xref:Metalama.Framework.Code.IMemberOrNamedType> | <xref:System.Reflection.MemberInfo> | <xref:Metalama.Framework.Code.IMemberOrNamedType.ToMemberInfo>
| <xref:Metalama.Framework.Code.IField> | <xref:System.Reflection.FieldInfo> | <xref:Metalama.Framework.Code.IField.ToFieldInfo>
| <xref:Metalama.Framework.Code.IPropertyOrIndexer> | <xref:System.Reflection.PropertyInfo> | <xref:Metalama.Framework.Code.IPropertyOrIndexer.ToPropertyInfo>
| <xref:Metalama.Framework.Code.IMethodBase> | <xref:System.Reflection.MethodBase> | <xref:Metalama.Framework.Code.IMethodBase.ToMethodBase>
| <xref:Metalama.Framework.Code.IMethod> | <xref:System.Reflection.MethodInfo> | <xref:Metalama.Framework.Code.IMethod.ToMethodInfo>
| <xref:Metalama.Framework.Code.IConstructor> | <xref:System.Reflection.ConstructorInfo> | <xref:Metalama.Framework.Code.IConstructor.ToConstructorInfo>
| <xref:Metalama.Framework.Code.IParameter> | <xref:System.Reflection.ParameterInfo> | <xref:Metalama.Framework.Code.IParameter.ToParameterInfo>


## Example

The following examples introduce a method that returns a list of all methods, represented as a <xref:System.Reflection.MethodInfo> object, in the target type.

[!include[Enumerate MethodInfos](../../../../code/Metalama.Documentation.SampleCode.AspectFramework/EnumerateMethodInfos.cs)]


