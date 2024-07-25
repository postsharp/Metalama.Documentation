// This is public domain Metalama sample code.

using System.Collections.Generic;
using System.IO;

namespace Metalama.Documentation.DfmExtensions;

internal class TransformedSingleFileCodeTab : CodeTab
{
    public TransformedSingleFileCodeTab( string tabId, string fullPath, string tabHeader ) : base(
        tabId,
        fullPath,
        SandboxFileKind.None,
        tabHeader: tabHeader ) { }

    protected override IEnumerable<string> HtmlExtensions => [".t.cs.html", ".i.cs.html"];
}