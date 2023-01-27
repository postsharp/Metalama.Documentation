using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.DocAsCode.MarkdownLite;
using Microsoft.DocAsCode.MarkdownLite.Matchers;

namespace Metalama.Documentation.DfmExtensions;

public sealed class SampleTokenRule : IMarkdownRule
{
    private static readonly Matcher _matcher =
        Matcher.WhiteSpacesOrEmpty +
        "[!" +
        Matcher.CaseInsensitiveString( "metalama-sample" ) +
        Matcher.WhiteSpacesOrEmpty +
        Matcher.AnyCharNotIn( ' ', ']' ).RepeatAtLeast( 1 ).ToGroup( "path" ) +
        Matcher.WhiteSpacesOrEmpty +
        ( Matcher.AnyWordCharacter.RepeatAtLeast( 1 ) + Matcher.WhiteSpacesOrEmpty + Matcher.Char( '=' )
          + Matcher.WhiteSpacesOrEmpty + Matcher.Char( '"' ) + Matcher.AnyCharNot( '"' ).RepeatAtLeast( 0 ) + Matcher.Char( '"' )
          + Matcher.WhiteSpacesOrEmpty )
        .RepeatAtLeast( 0 )
        .ToGroup( "attributes" )
        +
        Matcher.WhiteSpacesOrEmpty +
        ']' +
        Matcher.WhiteSpacesOrEmpty +
        ( Matcher.NewLine.RepeatAtLeast( 1 ) | Matcher.EndOfString );

    private static readonly Regex _attributesRegex = new( @"(?<name>\w+)=(""(?<quoted_value>[^""]*)""|(?<unquoted_value>\w+))" );

    public IMarkdownToken? TryMatch( IMarkdownParser parser, IMarkdownParsingContext context )
    {
        var match = context.Match( _matcher );

        if (match?.Length > 0)
        {
            var sourceInfo = context.Consume( match.Length );

            var path = match["path"].GetValue();
            var attributes = match["attributes"].GetValue();
            string name = "";
            string title = "";

            foreach (Match attributeMatch in _attributesRegex.Matches( attributes ))
            {
                var attributeName = attributeMatch.Groups["name"];
                var attributeValue = ( attributeMatch.Groups["quoted_value"] ?? attributeMatch.Groups["unquoted_value"] ).Value;

                switch (attributeName.Value)
                {
                    case "name":
                        name = attributeValue;

                        break;

                    case "title":
                        title = attributeValue;

                        break;
                }
            }

            return new SampleToken(
                this,
                parser.Context,
                StringHelper.UnescapeMarkdown( path ),
                StringHelper.UnescapeMarkdown( name ),
                StringHelper.UnescapeMarkdown( title ),
                sourceInfo );
        }

        return null;
    }

    public string Name => "Sample";
}