// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Documentation.Helpers.ConsoleApp;
using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Building;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Doc.Profiles
{
    public static class Program
    {
        public static void Main()
        {
            var builder = ConsoleApp.CreateBuilder();

            // Add the caching service.
            builder.Services.AddCaching( /*<Registration>*/
                caching => caching
                    .AddProfile( new CachingProfile { AbsoluteExpiration = TimeSpan.FromMinutes( 60 ) } )
                    .AddProfile( new CachingProfile( "Hot" ) { AbsoluteExpiration = TimeSpan.FromMilliseconds( 100 ) } ) ); /*</Registration>*/

            // Add other components as usual.
            builder.Services.AddConsoleMain<ConsoleMain>();
            builder.Services.AddSingleton<ProductCatalogue>();

            // Run the main service.
            using var app = builder.Build();
            app.Run();
        }
    }
}