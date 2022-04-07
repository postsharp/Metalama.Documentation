﻿// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Amazon;
using BuildMetalamaDocumentation;
using PostSharp.Engineering.BuildTools;
using PostSharp.Engineering.BuildTools.Build.Solutions;
using PostSharp.Engineering.BuildTools.AWS.S3.Publishers;
using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.BuildTools.Dependencies.Model;
using Spectre.Console.Cli;

const string docPackageFileName = "Metalama.Doc.zip";

var product = new Product
{
    ProductName = "Metalama.Documentation",
    Solutions = new Solution[] {
                    new DotNetSolution( "code\\Metalama.Documentation.DfmExtensions\\Metalama.Documentation.DfmExtensions.csproj" ) { CanFormatCode = true },
                    new DotNetSolution( "code\\Metalama.Documentation.SampleCode.sln" ) { CanFormatCode = true, BuildMethod = BuildMethod.Test },
                    new DocFxSolution( "docfx\\docfx.json" ) },

    PublicArtifacts = Pattern.Create(
        docPackageFileName ),

    Dependencies = new [] {
        Dependencies.PostSharpEngineering,
        Dependencies.Metalama },
    AdditionalDirectoriesToClean = new[] { "docfx\\obj", "docfx\\_site" },

    // Disable automatic build triggers.
    Configurations = Product.DefaultConfigurations
        .WithValue( BuildConfiguration.Debug, c => c with { BuildTriggers = default } )
        .WithValue( BuildConfiguration.Public, new BuildConfigurationInfo(
            MSBuildName: "Release",
            PublicPublishers: new Publisher[]
            {
                new DocumentationPublisher( new S3PublisherConfiguration[] {
                    //TODO
                    new( docPackageFileName, RegionEndpoint.EUWest1, "doc.postsharp.net", docPackageFileName),
                } )
            } ) )
};



var commandApp = new CommandApp();

commandApp.AddProductCommands( product );

return commandApp.Run( args );
