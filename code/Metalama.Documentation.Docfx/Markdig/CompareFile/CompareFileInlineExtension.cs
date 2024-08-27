using Markdig;
using Markdig.Renderers;

namespace Metalama.Documentation.Docfx.Markdig.CompareFile;

public class CompareFileInlineExtension : IMarkdownExtension
{
    public void Setup( MarkdownPipelineBuilder pipeline )
    {
        if ( !pipeline.InlineParsers.Contains<CompareFileInlineParser>() )
        {
            pipeline.InlineParsers.Insert( 0, new CompareFileInlineParser() );
        }
    }

    public void Setup( MarkdownPipeline pipeline, IMarkdownRenderer renderer )
    {
        if ( renderer is HtmlRenderer htmlRenderer )
        {
            htmlRenderer.ObjectRenderers.AddIfNotAlready<HtmlCompareFileInlineRenderer>();
        }
    }
}