// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Microsoft.DocAsCode.MarkdownLite;
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

            return new AspectTestToken(
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

    public string Name => "Metalama.Test";
}