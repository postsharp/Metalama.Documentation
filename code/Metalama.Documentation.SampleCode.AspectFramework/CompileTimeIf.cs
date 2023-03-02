
using System;

namespace Doc.CompileTimeIf
{
    internal class Foo
    {
        [CompileTimeIf]
        public void InstanceMethod()
        {
            Console.WriteLine( "InstanceMethod" );
        }

        [CompileTimeIf]
        public static void StaticMethod()
        {
            Console.WriteLine( "StaticMethod" );
        }
    }
}