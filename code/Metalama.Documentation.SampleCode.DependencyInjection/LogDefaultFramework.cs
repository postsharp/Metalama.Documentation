// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Documentation.Helpers.ConsoleApp;
using System;

namespace Doc.LogDefaultFramework
{
    // The class using the Log aspect. This class is instantiated by the host builder and dependencies are automatically passed.
    public class Worker : IConsoleMain
    {
        [Log]
        public void Execute()
        {
            Console.WriteLine( "Hello, world." );
        }
    }
}