
using Amazon;
using BuildMetalamaDocumentation;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
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
    
    Dependencies = new[] { 
         Dependencies.PostSharpEngineering,
         Dependencies.MetalamaMigration,
         Dependencies.MetalamaLinqPad,
         Dependencies.MetalamaSamples
    },

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