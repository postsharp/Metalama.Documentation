﻿using Amazon;
using BuildMetalamaDocumentation;
using PostSharp.Engineering.BuildTools;
using PostSharp.Engineering.BuildTools.S3.Publishers;
using PostSharp.Engineering.BuildTools.Build.Solutions;
using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.BuildTools.Build.Publishers;
using PostSharp.Engineering.BuildTools.Dependencies.Definitions;
using Spectre.Console.Cli;
using System.IO;
using System.IO.Compression;
using MetalamaDependencies = PostSharp.Engineering.BuildTools.Dependencies.Definitions.MetalamaDependencies.V2024_1;

var docPackageFileName = $"Metalama.Doc.{MetalamaDependencies.Metalama.ProductFamily.Version}.zip";

var product = new Product( MetalamaDependencies.MetalamaDocumentation )
{
    // Note that we don't build Metalama.Samples ourselves. We expect it to be built from the repo itself.
    // HTML artifacts should be restored from artifacts.

    Solutions =
    [
        new DotNetSolution( "code\\Metalama.Documentation.Prerequisites.sln" ) { CanFormatCode = true },
        new DotNetSolution( "code\\Metalama.Documentation.Snippets.TestBased.sln" )
        {
            CanFormatCode = true, BuildMethod = BuildMethod.Test,
        },
        new DotNetSolution( "code\\Metalama.Documentation.Snippets.ProjectBased.sln" )
        {
            CanFormatCode = true, BuildMethod = BuildMethod.Build,
        },
        new DocFxMetadataSolution( "docfx.json" ),
        new DocFxBuildSolution( "docfx.json", docPackageFileName )
    ],
    PublicArtifacts = Pattern.Create(
        docPackageFileName ),
    Dependencies =
    [
        DevelopmentDependencies.PostSharpEngineering,
            MetalamaDependencies.MetalamaMigration,
            MetalamaDependencies.MetalamaPatterns,
            MetalamaDependencies.MetalamaLinqPad,
            MetalamaDependencies.MetalamaSamples
    ],
    SourceDependencies = [MetalamaDependencies.MetalamaSamples, MetalamaDependencies.MetalamaCommunity],
    AdditionalDirectoriesToClean = [Path.Combine( "artifacts", "api" ), Path.Combine( "artifacts", "site" )],
    Configurations = Product.DefaultConfigurations
        .WithValue( BuildConfiguration.Debug, c => c with { BuildTriggers = default } )

        .WithValue( BuildConfiguration.Public, c => c with
        {
            ExportsToTeamCityDeployWithoutDependencies = true,
            PublicPublishers = new Publisher[]
            {
                new MergePublisher(), new DocumentationPublisher( new S3PublisherConfiguration[]
                {
                    new(docPackageFileName, RegionEndpoint.EUWest1, "doc.postsharp.net", docPackageFileName),
                }, "https://postsharp-helpbrowser.azurewebsites.net/" )
            }
        } )
};

product.PrepareCompleted += OnPrepareCompleted;


var commandApp = new CommandApp();

commandApp.AddProductCommands( product );

return commandApp.Run( args );


static void OnPrepareCompleted( PrepareCompletedEventArgs args )
{
    // Extract HTML artefact dependencies to the source dependency directory.
    var htmlSourceZipFile = Path.Combine( args.Context.RepoDirectory, "dependencies", "Metalama.Samples", "html-examples.zip" );
    var htmlTargetDirectory = Path.Combine( args.Context.RepoDirectory, "source-dependencies", "Metalama.Samples", "examples" );

    if ( File.Exists( htmlSourceZipFile ) )
    {
        args.Context.Console.WriteMessage( $"Restoring HTML files from '{htmlSourceZipFile}'." );
        Directory.CreateDirectory( htmlTargetDirectory );
        ZipFile.ExtractToDirectory( htmlSourceZipFile, htmlTargetDirectory, true );
    }
}