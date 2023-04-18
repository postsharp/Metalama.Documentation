// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

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
            : new CodeTab( name, token.Src, name, SandboxFileKind.ExtraCode, token.From, token.To, token.Member );

        return "<div class='single-file'>" + tab.GetTabContent( false ) + "</div>";
    }
}