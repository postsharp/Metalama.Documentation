---
uid: configuration-custom-merge
level: 400
---

# Customizing the change merging process

By default, options are inherited along several axis (<xref:Metalama.Framework.Options.ApplyChangesAxis>). When several options apply to the same declaration, they are merged using the <xref:Metalama.Framework.Options.IIncrementalObject.ApplyChanges*> method. This method receives an <xref:Metalama.Framework.Options.ApplyChangesContext> object which exposes the <xref:Metalama.Framework.Options.ApplyChangesAxis> over which options are being merged.

> [!WARNING]
> In order to guarantee a consistent experience for Metalama users accross different aspect libraries, aspect authors are advised to only customize options inheritance when there is a compelling to do so. 


## Merging of options defined for a specific declaration

If several sources (like fabrics, attributes, aspects) set the options for the same declaration, then all these options are first merged at the level of the node on which they have been defined, regardless of any inheritance. The sources are evaluated in the following order:

1. Options provided by _custom attributes_ implementing <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider>, except aspects. When overriding options provided by competing custom attributes, Metalama uses the axis named <xref:Metalama.Framework.Options.ApplyChangesAxis.SameDeclaration>. 
2. Options set by _fabrics_ using  <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SetOptions*?text=amender.Outgoing.SetOptions>. Metalama still uses the <xref:Metalama.Framework.Options.ApplyChangesAxis.SameDeclaration> axis when applying options that stem from  <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SetOptions*>.
3. Options set by _aspects_ using <xref:Metalama.Framework.Aspects.IAspectReceiver`1.SetOptions*?text=aspectBuilder.Outgoing.SetOptions>. The <xref:Metalama.Framework.Options.ApplyChangesAxis.SameDeclaration> axis is still used.
4. Options provided by _aspects_ implementing <xref:Metalama.Framework.Options.IHierarchicalOptionsProvider>. This time, the <xref:Metalama.Framework.Options.ApplyChangesAxis.Aspect> axis is used.


## Merging of inherited options

When all options have been merged at the level of a specific declaration, inheritance rules apply. Options are applied in the following order of priority, the first items on the list being overridden by the next items.

1. Default options provided by the <xref:Metalama.Framework.Options.IHierarchicalOptions.GetDefaultOptions*> method for the current project.

2. Namespace-level options, from the root to the leafs. When merging namespace-level options with each other and with default options. The name of this axis is <xref:Metalama.Framework.Options.ApplyChangesAxis.ContainingDeclaration>.

3. Options of the base type or overridden member. When base options override namespace options, the <xref:Metalama.Framework.Options.ApplyChangesAxis.BaseDeclaration> axis is used. Note that when considering options of the base type or member, fully merged options are considered, which means that namespace-level options of the base type have precedence over the namespace-level of the current type. In case of cross-project type inheritance, the default options of the base project take precedence over the default options of the current project.

4. Options of the enclosing declaration, recursively, up to and not including the level of namespaces. When options of the declaring type override options inherited from the base type or member, the <xref:Metalama.Framework.Options.ApplyChangesAxis.ContainingDeclaration> axis is used.

5. Options defined on the target declaration itself. The name of this axis is <xref:Metalama.Framework.Options.ApplyChangesAxis.TargetDeclaration>.


These details may seem overly complex at first sight but they ensure a rather intuitive use of options.


## Disabling inheritance axes

If you don't want your options to be inherited along one of the above axes, you can annotate your option class with the <xref:Metalama.Framework.Options.HierarchicalOptionsAttribute?text=[HierarchicalOptions]> attribute and set one of these properties to `false`: <xref:Metalama.Framework.Options.HierarchicalOptionsAttribute.InheritedByDerivedTypes>, <xref:Metalama.Framework.Options.HierarchicalOptionsAttribute.InheritedByMembers>, <xref:Metalama.Framework.Options.HierarchicalOptionsAttribute.InheritedByNestedTypes>, or <xref:Metalama.Framework.Options.HierarchicalOptionsAttribute.InheritedByOverridingMembers>.


## Hand-tuning the merging process

If you need to customize the merging process even further, you can make your <xref:Metalama.Framework.Options.IIncrementalObject.ApplyChanges*> implementation depend on the <xref:Metalama.Framework.Options.ApplyChangesContext> parameter. It makes it possible to handle each option property differently according to the context.

Note that this possibility should be considered an extreme case and we don't see a valid use case at the time of writing this documentation.