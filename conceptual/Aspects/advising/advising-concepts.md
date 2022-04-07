---
uid: advising-concepts
---

# Transforming Code: Concepts

Aspects can transform the target code by providing one or many _advices_. Advices are primitive transformations of code. Advices are safely composable: several aspects that do not know about each other can add advices to the same declaration.

There are two kinds of advices: _declarative_ and _imperative_.

## Declarative advices

The only _declarative advice_ is the _member introduction_ advice and is marked by the <xref:Metalama.Framework.Aspects.IntroduceAttribute> custom attribute. For each member of the aspect class annotated with `[Introduce]`, the aspect framework will attempt to introduce the member in the target class. For details, see <xref:introducing-members>.

## Imperative advices

_Imperative advices_ are added by the implementation of the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method thanks to the methods exposed by the <xref:Metalama.Framework.Aspects.IAspectLayerBuilder.Advices> property of the `builder` parameter. See <xref:Metalama.Framework.Aspects.IAdviceFactory> for a complete list of methods. In short:

* <xref:Metalama.Framework.Aspects.IAdviceFactory.OverrideMethod*>, <xref:Metalama.Framework.Aspects.IAdviceFactory.OverrideFieldOrProperty*>, <xref:Metalama.Framework.Aspects.IAdviceFactory.OverrideFieldOrPropertyAccessors*>,  <xref:Metalama.Framework.Aspects.IAdviceFactory.OverrideEventAccessors*> allow you to replace the implementation of a type member.
* <xref:Metalama.Framework.Aspects.IAdviceFactory.IntroduceMethod*>, <xref:Metalama.Framework.Aspects.IAdviceFactory.IntroduceProperty*>, <xref:Metalama.Framework.Aspects.IAdviceFactory.IntroduceField*> and <xref:Metalama.Framework.Aspects.IAdviceFactory.IntroduceEvent*> allows your aspect to introduce new members into the target type. See <xref:introducing-members> for details.
* <xref:Metalama.Framework.Aspects.IAdviceFactory.ImplementInterface*> makes the target type implement an interface. See <xref:implementing-interfaces> for details.

## Template methods

With most advices, you have to provide a _template_ of the member that you want to add to the target type (whether a new member or a new implementation of an existing one).

Templates are made of standard C# code but mix two kinds of code: _compile-time_ and _run-time_. When an advice is added to some target code, the compile-time part of the corresponding template is _executed_ and what results is the run-time code, which is then added to the source code.

For details, see <xref:templates>.