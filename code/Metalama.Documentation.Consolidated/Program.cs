// This is public domain Metalama sample code.

using Metalama.Compiler;
using Metalama.Framework.Aspects;
using Metalama.Framework.Engine.AspectWeavers;
using Metalama.Framework.Introspection;
using Metalama.Framework.Workspaces;
using Metalama.LinqPad;

Console.WriteLine( "Hello, World!" );

// // This is to make sure that all packages are properly referenced.
_ = new[]
{
    typeof(IAspect),
    typeof(AspectWeaverContext),
    typeof(Workspace),
    typeof(IIntrospectionAspectClass),
    typeof(ISourceTransformer),
    typeof(MetalamaWorkspaceDriver)
};