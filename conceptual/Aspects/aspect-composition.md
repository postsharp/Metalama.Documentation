---
uid: aspect-composition
---

# Aspect composition

What happens when several aspects are applied to the same class? This problem is called _aspect composition_. This is a very hard problem. The good news is that Metalama provides a consistent and deterministic model to cope with that situation. 

There are three major points of interest:

1. Strong ordering of aspects and advice
2. Code model versioning
3. Safe composition of advice


## 1. Strong ordering of aspects and advice

Aspects are "things" that receive a code model as input, and provides a number of outputs: advice, diagnostics, validators, and children aspects. The only important output for this discussion are _advice_ because the other outputs do not modify the code. Most aspects have a single layer of advice, but it is possible to define several layers.

To make sure that the order of execution of aspects and advice is consistent, Metalama uses two ordering factors:

1. _Aspect layer_. The order of execution of aspect layers _must_ be specified by the aspect author or user. To learn how to specify the order of aspect classes and layers, see <xref:ordering-aspects>. 
   
2. _Depth Level_ of target declarations. Every declaration in the compilation is assigned a _depth level_. Within the same aspect layer, declarations are processed by increasing order of depth, i.e. base classes are visited before derived classes, and types before their members, and so on.

Aspects and ddvices in the same layer and applied to declarations of the same depth are executed in undetermined order, and may be executed concurrently on several threads.


## 2. Code model versioning

Since the code model only represents declarations but does not give access to implementations such as method bodies or initializers, the only kind of advice that affect the code model are introductions and interface implementations. Overriding an existing method does not affect the code model because it only overrides its implementation.

For each aspect layer and depth level, Metalama will create a new version of the code model that reflects the changes done by the previous aspect layer or depth layer.

Therefore, if an aspect introduces a member into a type, the next aspects will see that new member in the code model, and will be able to advise it.

To ensure the consistency of this model, aspects cannot provide outputs to previous aspects or to declarations that are not "under" the current target.

## 3. Safe composition of advice

When several aspects that are not aware of each other add an advice to the same declaration, Metalama guarantees that the resulting code will be correct. 

For instance, if two aspects override the same method with, both templates are guaranteed to compose correctly. This is a very hard problem, but it is solved by Metalama, so you don't have to bother about it.

TODO: example log and cache

