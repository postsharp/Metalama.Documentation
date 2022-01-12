// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.Testing
{
    internal class SimpleLogTests
    {
        [SimpleLog]
        private void MyMethod()
        {
            Console.WriteLine( "Hello, world" );
        }
    }
}