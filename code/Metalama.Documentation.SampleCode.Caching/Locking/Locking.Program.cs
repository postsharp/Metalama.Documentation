// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Building;
using Metalama.Patterns.Caching.Locking;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.Locking
{
    internal static class Program
    {
        public static void Main()
        {
            var builder = ConsoleApp.CreateBuilder();

            // Add the caching service.
            builder.Services.AddCaching( /*<AddCaching>*/
                caching =>
                    caching.AddProfile( new CachingProfile( "Locking" ) { LockingStrategy = new LocalLockingStrategy() } ) ); /*</AddCaching>*/

            // Add other components as usual, then run the application.
            builder.Services.AddConsoleMain<ConsoleMain>();
            builder.Services.AddSingleton<CloudService>();

            using var app = builder.Build();
            app.Run();
        }
    }
}