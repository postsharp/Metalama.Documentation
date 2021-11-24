// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using PostSharp.Engineering.BuildTools;
using PostSharp.Engineering.BuildTools.Build.Model;
using Spectre.Console.Cli;
using System.Collections.Immutable;

namespace Build
{
    internal static class Program
    {
        private static int Main( string[] args )
        {
            var product = new Product
            {
                ProductName = "Caravela.Documentation",
                Solutions = ImmutableArray.Create<Solution>(
                    new DotNetSolution( "code\\Caravela.Documentation.SampleCode.sln" ) { CanFormatCode = true, BuildMethod = BuildMethod.Test },
                    new DocFxSolution( "docfx\\docfx.json" ) ),
                Dependencies = ImmutableArray.Create( new ProductDependency( "Caravela" ) ),
                AdditionalDirectoriesToClean = ImmutableArray.Create( "docfx\\obj", "docfx\\_site" )
            };

            var commandApp = new CommandApp();
            commandApp.AddProductCommands( product );

            return commandApp.Run( args );
        }
    }
}