using Markdig;
using Markdig.Renderers;

namespace Metalama.Documentation.Markdig.Extensions.Vimeo;

public class VimeoInlineExtension : IMarkdownExtension
{
    public void Setup( MarkdownPipelineBuilder pipeline )
    {
        if ( !pipeline.InlineParsers.Contains<VimeoInlineParser>() )
        {
            pipeline.InlineParsers.Insert( 0, new VimeoInlineParser() );
        }
    }

    public void Setup( MarkdownPipeline pipeline, IMarkdownRenderer renderer )
    {
        if ( renderer is HtmlRenderer htmlRenderer )
        {
            htmlRenderer.ObjectRenderers.AddIfNotAlready<HtmlVimeoInlineRenderer>();
        }
    }
}