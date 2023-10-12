// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Doc.LogCustomFramework;
using Metalama.Documentation.Helpers.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Doc.LogDefaultFramework
{
    // Program entry point. Creates the host, configure dependencies, and runs it.
    public static class Program
    {
        private static void Main()
        {
            var appBuilder = ConsoleApp.CreateBuilder();
            appBuilder.Services.AddLogging( logging => logging.AddConsole() );
            appBuilder.Services.AddConsoleMain<ConsoleMain>();
            using var app = appBuilder.Build();
            app.Run();
        }
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