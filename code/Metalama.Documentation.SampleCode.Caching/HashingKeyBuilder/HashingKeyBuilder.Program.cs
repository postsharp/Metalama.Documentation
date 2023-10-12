// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Documentation.Helpers.ConsoleApp;
using Metalama.Patterns.Caching.Building;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.HashKeyBuilder
{
    internal static class Program
    {
        public static void Main()
        {
            var builder = ConsoleApp.CreateBuilder();

            // Add the caching service.
            builder.Services.AddCaching(                                                                         /*<Registration>*/
                caching => caching.WithKeyBuilder( ( formatters, _ ) => new HashingKeyBuilder( formatters ) ) ); /*</Registration>*/

            // Add other components as usual.
            builder.Services.AddConsoleMain<ConsoleMain>();
            builder.Services.AddSingleton<FileSystem>();

            // Run the main service.
            using var app = builder.Build();
            app.Run();
        }
    }
}