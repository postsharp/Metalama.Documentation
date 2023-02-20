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
        AttributeMatcher.AttributeListMatcher
        +
        Matcher.WhiteSpacesOrEmpty +
        ']' +
        Matcher.WhiteSpacesOrEmpty +
        ( Matcher.NewLine.RepeatAtLeast( 1 ) | Matcher.EndOfString );

    public IMarkdownToken? TryMatch( IMarkdownParser parser, IMarkdownParsingContext context )
    {
        var match = context.Match( _matcher );

        if (match?.Length > 0)
        {
            var sourceInfo = context.Consume( match.Length );

            var path = match["path"].GetValue();

            var attributes = AttributeMatcher.ParseAttributes( match );

            attributes.TryGetValue( "name", out var name );
            attributes.TryGetValue( "title", out var title );
            attributes.TryGetValue( "tabs", out var tabs );

            return new SampleToken(
                this,
                parser.Context,
                sourceInfo,
                StringHelper.UnescapeMarkdown( path ),
                StringHelper.UnescapeMarkdown( name ?? "" ),
                StringHelper.UnescapeMarkdown( title ?? "" ),
                tabs ?? "" );
        }

        return null;
    }

    public string Name => "MetalamaSample";
}