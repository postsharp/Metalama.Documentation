
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