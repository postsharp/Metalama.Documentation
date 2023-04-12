// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Microsoft.DocAsCode.MarkdownLite;
using System.Text.RegularExpressions;

namespace Metalama.Documentation.DfmExtensions;

public sealed class SingleFileTokenRule : IMarkdownRule
{
    private static readonly Regex _regex = new( @"^\s*\[!metalama-file +(?<path>[^\s\]]+)\s*(?<transformed>transformed)?\s*(?<attributes>[^\]]*)\]" );

    public IMarkdownToken? TryMatch( IMarkdownParser parser, IMarkdownParsingContext context )
    {
        var match = _regex.Match( context.CurrentMarkdown );

        if ( match.Success )
        {
            var sourceInfo = context.Consume( match.Length );

            var path = match.Groups["path"].Value;
            var showTransformed = match.Groups["transformed"].Success;

            var attributes = AttributeMatcher.ParseAttributes( match.Groups["attributes"].Value );

            attributes.TryGetValue( "from", out var from );
            attributes.TryGetValue( "to", out var to );

            return new SingleFileToken( this, parser.Context, sourceInfo, path, showTransformed, from, to );
        }

        return null;
    }

    public string Name => "Metalama.SingleFile";
}