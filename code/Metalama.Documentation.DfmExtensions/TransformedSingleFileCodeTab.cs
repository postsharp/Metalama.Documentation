// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

namespace Metalama.Documentation.DfmExtensions;

internal class TransformedSingleFileCodeTab : CodeTab
{
    public TransformedSingleFileCodeTab( string fullPath ) : base( "transformed", fullPath, "Transformed", SandboxFileKind.None ) { }

    protected override string HtmlExtension => ".t.cs.html";
}