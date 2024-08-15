using Markdig;
using Markdig.Renderers;

namespace Metalama.Documentation.Markdig.Extensions.AspectTests;

public class AspectTestInlineExtension : IMarkdownExtension
{
    public void Setup( MarkdownPipelineBuilder pipeline )
    {
        if ( !pipeline.InlineParsers.Contains<AspectTestInlineParser>() )
        {
            pipeline.InlineParsers.Insert( 0, new AspectTestInlineParser() );
        }
    }

    public void Setup( MarkdownPipeline pipeline, IMarkdownRenderer renderer )
    {
        if ( renderer is HtmlRenderer htmlRenderer )
        {
            htmlRenderer.ObjectRenderers.AddIfNotAlready<HtmlAspectTestInlineRenderer>();
        }
    }
}