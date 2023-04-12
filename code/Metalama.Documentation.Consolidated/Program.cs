// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

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
    typeof(MetalamaPlugInAttribute),
    typeof(MetalamaDriver)
};