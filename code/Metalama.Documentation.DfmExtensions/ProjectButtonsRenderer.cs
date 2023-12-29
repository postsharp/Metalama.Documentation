// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Microsoft.DocAsCode.MarkdownLite;
using System;
using System.IO;
using System.Linq;

namespace Metalama.Documentation.DfmExtensions;

internal class ProjectButtonsRenderer : BaseRenderer<ProjectButtonsToken>
{
    protected override StringBuffer RenderCore( ProjectButtonsToken token, MarkdownBlockContext context )
    {
        var directory = token.Directory;
        var id = Path.GetFileNameWithoutExtension( directory ).ToLowerInvariant().Replace( '.', '_' );

        var tabGroup = new DirectoryTabGroup( id, directory );

        foreach ( var file in Directory.GetFiles( directory, "*.cs" ) )
        {
            var lines = File.ReadAllLines( file );

            var kind = lines.Any( l => l.StartsWith( "using Metalama.Framework" ) ) ? SandboxFileKind.AspectCode : SandboxFileKind.None;

            if ( kind == SandboxFileKind.None )
            {
                // We need to try harder to find the good category.


                var outHtmlPath = PathHelper.GetObjPath( token.Directory, file, ".t.cs.html" ); 
                    
                if ( outHtmlPath == null || !File.ReadAllText( outHtmlPath ).Contains( "cr-GeneratedCode" ) )
                {
                    kind = SandboxFileKind.ExtraCode;
                }
                else
                {
                    kind = SandboxFileKind.TargetCode;
                }
            }

            tabGroup.Tabs.Add( new CodeTab( Path.GetFileNameWithoutExtension( file ).ToLowerInvariant(), file, Path.GetFileName( file ), kind ) );
        }

        var sandboxPayload = tabGroup.GetSandboxPayload( token );

        if ( sandboxPayload == null )
        {
            throw new InvalidOperationException( $"Cannot load '{directory}' into the sandbox." );
        }

        var gitUrl = tabGroup.GetGitUrl();

        return $@"
<div class=""project-buttons"">
    <a href=""{gitUrl}"" class=""github"">See on GitHub</a>
    <a role=""button"" class=""sandbox"" onclick=""openSandbox('{sandboxPayload}')"" target=""github"">Open in sandbox</a>
</div>";
    }
}