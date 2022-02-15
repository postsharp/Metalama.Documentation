// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using BuildMetalamaDocumentation;
using PostSharp.Engineering.BuildTools;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.BuildTools.Dependencies.Model;
using Spectre.Console.Cli;
using System.Collections.Immutable;

var product = new Product
{
    ProductName = "Metalama.Documentation",
    Solutions = new Solution[] {
                    new DotNetSolution( "code\\Metalama.Documentation.DfmExtensions\\Metalama.Documentation.DfmExtensions.csproj" ) { CanFormatCode = true },
                    new DotNetSolution( "code\\Metalama.Documentation.SampleCode.sln" ) { CanFormatCode = true, BuildMethod = BuildMethod.Test },
                    new DocFxSolution( "docfx\\docfx.json" ) },
    Dependencies = new [] {
        Dependencies.PostSharpEngineering,
        Dependencies.Metalama },
    AdditionalDirectoriesToClean = new[] { "docfx\\obj", "docfx\\_site" },

    // Disable automatic build triggers.
    Configurations = Product.DefaultConfigurations.WithValue( PostSharp.Engineering.BuildTools.Build.BuildConfiguration.Debug, c => c with { BuildTriggers = default } )
};



var commandApp = new CommandApp();

commandApp.AddProductCommands( product );

return commandApp.Run( args );
