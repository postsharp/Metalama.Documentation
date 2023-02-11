---
uid: migrating-multicasting
---

# Migrating PostSharp Attribute Multicasting to Metalama

Multicasting, in PostSharp, is a feature of all aspects that allows you to target several declarations using a single custom attribute or a single XML in the `postsharp.config` configuration file. Multicasting, in PostSharp, is exposed by `MulticastAttribute`, the ultimate base type of all aspect classes.

Multicasting is not implemented as a core feature in Metalama but as an extension. The reason for that decision is that the goal of adding an aspect to several declarations is better achieved in Metalama using _fabrics_. For details, see <xref:fabrics-aspects>.

Multicasting is provided in Metalama for backward compatibility with PostSharp because of our objective _not_ to require PostSharp users to change their _business code_ when they migrate to Metalama, but only their _aspect code_. Multicasting in Metalama is implemented by the <xref:Metalama.Extensions.Multicast> namespace. The implementation of this namespace is [open source](https://github.com/postsharp/Metalama.Extensions/tree/master/src/Metalama.Extensions.Multicast).


## Enabling multicasting for a simple aspect

If your aspect is based on <xref:Metalama.Framework.Aspects.OverrideMethodAspect> or <xref:Metalama.Framework.Aspects.OverrideFieldOrPropertyAspect>, you can enable multicasting by changing the base class to <xref:Metalama.Extensions.Multicast.OverrideMethodMulticastAspect> or <xref:Metalama.Extensions.Multicast.OverrideFieldOrPropertyMulticastAspect> respectively. Nothing else should be required.

If your aspect is derived from another base class, things are a bit more complex.

## Enabling multicasting for a blank aspect

Here are general instructions to add the multicasting feature to any aspect. You can check for yourself in the [source code](https://github.com/postsharp/Metalama.Extensions/tree/master/src/Metalama.Extensions.Multicast) of <xref:Metalama.Extensions.Multicast.OverrideMethodMulticastAspect> or <xref:Metalama.Extensions.Multicast.OverrideFieldOrPropertyMulticastAspect> that it follows these instructions.

### Step 1. Derive your class from MulticastAspect and implement IAspect<T> as appropriate

The easiest approach is for your aspect to derive from <xref:Metalama.Extensions.Multicast.MulticastAspect> instead of any other class.

The <xref:Metalama.Extensions.Multicast.MulticastAspect> class defines:

* all the properties that simulate the `MulticastAttribute` class from PostSharp, such as <xref:Metalama.Extensions.Multicast.MulticastAspect.AttributeTargetTypes> or <xref:Metalama.Extensions.Multicast.MulticastAspect.AttributeTargetMemberAttributes>, and
* a protected property <xref:Metalama.Extensions.Multicast.MulticastAspect.Implementation> that you can call from your derived classes to implement multicasting.

Your aspect must also implement the <xref:Metalama.Framework.Aspects.IAspect`1> interface for all relevant kinds of declarations:
* on the _final_ declarations on which the aspect is actually applied (i.e. does some actual work), and
* on any _intermediate_ declaration where the aspect does no other work than multicasting itself to select children declarations.

The <xref:Metalama.Extensions.Multicast.MulticastAspect> class already implements the `IAspect<ICompilation>` and  `IAspect<INamedType>` interfaces and properly implements the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method. For the interfaces that you implement yourself, you also have to implement `BuildAspect`.


### Step 2. Implement the BuildAspect methods

The _only_ thing that your implementation of the <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> method should do is to call the <xref:Metalama.Extensions.Multicast.MulticastImplementation.BuildAspect*?text=this.Implementation.BuildAspect> method. The arguments you need to pass depend on the kind of declaration of the implemented <xref:Metalama.Framework.Aspects.IAspect`1> interface:

* For the _intermediate_ declarations, you must pass a single argument: the <xref:Metalama.Framework.Aspects.IAspectBuilder`1>.
* For the _final_ declarations, pass the <xref:Metalama.Framework.Aspects.IAspectBuilder`1> _and_ a delegate that does the actual work. This delegate will be called _unless_ the aspect is skipped because of <xref:Metalama.Extensions.Multicast.MulticastAspect.AttributeExclude>.

Example:

```csharp
    public void BuildAspect( IAspectBuilder<IMethod> builder )
    {
        this.Implementation.BuildAspect(
            builder,
            b => b.Advice.Override( b.Target, nameof(this.TheTemplate) ) );
    }
```

### Step 3. Implement eligibility

If your aspect has eligibility requirements on the _type_ to which it is applied, override the <xref:Metalama.Extensions.Multicast.MulticastAspect.BuildEligibility(Metalama.Framework.Eligibility.IEligibilityBuilder{Metalama.Framework.Code.INamedType})?text=BuildEligibility(INamedType)> method.

Instead of repeating this eligibility condition in the `BuildEligibility` method for all final destinations of the aspect, you can call the `BuildEligibility(INamedType)` method like this:

```csharp
public override void BuildEligibility( IEligibilityBuilder<INamedType> builder )
{
    // Do not offer the aspect on static types.
    builder.MustNotBeStatic();
}

public void BuildEligibility( IEligibilityBuilder<IMethod> builder )
{
    // Include the conditions of the declaring type.
    this.BuildEligibility( builder.DeclaringType() );

    // Conditions that are specific to the method.
    builder.MustNotBeAbstract();
}
```



