---
uid: pipeline
---

# Compilation pipeline

### Step 1. Initialization

1. Generation of the compile-time compilation:
    1. Referenced compile-time projects are identified and loaded.
    2. Compile-time code is identified in the current compilation and a separate compile-time compilation is created.
        1. Templates are transformed into code generating Roslyn syntax trees.
        2. Expressions `nameof` and `typeof` are transformed to make them independent from run-time references.

2. Initialization of aspect classes.
    1. A prototype instance of each aspect class is created using `FormatterServices.GetUninitializedObject`.
    2. The <xref:Metalama.Framework.Eligibility.IEligible`1.BuildEligibility*> method is executed executed. Aspect layers are discovered.
    3. Aspect ordering relationships are discovered in the current project and all referenced assemblies.
    4. Aspects layers are ordered.

### Step 2. Applying aspects

For each aspect layer, by order of application (i.e., inverse order of execution, see <xref:ordering-aspects>):

* For the default aspect layer:
  * Aspect sources are evaluated for this aspect type, resulting in a set of _target declarations_.
  * Target declarations are visited in [breadth-first order](https://en.wikipedia.org/wiki/Breadth-first_search) of _depth level_, as defined above. For each target declaration:
    * The aspect is instantiated.
    * <xref:Metalama.Framework.Aspects.IAspect`1.BuildAspect*> is invoked.
    * Advice are added to the next steps of the pipeline.

* For all aspect layers, and for all target declarations visited in breadth-first order:
  * Advice are executed. Advice can provide observable or non-observable transformations (or both), defined as follows:
    * _observable transformations_ are transformations that affect _declarations_, i.e. they are visible from the code model or from the source code (for instance: introducing a method);
    * _non-observable transformations_ are transformations that only affect the _implementation_ of declarations (for instance: overriding a method).

* Before we execute the next aspect layer or the next visiting depth, a new code model version is created incrementally from the previous version, including any observable transformation added by an advice before.

### Step 3. Transforming the compilation

Before this step, the algorithm collected transformations, but the compilation was never modified.

What happens next depends on whether the pipeline runs at design time or compile time.

#### Compile time

1. All transformations (observable and non-observable) are introduced into a new compilation. Templates are expanded at this moment.
2. The code is linked together and inlined where possible.

#### Design time

At design time, non-observable transformations are ignored, and partial files are created for observable transformations.
Templates are never executed at design time.
