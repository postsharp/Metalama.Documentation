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
    Solutions = ImmutableArray.Create<Solution>(
                    new DotNetSolution( "code\\Metalama.Documentation.SampleCode.sln" ) { CanFormatCode = true, BuildMethod = BuildMethod.Test },
                    new DocFxSolution( "docfx\\docfx.json" ) ),
    Dependencies = ImmutableArray.Create(
        Dependencies.PostSharpEngineering,
        Dependencies.Metalama ),
        AdditionalDirectoriesToClean = ImmutableArray.Create("docfx\\obj", "docfx\\_site" )
};

var commandApp = new CommandApp();

commandApp.AddProductCommands(product);

return commandApp.Run(args);