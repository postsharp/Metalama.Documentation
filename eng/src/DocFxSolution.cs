// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.BuildTools.Utilities;
using System;
using System.IO;

namespace BuildMetalamaDocumentation
{
    public class DocFxSolution : Solution
    {
        public DocFxSolution( string solutionPath ) : base( solutionPath )
        {
            // Packing is done by the publish command.
            this.BuildMethod = PostSharp.Engineering.BuildTools.Build.Model.BuildMethod.Build;
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
                $"\"docfx\\docfx.json\" {command}",
                context.RepoDirectory );
        }

        public override bool Pack( BuildContext context, BuildSettings settings ) => throw new NotImplementedException();

        public override bool Test( BuildContext context, BuildSettings settings ) => throw new NotSupportedException();

        public override bool Restore( BuildContext context, BaseBuildSettings settings )
        {
            return ToolInvocationHelper.InvokeTool(
                context.Console,
                "msbuild",
                "/t:Restore \"docfx\\DocFx.csproj\"",
                context.RepoDirectory );
        }
    }
}