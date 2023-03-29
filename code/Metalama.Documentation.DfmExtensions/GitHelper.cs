// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;
using System.Diagnostics;
using System.IO;

namespace Metalama.Documentation.DfmExtensions;

internal static class GitHelper
{
    public static string GetRepoUrl( string directory )
    {
        // Execute the git command to get the remote URL of the origin
        var psi = new ProcessStartInfo( "git", "remote get-url origin" )
        {
            RedirectStandardOutput = true, WorkingDirectory = directory, UseShellExecute = false
        };

        var process = Process.Start( psi );
        process.WaitForExit();

        // Read the output of the command
        var output = process.StandardOutput.ReadToEnd().Trim( ' ', '\n', '\r' );

        
        if ( !output.EndsWith( ".git" ) )
        {
            throw new InvalidOperationException( $"The git remote url '{output}' does not end with '.git." );
        }

        return output.Substring( 0, output.Length - ".git".Length );

    }

    public static string GetGitDirectory( string path )
    {
        for ( var directory = Path.GetDirectoryName( path ); directory != null; directory = Path.GetDirectoryName( directory ) )
        {
            if ( Directory.Exists( Path.Combine( directory, ".git" ) ) )
            {
                return directory;
            }
        }

        throw new InvalidOperationException( $"Cannot find the project directory for '{path}'." );
    }

    public static string GetOnlineUrl( string path )
    {
        var gitDirectory = GetGitDirectory( path );
        
        var relativePath = PathHelper.GetRelativePath( gitDirectory, path );
        var gitUrl = GetRepoUrl( gitDirectory );

        var fullUrl = gitUrl + "/blob/master/" + relativePath.Replace( "\\", "/" );

        return fullUrl;
    }
}