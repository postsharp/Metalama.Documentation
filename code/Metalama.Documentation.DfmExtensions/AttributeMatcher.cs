using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.DocAsCode.MarkdownLite.Matchers;

namespace Metalama.Documentation.DfmExtensions;

internal static class AttributeMatcher
{
    private static readonly Regex _oneAttributeRegex = new( @"(?<name>\w+)=(""(?<quoted_value>[^""]*)""|(?<unquoted_value>\w+))" );

    public static Matcher AttributeListMatcher { get; } = ( Matcher.AnyWordCharacter.RepeatAtLeast( 1 ) + Matcher.WhiteSpacesOrEmpty + Matcher.Char( '=' )
                                                            + Matcher.WhiteSpacesOrEmpty + Matcher.Char( '"' ) + Matcher.AnyCharNot( '"' ).RepeatAtLeast( 0 )
                                                            + Matcher.Char( '"' )
                                                            + Matcher.WhiteSpacesOrEmpty ).Repeat( 0, 32  ).ToGroup( "attributes" )
        .RepeatAtLeast( 0 );

    public static Dictionary<string, string> ParseAttributes( MatchResult match )
    {
        var attributes = match["attributes"].GetValue();
        var dictionary = new Dictionary<string, string>( StringComparer.OrdinalIgnoreCase );

        foreach (Match attributeMatch in _oneAttributeRegex.Matches( attributes ))
        {
            var attributeName = attributeMatch.Groups["name"].Value;
            var attributeValue = ( attributeMatch.Groups["quoted_value"] ?? attributeMatch.Groups["unquoted_value"] ).Value;
            dictionary[attributeName] = attributeValue;
        }

        return dictionary;
    }
}