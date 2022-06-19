// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using System;

namespace Doc.Testing
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