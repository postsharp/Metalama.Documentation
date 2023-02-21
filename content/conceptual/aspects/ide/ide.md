---
uid: building-ide-interactions
level: 200
---

# Building IDE interactions

You will almost certainly be aware of the code fixes and refactorings offered by your IDE via the screwdriver or lightbulb icons in the editor. These code actions have been programmed by the manufacturer of your IDE.

In this chapter, you will learn how to add your own actions to the screwdriver or lightbulb menus, and integrate them with the other features of Metalama.

## Benefits

The benefits of building custom IDE interactions for your architecture and your team are:

* **Fewer keystrokes**. Developers will save typing time and effort.
* **Fewer documentation lookups**. A code fix provides implicit implementation guidance; team members do not need to look at the documentation to know how to fix the custom warning or error that was reported by your aspect.
* **Improved team alignment**. By providing live templates for those implementation scenarios that occur most often, you are encouraging the team to follow common development patterns. The resulting codebase will be more consistent.
* **Higher developer productivity**. As a result, you will enhance developer productivity by providing them with custom code fixes that are relevant to their current code.

## In this chapter

This chapter is composed of the following articles:

| Article | Description |
|---------|-------------|
| <xref:live-template> | This article shows how to expose an aspect as a code refactoring that can be applied directly to _source_ code instead of being applied in the background to the executable code. |
| <xref:code-fixes> | This article describes how aspects and validators can suggest code fixes and refactorings. |

