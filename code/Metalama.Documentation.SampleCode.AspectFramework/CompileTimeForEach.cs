// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this git repo for details.

using System;

namespace Doc.CompileTimeForEach
{
    internal class Foo
    {
        [CompileTimeForEach]
        private void Bar( int a, string b )
        {
            Console.WriteLine( $"Hello, world! a={a}, b='{b}'." );
        }
    }
}