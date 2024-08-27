using Markdig;
using Markdig.Renderers;

namespace BuildMetalamaDocumentation.Markdig.ProjectButtons;

public class ProjectButtonsInlineExtension : IMarkdownExtension
{
    public void Setup( MarkdownPipelineBuilder pipeline )
    {
        if ( !pipeline.InlineParsers.Contains<ProjectButtonsInlineParser>() )
        {
            pipeline.InlineParsers.Insert( 0, new ProjectButtonsInlineParser() );
        }
    }

    public void Setup( MarkdownPipeline pipeline, IMarkdownRenderer renderer )
    {
        if ( renderer is HtmlRenderer htmlRenderer )
        {
            htmlRenderer.ObjectRenderers.AddIfNotAlready<HtmlProjectButtonsInlineRenderer>();
        }
    }
}