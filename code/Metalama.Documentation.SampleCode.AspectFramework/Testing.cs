
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