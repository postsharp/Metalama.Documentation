// This is public domain Metalama sample code.

using System.IO;

namespace Metalama.Documentation.DfmExtensions;

internal class TransformedSingleFileCodeTab : CodeTab
{
    public TransformedSingleFileCodeTab( string tabId, string fullPath, string tabHeader ) : base(
        tabId,
        fullPath,
        SandboxFileKind.None,
        tabHeader: tabHeader ) { }

    protected override string HtmlExtension => ".t.cs.html";
}