// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;

namespace Doc.Tags
{
    internal class Foo
    {
        [TagsAspect]
        private void Bar( int a, int b )
        {
            Console.WriteLine( $"Method({a}, {b})" );
        }
    }
}