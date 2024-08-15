// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Documentation.Markdig.Extensions.Sandbox;

namespace Metalama.Documentation.Markdig.Extensions.Tabs;

internal class TransformedSingleFileCodeTab : CodeTab
{
    public TransformedSingleFileCodeTab( string fullPath ) : base( "transformed", fullPath, "Transformed", SandboxFileKind.None ) { }

    protected override string HtmlExtension => ".t.cs.html";
}