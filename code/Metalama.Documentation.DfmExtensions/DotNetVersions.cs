// This is public domain Metalama sample code.

using System.Collections.Immutable;

namespace Metalama.Documentation.DfmExtensions;

internal static class DotNetVersions
{
    public static ImmutableArray<string> Versions { get; } = ImmutableArray.Create( "net6.0", "net7.0" );
}