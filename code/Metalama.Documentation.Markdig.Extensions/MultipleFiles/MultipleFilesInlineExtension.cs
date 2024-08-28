using Markdig;
using Markdig.Renderers;

namespace Metalama.Documentation.Markdig.Extensions.MultipleFiles;

public class MultipleFilesInlineExtension : IMarkdownExtension
{
    public void Setup( MarkdownPipelineBuilder pipeline )
    {
        if ( !pipeline.InlineParsers.Contains<MultipleFilesInlineParser>() )
        {
            pipeline.InlineParsers.Insert( 0, new MultipleFilesInlineParser() );
        }
    }

    public void Setup( MarkdownPipeline pipeline, IMarkdownRenderer renderer )
    {
        if ( renderer is HtmlRenderer htmlRenderer )
        {
            htmlRenderer.ObjectRenderers.AddIfNotAlready<HtmlMultipleFilesInlineRenderer>();
        }
    }
}