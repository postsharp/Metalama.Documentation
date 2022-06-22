using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Doc.DependencyInjection
{
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

    public interface IMessageWriter
    {
        void Write( string message );
    }

    public class MessageWriter : IMessageWriter
    {
        public void Write( string message )
        {
            Console.WriteLine( message );
        }
    }
}