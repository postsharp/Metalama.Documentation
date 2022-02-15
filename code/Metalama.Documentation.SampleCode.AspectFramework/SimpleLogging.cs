using System;

namespace Doc.SimpleLogging
{
    internal class TargetCode
    {
        [SimpleLog]
        public void Method1()
        {
            Console.WriteLine( "Hello, world." );
        }
    }
}