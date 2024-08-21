// This is public domain Metalama sample code.

using System.Collections.Generic;
using System.IO;

using Metalama.Documentation.Markdig.Extensions.Sandbox;

namespace Metalama.Documentation.Markdig.Extensions.Tabs;

internal class TransformedSingleFileCodeTab : CodeTab
{
    public TransformedSingleFileCodeTab( string tabId, string fullPath, string tabHeader ) : base(
        tabId,
        fullPath,
        SandboxFileKind.None,
        tabHeader: tabHeader ) { }

    protected override IEnumerable<string> HtmlExtensions => [".t.cs.html", ".i.cs.html"];
}