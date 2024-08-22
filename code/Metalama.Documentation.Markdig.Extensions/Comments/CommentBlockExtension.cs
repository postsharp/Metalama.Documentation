﻿using Markdig;
using Markdig.Renderers;

namespace Metalama.Documentation.Markdig.Extensions.Comments;

public class CommentBlockExtension : IMarkdownExtension
{
    public void Setup( MarkdownPipelineBuilder pipeline )
    {
        if ( !pipeline.BlockParsers.Contains<CommentBlockParser>() )
        {
            pipeline.BlockParsers.Insert( 0, new CommentBlockParser() );
        }
    }

    public void Setup( MarkdownPipeline pipeline, IMarkdownRenderer renderer ) { }
}