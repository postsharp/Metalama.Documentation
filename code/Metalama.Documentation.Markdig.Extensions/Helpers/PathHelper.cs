// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Docfx.MarkdigEngine.Extensions;

namespace Metalama.Documentation.Markdig.Extensions.Helpers;

internal static class PathHelper
{
    public static string GetRelativePath( string projectDir, string targetPath )
        => new Uri( Path.Combine( projectDir, "_" ) ).MakeRelativeUri( new Uri( targetPath ) ).ToString();

    public static string? GetObjPath( string projectDir, string targetPath, string extension )
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

    public static string ResolvePath( string path )
    {
        var rootDirectory = Environment.CurrentDirectory;
        var sourceFileRelativePath = InclusionContext.File.ToString()!;
        
        var sourceFilePath = Path.Combine( rootDirectory, sourceFileRelativePath );

        if ( path.StartsWith( "~", StringComparison.Ordinal ) )
        {
            var baseDirectory = GitHelper.GetGitDirectory( sourceFilePath );
            path = path.Replace( "~", baseDirectory, StringComparison.Ordinal ).Replace( "/", "\\", StringComparison.Ordinal );
        }
        else
        {
            var baseDirectory = Path.GetDirectoryName( sourceFilePath )!;
            path = path.Replace( "/", "\\", StringComparison.Ordinal );
            path = Path.Combine( baseDirectory, path );
        }

        path = Path.GetFullPath( path );

        return path;
    }
}