---
uid: fabrics-validation
---

# Validating Code with Fabrics

(TODO)

## Adding validators from a fabric

The article <xref:validation> explains how to validate code from an aspect.

Validating code from a fabric is exactly the same, but you use the _amender_ parameter instead of the _aspectBuilder_.

## Using validation API

We have a vision for validation using fabrics, but it is not implemented yet. However, you can implemented it yourself for your own projects.

That vision is to create a validation API that would be exposed as extension methods of the _amender_ parameter. For instance, if we have an `INamespaceAmender`, we could create an extension method `MakeNamespaceInternal(this INamespaceAmender)` that would forbid the use of internal members of this namespace to be used out of this namespace.

<!--- rather than the line immediatly below this I think the following might be better
To achieve something similar yourself in Metalama at the moment you can do the following: -->
To realize this vision, the steps are the following:

1. Create an aspect that implements the functionality as described in <xref:validation>. Note that the aspect does not need to be a custom attribute. You must create an aspect because, if you are creating a validation API, you do not "own" the fabric class, but validator methods must be either in a fabric class or in an aspect class.

2. Create a front-end API that exposes the amender class and adds your aspect.



