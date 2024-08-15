using System.Collections.Immutable;

namespace Metalama.Documentation.Markdig.Extensions.Helpers;

internal static class DotNetVersions
{
    public static ImmutableArray<string> Versions { get; } = ImmutableArray.Create( "net6.0", "net7.0" );
}