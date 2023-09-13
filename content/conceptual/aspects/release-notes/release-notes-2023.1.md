---
uid: release-notes-2023.1
---

# Metalama 2023.1

Metalama 2023.2 focuses on bugfixes and minor enhancements.


## Platform updates

* Visual Studio 17.6
* Roslyn 4.6
* .NET SDK 7.0.300.


## Enhancements

- Added support for Visual Studio 17.6, Roslyn 4.6, and .NET SDK 7.0.300.
- The <xref:Metalama.Framework.Advising.IAdviceFactory.ImplementInterface*?text=IAdviceFactory.ImplementInterface> method now exposes the created members under the <xref:Metalama.Framework.Advising.IImplementInterfaceAdviceResult.InterfaceMembers?IImplementInterfaceAdviceResult.InterfaceMembers> property.
- Introducing the new extension method <xref:Metalama.Framework.Code.TypeExtensions.ToTypeOfExpression*?text=IType.ToTypeOfExpression>, which returns an <xref:Metalama.Framework.Code.IExpression>.
- Contracts now support `IEnumerable`.
- Ability to make an introduced field `readonly`.
- Improved error reporting for aspect members with more than one advice/template attribute.
- Enhanced error message when referencing a non-existing type in compile-time code.
- In templates, `foreach` loops are now allowed in run-time-conditional blocks.
- Better error reporting when the `[Template]` attribute is used on accessors.
