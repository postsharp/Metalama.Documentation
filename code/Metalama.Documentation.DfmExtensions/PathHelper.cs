// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Microsoft.DocAsCode.MarkdownLite;
using System;
using System.IO;
using System.Linq;

namespace Metalama.Documentation.DfmExtensions;

internal static class PathHelper
{
    public static string GetRelativePath( string projectDir, string targetPath )
        => new Uri( Path.Combine( projectDir, "_" ) ).MakeRelativeUri( new Uri( targetPath ) ).ToString();

    public static string? GetObjPath(string projectDir, string targetPath, string extension )
    {
        var relativePath = GetRelativePath( projectDir, targetPath );

        var possiblePaths = DotNetVersions.Versions.Select(
                v => Path.GetFullPath(
                    Path.Combine(
                        projectDir,
                        "obj",
                        "html",
                        v,
                        Path.ChangeExtension( relativePath, extension ) ) ) )
            .ToArray();
        
        foreach ( var path in possiblePaths )
        {
            if ( File.Exists( path ) )
            {
                return path;
            }
        }

        return null;
    }

    public static string ResolveTokenPath( string path, IMarkdownContext context, SourceInfo sourceInfo )
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