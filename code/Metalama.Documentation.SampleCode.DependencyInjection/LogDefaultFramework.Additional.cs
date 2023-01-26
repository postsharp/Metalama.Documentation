using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Doc.LogDefaultFramework
{
    // Program entry point. Creates the host, configure dependencies, and runs it.
    public static class Program
    {
        private static Task Main() =>
            CreateHostBuilder( Array.Empty<string>() ).Build().RunAsync();

        private static IHostBuilder CreateHostBuilder( string[] args ) =>
            Host.CreateDefaultBuilder( args )
                .ConfigureServices( ( _, services ) =>
                    services.AddHostedService<Worker>()
                            .AddScoped<IMessageWriter, MessageWriter>() )
                                        .ConfigureLogging( loggingBuilder => loggingBuilder.ClearProviders() );

    }

    // Definition of the interface consumed by the aspect.
    public interface IMessageWriter
    {
        void Write( string message );
    }

    // Implementation actually consumed by the aspect.
    public class MessageWriter : IMessageWriter
    {
        public void Write( string message )
        {
            Console.WriteLine( message );
        }
    }
}