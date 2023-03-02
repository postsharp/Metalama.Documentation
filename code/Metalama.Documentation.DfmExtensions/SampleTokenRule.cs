
using Microsoft.DocAsCode.MarkdownLite;
using Microsoft.DocAsCode.MarkdownLite.Matchers;
using System.Text.RegularExpressions;

namespace Metalama.Documentation.DfmExtensions;

public sealed class SampleTokenRule : IMarkdownRule
{
    private static readonly Regex _regex = new( @"^\s*\[!metalama-sample +(?<path>[^\s\]]+)\s*(?<attributes>[^\]]*)\]" );

    public IMarkdownToken? TryMatch( IMarkdownParser parser, IMarkdownParsingContext context )
    {
        var match = _regex.Match( context.CurrentMarkdown );

        if ( match.Success )
        {
            var sourceInfo = context.Consume( match.Length );

            var path = match.Groups["path"].Value;

            var attributes = AttributeMatcher.ParseAttributes( match.Groups["attributes"].Value );

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