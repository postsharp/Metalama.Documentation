---
uid: services
level: 400
---

# SDK services

> [!WARNING]
> This feature is not yet complete and may not function as documented.

Aspects typically do not have access to the underlying Roslyn code model of the current project. The most significant consequence of this limitation is that aspects cannot access the syntax tree of method implementation. Indeed, the <xref:Metalama.Framework.Code> namespace only exposes declarations, not implementation syntax.

If your aspect requires access to the syntax tree, it can achieve this through an _SDK service_. An SDK service is an interface defined in a standard project (thus, it can be used by the aspect) but is implemented in a weaver project (therefore, it can access the Roslyn API).

## Creating an SDK service

### Step 1. Create the solution scaffolding

This step is detailed in <xref:sdk-scaffolding>.

### Step 2. Define the interface

In the _public project_ created in the previous step, define the service interface. The interface can be internal if the public project has granted access to its internals to the weaver project.

### Step 3. Implement the interface

In the weaver project created in Step 1:

1. Add a class that implements the service interface defined in the previous step.
2. Ensure the class is `public`.
3. Attach the <xref:Metalama.Compiler.MetalamaPlugInAttribute> custom attribute to this class.

[comment]: # (TODO: example)

## Consuming an SDK service

In your aspect class, you can retrieve your service implementation from the <xref:Metalama.Framework.Services.IServiceProvider`1> exposed on the <xref:Metalama.Framework.Project.IProject> interface. This interface is accessible from any declaration of the code model through `declaration.Compilation.Project`, or from the <xref:Metalama.Framework.Project.MetalamaExecutionContext> static class.

> [!WARNING]
> SDK services are not available at design time.


