// This is public domain Metalama sample code.

using Docfx.MarkdigEngine.Extensions;
using Markdig.Parsers;

namespace BuildMetalamaDocumentation.Markdig.Comments;

// Markdig doesn't always hide the comment block, so we do it ourselves.
public class CommentBlockParser : BlockParser
{
    public override BlockState TryOpen( BlockProcessor processor )
    {
        var slice = processor.Line;

        if ( processor.IsCodeIndent
             || !ExtensionsHelper.MatchStart( ref slice, "[comment]" ) )
        {
            return BlockState.None;
        }
        else
        {
            return BlockState.BreakDiscard;
        }
    }
}