using Amazon;
using BuildMetalamaDocumentation;
using PostSharp.Engineering.BuildTools;
using PostSharp.Engineering.BuildTools.AWS.S3.Publishers;
using PostSharp.Engineering.BuildTools.Build.Solutions;
using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.BuildTools.Build.Publishers;
using PostSharp.Engineering.BuildTools.Dependencies.Definitions;
using PostSharp.Engineering.BuildTools.Search;
using PostSharp.Engineering.BuildTools.Utilities;
using Spectre.Console.Cli;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using MetalamaDependencies = PostSharp.Engineering.BuildTools.Dependencies.Definitions.MetalamaDependencies.V2024_0;

var docPackageFileName = $"Metalama.Doc.{MetalamaDependencies.Metalama.ProductFamily.Version}.zip";

var product = new Product( MetalamaDependencies.MetalamaDocumentation )
{
    // Note that we don't build Metalama.Samples ourselves. We expect it to be built from the repo itself.
    // HTML artifacts should be restored from artifacts.

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
        new DocFxSolution( "docfx.json", docPackageFileName )
    },
    PublicArtifacts = Pattern.Create(
        docPackageFileName ),
    Dependencies =
        new[]
        {
            DevelopmentDependencies.PostSharpEngineering,
//            MetalamaDependencies.MetalamaMigration,
            MetalamaDependencies.MetalamaPatterns, MetalamaDependencies.MetalamaLinqPad,
            MetalamaDependencies.MetalamaSamples
        },
    SourceDependencies = new[] { MetalamaDependencies.MetalamaSamples, MetalamaDependencies.MetalamaCommunity },
    AdditionalDirectoriesToClean = new[] { "obj", "docfx\\_site" },
    Configurations = Product.DefaultConfigurations
        .WithValue( BuildConfiguration.Debug, c => c with { BuildTriggers = default } )

        .WithValue( BuildConfiguration.Public, c => c with
        {
            ExportsToTeamCityDeployWithoutDependencies = true,
            PublicPublishers = new Publisher[]
            {
                new MergePublisher(), new DocumentationPublisher( new S3PublisherConfiguration[]
                {
                    //TODO
                    new(docPackageFileName, RegionEndpoint.EUWest1, "doc.postsharp.net", docPackageFileName),
                } )
            }
        } ),
    Extensions = new ProductExtension[]
    {
        new UpdateSearchProductExtension<UpdateMetalamaDocumentationCommand>(
            "https://0fpg9nu41dat6boep.a1.typesense.net",
            "metalamadoc",
            "https://doc-production.metalama.net/sitemap.xml",
            true )
    }
};

product.PrepareCompleted += OnPrepareCompleted;


var commandApp = new CommandApp();

commandApp.AddProductCommands( product );

return commandApp.Run( args );


static void OnPrepareCompleted( PrepareCompletedEventArgs args )
{
    // Restore DocFx.
    var nuget = Path.Combine( Path.GetDirectoryName( Process.GetCurrentProcess().MainModule!.FileName )!, "nuget.exe " );

    if ( !ToolInvocationHelper.InvokeTool( args.Context.Console, nuget,
        "restore \"docfx\\packages.config\" -OutputDirectory \"docfx\\packages\"", args.Context.RepoDirectory ) )
    {
        args.IsFailed = true;
    }

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