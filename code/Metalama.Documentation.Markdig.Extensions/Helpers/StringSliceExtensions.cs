using Markdig.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace Metalama.Documentation.Markdig.Extensions.Helpers;

public static class StringSliceExtensions
{
    public static void SkipWhitespaces( this ref StringSlice slice )
    {
        while ( slice.CurrentChar.IsWhitespace() )
        {
            slice.SkipChar();
        }
    }

    public static bool ReadUntilCharOrWhitespace( this ref StringSlice slice, char endChar, [NotNullWhen( true )] out string? value )
        => slice.ReadUntil( c => c == endChar || c.IsWhitespace(), out value );

    public static bool ReadUntilChar( this ref StringSlice slice, char endChar, [NotNullWhen( true )] out string? value )
        => slice.ReadUntil( c => c == endChar, out value );

    public static bool ReadUntil(
        this ref StringSlice slice,
        Func<char, bool> predicate,
        [NotNullWhen( true )] out string? value )
    {
        var pathBuilder = StringBuilderCache.Local();

        var c = slice.CurrentChar;

        while ( !predicate( c ) && c != '\0' )
        {
            pathBuilder.Append( c );
            c = slice.NextChar();
        }

        if ( c == '\0' )
        {
            value = null;

            return false;
        }

        value = pathBuilder.ToString();

        return true;
    }
}