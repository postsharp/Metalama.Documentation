using Docfx.MarkdigEngine.Extensions;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax;
using Metalama.Documentation.Markdig.Extensions.Helpers;
using Metalama.Documentation.Markdig.Extensions.Tabs;

namespace Metalama.Documentation.Markdig.Extensions.SingleFiles;

public class SingleFileInlineParser : InlineParser
{
    private const string _startString = "[!metalama-file";

    public SingleFileInlineParser()
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

        var file = new SingleFileInline { Src = resolvedPath };

        while ( true )
        {
            slice.SkipWhitespaces();
            var c = slice.CurrentChar;

            if ( c == ']' )
            {
                break;
            }

            if ( c == '\0' )
            {
                return false;
            }

            if ( !slice.MatchArgument( out var argument ) )
            {
                return false;
            }

            if ( argument.Value.Key == "transformed" )
            {
                file.ShowTransformed = true;

                continue;
            }

            if ( string.IsNullOrEmpty( argument.Value.Value ) )
            {
                throw new InvalidOperationException( $"Argument '{argument.Value.Key}' is missing a value." );
            }

            switch ( argument.Value.Key )
            {
                case "name":
                    file.Name = argument.Value.Value;

                    break;

                case "title":
                    file.Title = argument.Value.Value;

                    break;

                case "tabs":
                    file.Tabs = TabsHelper.SplitTabs( argument.Value.Value );

                    break;
                
                case "marker":
                    file.Marker = argument.Value.Value;

                    break;
                
                case "member":
                    file.Member = argument.Value.Value;

                    break;
                
                // TODO
                case "from":
                    break;

                default:
                    throw new InvalidOperationException( $"Unknown argument '{argument.Value.Key}'." );
            }
        }

        file.Span = new SourceSpan(
            processor.GetSourcePosition( saved.Start, out var line, out var column ),
            processor.GetSourcePosition( slice.Start - 1 ) );

        file.Line = line;
        file.Column = column;

        processor.Inline = file;

        return true;
    }
}