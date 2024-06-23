// This is public domain Metalama sample code.

using Microsoft.DocAsCode.MarkdownLite;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace Metalama.Documentation.DfmExtensions;

public sealed class MultipleFilesTokenRule : IMarkdownRule
{
    private static readonly Regex _regex = new(
        @"^\s*\[!metalama-files(?<paths>(\s+[^\s\]=]+)+)*(?<attributes>\s(?=\w+=)+[^\]]*)?\s*\]" );

    public IMarkdownToken? TryMatch( IMarkdownParser parser, IMarkdownParsingContext context )
    {
        var match = _regex.Match( context.CurrentMarkdown );

        if ( match.Success )
        {
            var sourceInfo = context.Consume( match.Length );

            var paths = match.Groups["paths"]
                .Value.Split( ' ' )
                .Select( x => x.Trim() )
                .Where( x => !string.IsNullOrEmpty( x ) )
                .ToArray();

            var attributes = AttributeMatcher.ParseAttributes( match.Groups["attributes"].Value );

            attributes.TryGetValue( "mode", out var mode );
            attributes.TryGetValue( "name", out var name );
            attributes.TryGetValue( "links", out var links );

            return new MultipleFilesToken(
                this,
                parser.Context,
                sourceInfo,
                name ?? Path.GetFileName( (string) parser.Context.Variables["BaseFolder"] ),
                paths,
                mode == null ? TabMode.Default : (TabMode) Enum.Parse( typeof(TabMode), mode ),
                addLinks: string.IsNullOrEmpty( links ) || XmlConvert.ToBoolean( links ) );
        }

        return null;
    }

    public string Name => "Metalama.SingleFile";
}