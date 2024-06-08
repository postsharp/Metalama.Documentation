// This is public domain Metalama sample code.

using Microsoft.DocAsCode.MarkdownLite;
using System.IO;

namespace Metalama.Documentation.DfmExtensions;

internal class SingleFileRenderer : BaseRenderer<SingleFileToken>
{
    protected override StringBuffer RenderCore( SingleFileToken token, MarkdownBlockContext context )
    {
        var name = Path.GetFileNameWithoutExtension( token.Src );

        var tab = token.ShowTransformed
            ? new TransformedSingleFileCodeTab( token.Src )
            : new CodeTab( name, token.Src, name, SandboxFileKind.ExtraCode, token.Marker, token.Member );

        return "<div class='single-file'>" + tab.GetTabContent( false ) + "</div>";
    }
}