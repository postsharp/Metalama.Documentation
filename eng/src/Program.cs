
using Amazon;
using BuildMetalamaDocumentation;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using PostSharp.Engineering.BuildTools;
using PostSharp.Engineering.BuildTools.Build.Solutions;
using PostSharp.Engineering.BuildTools.AWS.S3.Publishers;
using PostSharp.Engineering.BuildTools.Build;
using PostSharp.Engineering.BuildTools.Build.Model;
using PostSharp.Engineering.BuildTools.Build.Publishers;
using PostSharp.Engineering.BuildTools.Dependencies.Definitions;
using PostSharp.Engineering.BuildTools.Utilities;
using Spectre.Console.Cli;
using System.IO;
using System.Diagnostics;
using PostSharp.Engineering.BuildTools.Search;
using MetalamaDependencies = PostSharp.Engineering.BuildTools.Dependencies.Definitions.MetalamaDependencies.V2023_3;

const string docPackageFileName = "Metalama.Doc.zip";

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
        new DocFxSolution( "docfx.json" )
    },
    PublicArtifacts = Pattern.Create(
        docPackageFileName ),
    Dependencies =
        new[]
        {
            DevelopmentDependencies.PostSharpEngineering, MetalamaDependencies.MetalamaMigration,
            MetalamaDependencies.MetalamaLinqPad, MetalamaDependencies.MetalamaSamples
        },
    SourceDependencies = new[] { MetalamaDependencies.MetalamaSamples, MetalamaDependencies.MetalamaCommunity },
    AdditionalDirectoriesToClean = new[] { "obj", "docfx\\_site" },

    Configurations = Product.DefaultConfigurations
        // Disable automatic build triggers.
        .WithValue( BuildConfiguration.Debug, c => c with { BuildTriggers = default } )

        // Documentation 2023.3 is not yet published. See earlier versions for deployment configuration.
        .WithValue( BuildConfiguration.Public, c => c with { ExportsToTeamCityDeploy = false } ),
    
    // Extensions = new ProductExtension[]
    // {
    //     new UpdateSearchProductExtension(
    //         "https://0fpg9nu41dat6boep.a1.typesense.net",
    //         "metalamadoc",
    //         "https://doc-production.metalama.net/sitemap.xml",
    //         true )
    // }
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
    
    // Copy HTML artefact dependencies to the source dependency directory.
    var htmlSourceDirectory = Path.Combine( args.Context.RepoDirectory, "dependencies", "Metalama.Samples", "html" );
    var htmlTargetDirectory = Path.Combine( args.Context.RepoDirectory, "source-dependencies", "Metalama.Samples", "examples" );
    
    if ( Directory.Exists( htmlSourceDirectory ) )
    {
        args.Context.Console.WriteMessage( $"Restoring HTML files from '{htmlSourceDirectory}'." );
        var matcher = new Matcher();
        matcher.AddInclude("**/*.html");
        var matches = matcher.Execute(new DirectoryInfoWrapper( new DirectoryInfo( htmlSourceDirectory ) ));

        var count = 0;
        foreach (var match in matches.Files)
        {
            var sourceFile = Path.Combine( htmlSourceDirectory, match.Path );
            var targetFile = Path.Combine( htmlTargetDirectory, match.Path );
            var targetSubdirectory = Path.GetDirectoryName( targetFile )!;
            Directory.CreateDirectory( targetSubdirectory );
            File.Copy(sourceFile, targetFile, true);

            count++;
        }
        
        args.Context.Console.WriteMessage( $"{count} files copied." );
        
    }
}