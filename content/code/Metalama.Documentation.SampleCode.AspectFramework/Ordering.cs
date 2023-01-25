// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using System;

namespace Doc.Ordering
{
    [Aspect1]
    [Aspect2]
    internal class Foo
    {
        public static void SourceMethod()
        {
            Console.WriteLine( "Method defined in source code." );
        }
    }

    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine( "Executing SourceMethod:" );
            Foo.SourceMethod();

            Console.WriteLine( "---" );
            Console.WriteLine( "Executing IntroducedMethod1:" );
#if TESTRUNNER
             Foo.IntroducedMethod1();
#endif

            Console.WriteLine( "---" );
            Console.WriteLine( "Executing IntroducedMethod2:" );
#if TESTRUNNER
             Foo.IntroducedMethod2();
#endif
        }
    }
}