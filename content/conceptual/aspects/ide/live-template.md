---
uid: live-template
---

# Exposing an aspect as a live template

A _live template_ is a custom Code Action available in the code editor menu like other code suggestions or refactoring actions offered by the IDE. For details about using live templates, see <xref:applying-live-templates>.

Live templates are built with the Metalama Aspect Framework. Instead of being executed at compile time by the compiler, they are interactively applied by the user in the editor, and they modify the source code.


> [!NOTE]
> A fundamental characteristic of an aspect is that it is applied at compile time and does not affect the source code. Therefore, a live template cannot be called an aspect, even if it is built with the aspect framework. To avoid confusion, we suggest not referring to live templates as aspects in your communication.

## To write a live template

1. Write an aspect class as usual, with the following differences:
   - The aspect class does not need to be derived from `System.Attribute`.
   - Pay attention to generating idiomatic C# code.
   - Diagnostics reported by the aspect will be ignored.
   - Aspect ordering and requirements will be ignored.
2. Make sure that the aspect class has a default constructor.
3. Annotate the class with `[EditorExperience(SuggestAsLiveTemplate = true)]`.
4. Make sure that you properly define the aspect eligibility, so the code refactoring will only be suggested for relevant declarations. See <xref:eligibility> for details.

