// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Metalama.Documentation.DfmExtensions;

internal static class AttributeMatcher
{
    private static readonly Regex _oneAttributeRegex = new( @"(?<name>\w+)=(""(?<quoted_value>[^""]*)""|(?<unquoted_value>\w+))" );

    public static Dictionary<string, string> ParseAttributes( string attributes )
    {
        var dictionary = new Dictionary<string, string>( StringComparer.OrdinalIgnoreCase );

        foreach ( Match attributeMatch in _oneAttributeRegex.Matches( attributes ) )
        {
            var attributeName = attributeMatch.Groups["name"].Value;
            var attributeValue = (attributeMatch.Groups["quoted_value"] ?? attributeMatch.Groups["unquoted_value"]).Value;
            dictionary[attributeName] = attributeValue;
        }

        return dictionary;
    }
}