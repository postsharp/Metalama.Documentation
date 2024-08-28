using Metalama.Documentation.Docfx.Cli;
using Spectre.Console.Cli;

var app = new CommandApp();

app.Configure(
    config =>
    {
        config.PropagateExceptions();
        config.AddCommand<MetadataCommand>( "metadata" );
        config.AddCommand<BuildCommand>( "build" );
    } );
    
return app.Run( args );