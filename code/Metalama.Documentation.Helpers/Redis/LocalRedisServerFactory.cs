// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

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