// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using System;

namespace Doc.SimpleLogging
{
    internal class Foo
    {
        [SimpleLog]
        public void Method1()
        {
            Console.WriteLine( "Hello, world." );
        }
    }
}