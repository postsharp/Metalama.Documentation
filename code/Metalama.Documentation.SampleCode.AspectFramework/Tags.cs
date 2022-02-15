using System;

namespace Doc.Tags
{
    internal class TargetCode
    {
        [TagsAspect]
        private void Method( int a, int b )
        {
            Console.WriteLine( $"Method({a}, {b})" );
        }
    }
}