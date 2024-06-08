// This is public domain Metalama sample code.

using Microsoft.DocAsCode.MarkdownLite;

namespace Metalama.Documentation.DfmExtensions;

internal class VimeoRenderer : BaseRenderer<VimeoToken>
{
    protected override StringBuffer RenderCore( VimeoToken token, MarkdownBlockContext context )
    {
        return $"<div class='vimeo' data-id='{token.Id}'></div>";
    }
}