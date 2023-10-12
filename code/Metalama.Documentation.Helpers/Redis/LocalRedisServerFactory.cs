// This is public domain Metalama sample code.

using Microsoft.Extensions.DependencyInjection;

namespace Metalama.Documentation.Helpers.Redis;

public static class LocalRedisServerFactory
{
    public static IServiceCollection AddLocalRedisServer( this IServiceCollection serviceCollection )
    {
        serviceCollection.AddSingleton<LocalRedisServer>();

        return serviceCollection;
    }
}