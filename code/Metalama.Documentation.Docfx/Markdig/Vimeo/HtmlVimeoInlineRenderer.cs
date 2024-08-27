using Markdig.Renderers;
using Markdig.Renderers.Html;

namespace Metalama.Documentation.Docfx.Markdig.Vimeo;

public class HtmlVimeoInlineRenderer : HtmlObjectRenderer<VimeoInline>
{
    protected override void Write( HtmlRenderer renderer, VimeoInline obj ) => renderer.WriteLine( $"<div class='vimeo' data-id='{obj.Id}'></div>" );
}