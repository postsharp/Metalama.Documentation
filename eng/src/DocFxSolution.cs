// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.BuildTools.Utilities;
using System;

namespace Build
{
    public class DocFxSolution : Solution
    {
        public DocFxSolution( string solutionPath ) : base( solutionPath )
        {
            // Packing is done by the publish command.
            this.BuildMethod = PostSharp.Engineering.BuildTools.Build.Model.BuildMethod.Build;
        }

        public override bool Build( BuildContext context, BuildOptions options )
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
                "docfx\\packages\\docfx.console.2.58.9\\tools\\docfx.exe",
                $"\"docfx\\docfx.json\" {command}",
                context.RepoDirectory );
        }

        public override bool Pack( BuildContext context, BuildOptions options ) => throw new NotImplementedException();

        public override bool Test( BuildContext context, BuildOptions options ) => throw new NotSupportedException();

        public override bool Restore( BuildContext context, BaseBuildSettings options )
        {
            return ToolInvocationHelper.InvokeTool(
                context.Console,
                "nuget",
                "restore \"docfx\\packages.config\" -OutputDirectory \"docfx\\packages\"",
                context.RepoDirectory );
        }
    }
}