using Metalama.Documentation.Docfx.Cli;
using Spectre.Console.Cli;

var app = new CommandApp();

app.Configure(
    config =>
    {
        config.PropagateExceptions();
        config.AddCommand<GenerateManagedReferenceYamlFilesCommand>( "api" );
        config.AddCommand<BuildCommand>( "doc" );
    } );
    
return app.Run( args );