// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

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