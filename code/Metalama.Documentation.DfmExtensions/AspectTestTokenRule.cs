// This is public domain Metalama sample code.

using Microsoft.DocAsCode.MarkdownLite;
using System;
using System.Text.RegularExpressions;

namespace Metalama.Documentation.DfmExtensions;

public sealed class AspectTestTokenRule : IMarkdownRule
{
    private static readonly Regex _regex = new( @"^\s*\[!metalama-test +(?<path>[^\s\]]+)\s*(?<attributes>[^\]]*)\]" );

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

            if ( !attributes.TryGetValue( "diff-side", out var diffSideString )
                 || !Enum.TryParse<DiffSide>( diffSideString, out var diffSide ) )
            {
                diffSide = DiffSide.Both;
            }

            return new AspectTestToken(
                this,
                parser.Context,
                sourceInfo,
                StringHelper.UnescapeMarkdown( path ),
                StringHelper.UnescapeMarkdown( name ?? "" ),
                StringHelper.UnescapeMarkdown( title ?? "" ),
                tabs ?? "",
                diffSide: diffSide );
        }

        return null;
    }

    public string Name => "Metalama.Test";
}