// This is public domain Metalama sample code.

using Microsoft.DocAsCode.MarkdownLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Metalama.Documentation.DfmExtensions;

internal static class PathHelper
{
    public static string GetRelativePath( string projectDir, string targetPath )
        => new Uri( Path.Combine( projectDir, "_" ) ).MakeRelativeUri( new Uri( targetPath ) )
            .ToString();

    public static IReadOnlyList<string> GetObjPaths(
        string projectDir,
        string targetPath,
        string extension )
    {
        var relativePath = GetRelativePath( projectDir, targetPath );

        return DotNetVersions.Versions.Select(
                v => Path.GetFullPath(
                    Path.Combine(
                        projectDir,
                        "obj",
                        "html",
                        v,
                        Path.ChangeExtension( relativePath, extension ) ) ) )
            .ToArray();
    }

    public static string ResolveTokenPath(
        string path,
        IMarkdownContext context,
        SourceInfo sourceInfo )
    {
        if ( context == null! )
        {
            // This happens in tests.
            return path;
        }

        var baseDirectory = (string) context.Variables["BaseFolder"];
        path = path.Replace( "~", baseDirectory ).Replace( "/", "\\" );

        var directory = Path.Combine( baseDirectory, Path.GetDirectoryName( sourceInfo.File )! );
        path = Path.GetFullPath( Path.Combine( directory, path ) );

        return path;
    }
}