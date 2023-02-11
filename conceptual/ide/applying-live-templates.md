---
uid: applying-live-templates
---

# Using Live Templates

> [!NOTE]
> This scenario is in early preview.
> It requires __Visual Studio 2022__ and does not work in other IDEs.

## Step 1. Add a project or package reference.

Add the aspect library to your project using a `<ProjectReference>` or a `<PackageReference>`. This step is required to have the aspect registered under the refactoring menu. You can remove the reference after you applied the live template if needed.

   > [!NOTE]
   > If you use only live templates from this project, and no aspects, we recommend using the `PrivateAssets="all"` option, so the reference does not flow to the consumers of your project.

## Step 2. Apply the live template

1. Position the caret on the name of the declaration to which you want to apply the aspect.
2. Click on the lightbulb or refactoring icon, and choose _Apply live template_.
    ![Screenshot](LiveTemplate1.png)

3. Select the live template that you want to apply.
    ![Screenshot](LiveTemplate2.png)

