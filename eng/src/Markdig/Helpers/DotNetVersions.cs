// This is public domain Metalama sample code.

using System.Collections.Immutable;

namespace BuildMetalamaDocumentation.Markdig.Helpers;

internal static class DotNetVersions
{
    public static ImmutableArray<string> Versions { get; } =
        ImmutableArray.Create(
            "net6.0",
            "net7.0",
            "net6.0-windows",
            "net7.0-windows",
            "net8.0",
            "net8.0-windows" );
}