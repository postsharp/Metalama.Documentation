---
uid: fabrics-validation
---

# Validating code with fabrics

[comment]: # (TODO)


## Adding validators with a fabric

The article <xref:validation> explains how to validate code with an aspect.
Validating code with a fabric is exactly the same, except that you use the _amender_ parameter instead of the _aspectBuilder_.

## Using validation API

We have a vision for validation using fabrics, but it is not implemented yet. However, you can implement it yourself for your own projects.

That vision is to create a validation API that would be exposed as extension methods of the _amender_ parameter. For instance, if we have an `INamespaceAmender`, we could create an extension method `MakeNamespaceInternal(this INamespaceAmender)` that would forbid the use of internal members outside this namespace.

To achieve something similar yourself in Metalama today, you can do the following:

1. Create an aspect that implements the functionality. See <xref:validation>. Note that the aspect does not need to be a custom attribute. You must create an aspect because, if you are creating a validation API, you do not "own" the fabric class, but validator methods must be either in a fabric class or in an aspect class.

2. Create a front-end API that exposes the amender class and adds your aspect.



