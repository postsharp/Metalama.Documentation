using Docfx.MarkdigEngine.Extensions;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax;
using Metalama.Documentation.Docfx.Markdig.Helpers;

namespace Metalama.Documentation.Docfx.Markdig.Vimeo;

public class VimeoInlineParser : InlineParser
{
    private const string _startString = "[!metalama-vimeo";

    public VimeoInlineParser()
    {
        this.OpeningCharacters = ['['];
    }

    public override bool Match( InlineProcessor processor, ref StringSlice slice )
    {
        var saved = slice;

        if ( !ExtensionsHelper.MatchStart( ref slice, _startString, false ) )
        {
            return false;
        }

        if ( !slice.MatchArgument( out var id ) )
        {
            throw new InvalidOperationException( $"ID is missing for '{_startString}'" );
        }
        
        slice.EnsureClosingBracket();

        var vimeo = new VimeoInline
        {
            Id = id,
            Span = new SourceSpan(
                processor.GetSourcePosition( saved.Start, out var line, out var column ),
                processor.GetSourcePosition( slice.Start - 1 ) ),
            Line = line,
            Column = column
        };

        processor.Inline = vimeo;

        return true;
    }
}