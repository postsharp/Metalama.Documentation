// This is public domain Metalama sample code.

using Metalama.Documentation.Docfx.Markdig.Sandbox;

namespace Metalama.Documentation.Docfx.Markdig.Tabs;

internal class TransformedSingleFileCodeTab : CodeTab
{
    public TransformedSingleFileCodeTab( string tabId, string fullPath, string tabHeader ) : base(
        tabId,
        fullPath,
        SandboxFileKind.None,
        tabHeader: tabHeader ) { }

    protected override IEnumerable<string> HtmlExtensions => [".t.cs.html", ".i.cs.html"];
}