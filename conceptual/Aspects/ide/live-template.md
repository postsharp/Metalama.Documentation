---
uid: live-template
---

# Exposing an Aspect as a Live Template

A _live template_ is a custom Quick Action available in the code editor in the lightbulb or screwdriver menu along other code fixes or refactoring actions offered by the IDE. For details about consuming live templates, see <xref:applying-live-templates>.

Live templates are built with the Metalama aspect framework, but instead of being executed at compile time by the compiler on intermediate code, they are applied in the editor and modify your _source_ code.

> [!NOTE]
> A fundamental characteristic of an aspect is that it is applied at compile time and does not affect your source code. Therefore, a live template cannot be named an aspect, even if it is built with the aspect framework. To avoid confusion, we suggest not to refer to live templates as aspects in your communications.

## To write a live template

1. Write an aspect class as usual, with the following differences:
   - The aspect class does not need to be derived from `System.Attribute`.
   - The implementation should pay more attention to generating idiomatic C# code.
   - Diagnostics reported by the aspect will be ignored.
   - Aspect ordering and requirements will be ignored.
2. Make sure that the aspect class has a default constructor.
3. Annotate the class with the <xref:Metalama.Framework.Aspects.LiveTemplateAttribute> attribute.
4. Make sure that you properly defined the aspect eligibility, so the code refactoring will only be suggested for relevant declarations. See <xref:eligibility> for details.