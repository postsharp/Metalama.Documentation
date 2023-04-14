// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Microsoft.DocAsCode.MarkdownLite;
using System.IO;

namespace Metalama.Documentation.DfmExtensions;

internal class CompareFileRenderer : BaseRenderer<CompareFileToken>
{
    protected override StringBuffer RenderCore( CompareFileToken token, MarkdownBlockContext context )
    {
        var name = Path.GetFileNameWithoutExtension( token.Src ).ToLowerInvariant();

        var compareTab = new CompareTab( name, name, token.Src );

        return compareTab.GetTabContent();
    }
}