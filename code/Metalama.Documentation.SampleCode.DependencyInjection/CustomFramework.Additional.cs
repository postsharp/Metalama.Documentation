using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Doc.CustomFramework
{
    public static class Program
    {
        private static Task Main() =>
            CreateHostBuilder( Array.Empty<string>() ).Build().RunAsync();

        private static IHostBuilder CreateHostBuilder( string[] args ) =>
           Host.CreateDefaultBuilder( args )
               .ConfigureServices( ( _, services ) =>
                   services.AddHostedService<Worker>() )
                                       .ConfigureLogging( loggingBuilder => loggingBuilder.AddFilter( "Microsoft.Hosting.Lifetime", LogLevel.None )  );


    }

}