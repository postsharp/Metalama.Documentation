// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Caching.Aspects;
using System;

namespace Doc.GettingStarted_NoDI
{
    public static class CloudCalculator
    {
        public static int OperationCount { get; private set; }

        [Cache]
        public static int Add( int a, int b )
        {
            Console.WriteLine( "Doing some very hard work." );

            OperationCount++;

            return a + b;
        }
    }
}