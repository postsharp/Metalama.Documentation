﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Amazon;
using BuildMetalamaDocumentation;
using PostSharp.Engineering.BuildTools;
using PostSharp.Engineering.BuildTools.Build.Solutions;
using PostSharp.Engineering.BuildTools.AWS.S3.Publishers;
using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.BuildTools.Dependencies.Model;
using PostSharp.Engineering.BuildTools.Utilities;
using Spectre.Console.Cli;
using System.IO;
using System.Diagnostics;
using PostSharp.Engineering.BuildTools.Build.Publishers;

const string docPackageFileName = "Metalama.Doc.zip";

var product = new Product( Dependencies.MetalamaDocumentation )
{
    Solutions = new Solution[]
    {
        new DotNetSolution( "code\\Metalama.Documentation.Prerequisites.sln" ) { CanFormatCode = true },
        new DotNetSolution( "code\\Metalama.Documentation.Snippets.TestBased.sln" )
        {
            CanFormatCode = true, BuildMethod = BuildMethod.Test,
        },
        new DotNetSolution( "code\\Metalama.Documentation.Snippets.ProjectBased.sln" )
        {
            CanFormatCode = true, BuildMethod = BuildMethod.Build,
        },
        new DotNetSolution( "source-dependencies\\Metalama.Samples\\Metalama.Samples.sln") { BuildMethod = BuildMethod.Test },
        new DocFxSolution( "docfx.json" )
    },

    PublicArtifacts = Pattern.Create(
        docPackageFileName ),
    
    Dependencies = new[] { 
         Dependencies.PostSharpEngineering,
         Dependencies.MetalamaMigration,
         Dependencies.MetalamaLinqPad },

    SourceDependencies = new[]
    {
        Dependencies.MetalamaSamples
    },

    AdditionalDirectoriesToClean = new[] { "obj", "docfx\\_site" },

    // Disable automatic build triggers.
    Configurations = Product.DefaultConfigurations
        .WithValue( BuildConfiguration.Debug, c => c with { BuildTriggers = default } )
        .WithValue( BuildConfiguration.Public, new BuildConfigurationInfo(
            MSBuildName: "Release",
            ExportsToTeamCityDeployWithoutDependencies: true,
            PublicPublishers: new Publisher[]
            {
                new MergePublisher(),
                new DocumentationPublisher( new S3PublisherConfiguration[]
                {
                    //TODO
                    new(docPackageFileName, RegionEndpoint.EUWest1, "doc.postsharp.net", docPackageFileName),
                } )
            } ) )
};

product.PrepareCompleted += OnPrepareCompleted;


var commandApp = new CommandApp();

commandApp.AddProductCommands( product );

return commandApp.Run( args );


static void OnPrepareCompleted( PrepareCompletedEventArgs args )
{
    var nuget = Path.Combine( Path.GetDirectoryName( Process.GetCurrentProcess().MainModule!.FileName )!, "nuget.exe " );

    if ( !ToolInvocationHelper.InvokeTool( args.Context.Console, nuget,
        "restore \"docfx\\packages.config\" -OutputDirectory \"docfx\\packages\"", args.Context.RepoDirectory ) )
    {
        args.IsFailed = true;
    }
}