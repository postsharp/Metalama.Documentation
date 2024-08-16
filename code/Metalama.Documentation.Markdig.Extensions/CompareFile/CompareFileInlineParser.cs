using Docfx.MarkdigEngine.Extensions;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax;
using Metalama.Documentation.Markdig.Extensions.Helpers;

namespace Metalama.Documentation.Markdig.Extensions.CompareFile;

public class CompareFileInlineParser : InlineParser
{
    private const string _startString = "[!metalama-compare";

    public CompareFileInlineParser()
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

        slice.SkipWhitespaces();

        if ( !slice.ReadUntilCharOrWhitespace( ']', out var path ) )
        {
            return false;
        }

        var resolvedPath = PathHelper.ResolvePath( path );

        var diff = new CompareFileInline { Src = resolvedPath };

        slice.SkipWhitespaces();

        if ( slice.CurrentChar != ']' )
        {
            return false;
        }

        diff.Span = new SourceSpan(
            processor.GetSourcePosition( saved.Start, out var line, out var column ),
            processor.GetSourcePosition( slice.Start - 1 ) );

        diff.Line = line;
        diff.Column = column;

        processor.Inline = diff;

        return true;
    }
}