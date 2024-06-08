// This is public domain Metalama sample code.

namespace Metalama.Documentation.DfmExtensions;

internal class TransformedSingleFileCodeTab : CodeTab
{
    public TransformedSingleFileCodeTab( string fullPath ) : base( "transformed", fullPath, "Transformed", SandboxFileKind.None ) { }

    protected override string HtmlExtension => ".t.cs.html";
}