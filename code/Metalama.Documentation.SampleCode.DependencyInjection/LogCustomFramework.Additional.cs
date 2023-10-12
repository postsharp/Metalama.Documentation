// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Documentation.Helpers.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Doc.LogCustomFramework
{
    // Program entry point. Creates the host, configure dependencies, and runs it.
    public static class Program
    {
        private static void Main()
        {
            var builder = ConsoleApp.CreateBuilder();
            builder.Services.AddLogging( logging => logging.AddConsole() );
            builder.Services.AddConsoleMain<ConsoleMain>();
            using var app = builder.Build();
            app.Run();
        }
    }
}