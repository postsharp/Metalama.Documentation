﻿using Microsoft.DocAsCode.MarkdownLite;
using System.Text.RegularExpressions;

namespace Metalama.Documentation.DfmExtensions;

public sealed class VimeoTokenRule : IMarkdownRule
{
    private static readonly Regex _regex = new( @"^\s*\[!metalama-vimeo +(?<id>[^\s\]]+)\s*(?<attributes>[^\]]*)\]" );

    public IMarkdownToken? TryMatch( IMarkdownParser parser, IMarkdownParsingContext context )
    {
        var match = _regex.Match( context.CurrentMarkdown );

        if ( match.Success )
        {
            var sourceInfo = context.Consume( match.Length );

            var id = match.Groups["id"].Value;

            var attributes = AttributeMatcher.ParseAttributes( match.Groups["attributes"].Value );

            attributes.TryGetValue( "name", out var name );
            attributes.TryGetValue( "title", out var title );
            attributes.TryGetValue( "tabs", out var tabs );

            return new VimeoToken(
                this,
                parser.Context,
                sourceInfo,
                id );
        }

        return null;
    }

    public string Name => "Metalama.Vimeo";
}