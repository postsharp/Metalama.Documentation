// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using Metalama.Documentation.Helpers.Redis;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Backends.Redis;
using Metalama.Patterns.Caching.Building;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;
using System.Threading.Tasks;

namespace Doc.RedisWithLocalCache;

internal static class Program
{
    public static async Task Main()
    {
        var builder = ConsoleApp.CreateBuilder();

        // Add a local Redis server with a random-assigned port. You don't need this in your code.
        using var redis = builder.Services.AddLocalRedisServer();

        // Add the caching service.                         
        builder.Services.AddMetalamaCaching( /*<AddMetalamaCaching>*/
            caching => caching.WithBackend(
                backend =>
                {
                    // Get the random port of the test Redis server. You don't need this in your code.
                    var redisServer = caching.ServiceProvider!.GetRequiredService<LocalRedisServer>();

                    // Build the Redis connection options.
                    var redisConnectionOptions = new ConfigurationOptions();
                    redisConnectionOptions.EndPoints.Add( "localhost", redisServer.Port );

                    // Build the Redis caching options. As a best practice, assign a version-specific KeyPrefix.
                    var thisAssembly = Assembly.GetCallingAssembly().GetName();
                    var keyPrefix = $"{thisAssembly.Name}.{thisAssembly.Version}";

                    // Finally, build the Redis caching back-end. Add an L1 cache.
                    return backend.Redis( new RedisCachingBackendConfiguration( redisConnectionOptions, keyPrefix ) )
                        .WithL1();
                } ) ); /*</AddMetalamaCaching>*/

        // Add other components as usual.
        builder.Services.AddAsyncConsoleMain<ConsoleMain>();
        builder.Services.AddSingleton<CloudCalculator>();

        // Build the host.
        await using var app = builder.Build();

        // Initialize caching.
        await app.Services.GetRequiredService<ICachingService>().InitializeAsync(); /*<Initialize>*/
        /*</Initialize>*/

        // Run the host.
        await app.RunAsync();
    }
}