
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