using Markdig;
using Markdig.Renderers;

namespace Metalama.Documentation.Docfx.Markdig.SingleFiles;

public class SingleFileInlineExtension : IMarkdownExtension
{
    public void Setup( MarkdownPipelineBuilder pipeline )
    {
        if ( !pipeline.InlineParsers.Contains<SingleFileInlineParser>() )
        {
            pipeline.InlineParsers.Insert( 0, new SingleFileInlineParser() );
        }
    }

    public void Setup( MarkdownPipeline pipeline, IMarkdownRenderer renderer )
    {
        if ( renderer is HtmlRenderer htmlRenderer )
        {
            htmlRenderer.ObjectRenderers.AddIfNotAlready<HtmlSingeFileInlineRenderer>();
        }
    }
}