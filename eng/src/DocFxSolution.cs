// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.BuildTools.Utilities;
using System.IO;
using System.IO.Compression;

namespace BuildMetalamaDocumentation
{
    public class DocFxSolution : Solution
    {
        public DocFxSolution( string solutionPath ) : base( solutionPath )
        {
            // Packing is done by the publish command.
            this.BuildMethod = PostSharp.Engineering.BuildTools.Build.Model.BuildMethod.Pack;            
        }

        public override bool Build( BuildContext context, BuildSettings settings )
        {
            if ( !RunDocFx( context, "metadata" ) )
            {
                return false;
            }

            if ( !RunDocFx( context, "build" ) )
            {
                return false;
            }

            return true;
        }

        private static bool RunDocFx( BuildContext context, string command )
        {
            return ToolInvocationHelper.InvokeTool(
                context.Console,
                Path.Combine( context.RepoDirectory, "docfx\\packages\\docfx.console.2.59.0\\tools\\docfx.exe" ),
                Path.Combine( context.RepoDirectory, "docfx\\docfx.json" ) + " " + command,
                context.RepoDirectory );
        }

        public override bool Pack( BuildContext context, BuildSettings settings )
        {
            if ( !this.Build( context, settings ))
            {
                return false;
            }

            ZipFile.CreateFromDirectory(
                Path.Combine( context.RepoDirectory, "docfx\\_site" ),
                Path.Combine( context.RepoDirectory, "artifacts\\publish\\private\\Metalama.Doc.zip" ) );

            return true;
        }

        public override bool Test( BuildContext context, BuildSettings settings )
        {
            return true;
        }

        public override bool Restore( BuildContext context, BuildSettings settings )
        {
            return DotNetHelper.Run(context, settings, Path.Combine( context.RepoDirectory, "docfx\\DocFx.csproj" ), "restore");
        }
    }
}