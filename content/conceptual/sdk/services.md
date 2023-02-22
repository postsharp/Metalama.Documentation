---
uid: services
level: 400
---

# SDK services

> [!WARNING]
> This feature is not completed and does not work as documented.

Aspects generally do not get access to the underlying Roslyn code model of the current project. The most serious consequence of this limitation is that aspects have no access to the syntax tree of method implementation -- indeed, the <xref:Metalama.Framework.Code> namespace only exposes declarations, but not implementation syntax.

If your aspect needs to read the syntax tree, it can do it through an _SDK service_. An SDK service is an interface that is defined in a normal project (and is therefore usable by the aspect) but is implemented in a weaver project (and can therefore access the Roslyn API).

## Creating an SDK service

### Step 1. Create the solution scaffolding

This step is described in <xref:sdk-scaffolding>.

### Step 2. Define the interface

In the _public project_ created in the previous step, define the service interface. The interface can be internal if the public project has exposed its internals to the weaver project.


### Step 3. Implement the interface

In the weaver project created in Step 1:

1. Add a class that implements the service interface defined in the previous step.
2. Make sure the class is `public`.
3. Add the <xref:Metalama.Compiler.MetalamaPlugInAttribute> custom attribute to this class.


[comment]: # (TODO: example)

## Consuming an SDK service

In your aspect class, you can get your service implementation from the <xref:Metalama.Framework.Services.IServiceProvider`1> exposed on the <xref:Metalama.Framework.Project.IProject> interface. This interface is available from any declaration of the code model through `declaration.Compilation.Project`, or from the <xref:Metalama.Framework.Project.MetalamaExecutionContext> static class.

> [!WARNING]
> SDK services are not available at design time.



