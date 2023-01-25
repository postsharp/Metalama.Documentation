---
uid: advising-concepts
---

# Transforming Code: Concepts

Aspects can transform the target code by providing _advice_. An advice is a primitive transformation of code. Advice is safely composable: several aspects that do not know about each other can add advice to the same declaration.

> [!NOTE]
> The word _advice_, in English, is uncountable, i.e. grammatically plural. The grammatically-correct singular form of _advice_ is _piece of advice_, but it seems very odd to use these words in a software engineering text. In aspect-oriented programming, _advice_ is a countable concept. We sometimes use _an advice_ for the singular form and _advices_ for the plural form, which may be shocking to native English speakers. Whenever possible, we use neutral turns of phrases, unless it would make the phrase cumbersome or less understandable.

There are two ways to add advice: _declaratively_ and _imperatively_.

## Declarative advising

The only _declarative advice_ is the _member introduction_ advice which is marked by the <xref:Metalama.Framework.Aspects.IntroduceAttribute> custom attribute. For each member of the aspect class annotated with `[Introduce]`, the aspect framework will attempt to introduce the member in the target class. For details, see <xref:introducing-members>.

## Imperative advising

_Imperative advice_ is added by the implementation of the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method thanks to the methods exposed by the <xref:Metalama.Framework.Aspects.IAspectBuilder.Advice> property of the `builder` parameter. See <xref:Metalama.Framework.Advising.IAdviceFactory> for a complete list of methods. In short:

* <xref:Metalama.Framework.Advising.IAdviceFactory.Override*> allows you to replace the implementation of a type member.
* <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceMethod*>, <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceProperty*>, <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceField*> and <xref:Metalama.Framework.Advising.IAdviceFactory.IntroduceEvent*> allows your aspect to introduce new members into the target type. See <xref:introducing-members> for details.
* <xref:Metalama.Framework.Advising.IAdviceFactory.ImplementInterface*> makes the target type implement an interface. See <xref:implementing-interfaces> for details.

## Template methods

With most kinds of advice, you have to provide a _template_ of the member that you want to add to the target type (whether a new member or a new implementation of an existing one).

Templates are made of standard C# code but mix two kinds of code: _compile-time_ and _run-time_. When some target code is advised, the compile-time part of the corresponding template is _executed_ and what results is the run-time code, which is then added to the source code.

For details, see <xref:templates>.